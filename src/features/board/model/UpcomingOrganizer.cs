using Board;
using Content;
using Godot;
using Levels;
using Room;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;
using Util;

public partial class UpcomingOrganizer : Node
{
	[Export] private Node _tileOrganizer;
	[Export] private Node _tileFactory;
	[Export] private Node _tileContainer;
	[Export] private Node _roomModifiers;
    [Export] private Node _tileMatcher;

	private Grid<Control> _tiles => (_tileOrganizer as WithTiles).Tiles;
	//private GameSave _loadedGame;
    private List<PieceOdds> _randomPieceDistribution;
	private delegate void CollapsedEventHandler();
	private CollapsedEventHandler _collapsedEvent;

	public override void _Ready(){
		_collapsedEvent += _FillInTheBlanks;


        //might be cleaner if this was in the board manager and passed here, but screw it, it's less work
		Files.LoadJson<CurrentSaveGame>(Files.SavesPath, "current.json")
			.ContinueWith(task => {
				var currentGame = task.Result;
				var currentGameName = currentGame.CurrentSave;
				Files.LoadJson<GameSave>(Files.SavesPath, currentGameName)
					.ContinueWith(t => {

						var loadedGame = t.Result; 

						var roomIndex = loadedGame.LevelIndex;

					
                        Files.LoadJson<List<LevelSchema>>(Files.LevelsPath, "levels.json")
                            .ContinueWith(task =>{
                                var levels = task.Result;
                                var level = levels[roomIndex];
                                _randomPieceDistribution = level.RandomPieceDistribution;
                            });	                    
					});
			
			});

	}

    public void Initialize(Grid<TileTypes> pieceTypes){
        (_tileOrganizer as Organizable).Initialize(pieceTypes);

        _FillInTheBlanks(); //this might run before  random piece distribution data is loaded

        Debugging.PrintStackedGridInitials(_tiles.GetGridAs2DList(), 2, 2, "UPCOMING PIECES INITIALIZED");
    }

    //not in interface
    public async Task<List<Control>> MoveColumnDown(int column, int cellCount){ //should move to upcoming organizer, I'll just copy/paste for now
        GD.PrintRich($"[color=green] MOVING COLUMN {column}; has {cellCount} pieces to move; Grid height is {_tiles.Height} [/color]");
        var cellsToTransfer = new List<Control>();
        for(int y = _tiles.Height-1; y >= 0; y--){
            var piece = _tiles.GetItem(column, y);
            if(piece != null && (piece as Tile).Type != TileTypes.Blank){
                _tiles.SetCell( //might work, gets replaced by falling piece if necessary
                    (Control) (_tileFactory as TileMaking).Create(TileTypes.Blank),   
                    column,
                    y              
                );

                var fallToHeight = y + cellCount;
                if(fallToHeight >= _tiles.Height){
                    fallToHeight = _tiles.Height; //otherwise it may keep falling over the play area and settle there
                    cellsToTransfer.Add(piece); 
                }else{
                    _tiles.SetCell(piece, column, fallToHeight);   
                }

                (piece as Movable).MoveOverDistance(new Vector2I(column, fallToHeight), fallToHeight - y);
                if(y <= 0){
                    await (piece as Movable).WaitUntilMoved();                    
                }                
            }
        } 
        //cellsToTransfer.Reverse(); 
        foreach(var piece in cellsToTransfer){
            _tileContainer.RemoveChild(piece);
        }

		//New

		_collapsedEvent.Invoke();

		//

        Debugging.PrintStackedGridInitials(_tiles.GetGridAs2DList(), 2, 2, "UPCOMING PIECE COLUMN COLLAPSED");

        return cellsToTransfer;          
    }

    //TODO: Ensure that there are no matched generations
    //      Also make sure that enemies don't act in the upcoming grid
	private void _FillInTheBlanks(){ 
		var levelMods = (_roomModifiers as ModifiableRoom).Modifiers;
		if(
			levelMods.Contains(LevelModifiers.random_upcoming_pieces.ToString()) &&
			levelMods.Contains(LevelModifiers.infinite_upcoming_pieces.ToString())
		){
			for(var x=0; x<_tiles.Width; x++){
				for(var y=0; y<_tiles.Height; y++){
					if((_tiles.GetItem(x, y) as Tile).Type == TileTypes.Blank/*  || (_tiles.GetItem(x, y) == null) */){
                        var blank = _tiles.GetItem(x, y);
						//var pieceName = _RollPiece(_randomPieceDistribution);
                        var pieceName = _RollPieceWithoutMatches(_randomPieceDistribution, new Vector2I(x, y));
                        var pieceType = TileDict.GetEnum(pieceName);
                        var piece = (_tileFactory as TileMaking).Create(pieceType);
                        _tiles.SetCell(piece as Control, new Vector2I(x, y));
                        (_tileContainer as Viewable).PlaceNew(piece as Control, blank, new Vector2I(x, y));                        
					}
				}
			}
            //(_tileContainer as Viewable).Initialize(_tiles); //2ND TIME

		}
	}

    private string _RollPiece(List<PieceOdds> pieceDistribution){
        int maxOdds = 0;
        foreach(var pieceOdds in pieceDistribution){
            maxOdds += pieceOdds.Odds;
        }
        var rng = new Random();
        int roll = rng.Next(maxOdds);

        int cumulativeInterval = 0;
        foreach(var pieceOdds in pieceDistribution){
            cumulativeInterval += pieceOdds.Odds;
            if(roll < cumulativeInterval){


                //(_tileMatcher as MatchableBoard).CheckForMatchesInUpcomingGrid;
                return pieceOdds.Piece;
            }
        } 

        return "no upcoming randomized piece for you!";       
    }

    private string _RollPieceWithoutMatches(List<PieceOdds> pieceDistribution, Vector2I cell)
    {
        var pieceName = _RollPiece(pieceDistribution);
        var pieceType = TileDict.GetEnum(pieceName);
        var piece = (_tileFactory as TileMaking).Create(pieceType) as Control;    
        var testGrid = _tiles.Clone();
        testGrid.SetCell(piece, cell);    

        var gotMatches = (_tileMatcher as /* MatchableBoard */TileMatcher).CheckForMatchesInUpcomingGrid(testGrid); //this whole thing looks like shit...
        while (gotMatches)
        {
            pieceName = _RollPiece(pieceDistribution);
            pieceType = TileDict.GetEnum(pieceName);
            piece = (_tileFactory as TileMaking).Create(pieceType) as Control;    
            testGrid = _tiles.Clone();
            testGrid.SetCell(piece, cell);  
            gotMatches = (_tileMatcher as TileMatcher).CheckForMatchesInUpcomingGrid(testGrid);          
        }

        return pieceName;
    }
}
