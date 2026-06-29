using Board;
using Godot;
using Levels;
using Room;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class UpcomingOrganizer : Node
{
	[Export] private Node _tileOrganizer;
	[Export] private Node _tileFactory;
	[Export] private Node _tileContainer;
	[Export] private Node _roomModifiers;

	private Grid<Control> _tiles => (_tileOrganizer as WithTiles).Tiles;

	private delegate void CollapsedEventHandler();
	private CollapsedEventHandler _collapsedEvent;

	public override void _Ready(){
		_collapsedEvent += _FillInTheBlanks;
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
        return cellsToTransfer;          
    }

	private void _FillInTheBlanks(){
		var levelMods = (_roomModifiers as ModifiableRoom).Modifiers;
		if(
			levelMods.Contains(LevelModifiers.random_upcoming_pieces.ToString()) &&
			levelMods.Contains(LevelModifiers.infinite_upcoming_pieces.ToString())
		){
			for(var x=0; x<_tiles.Width; x++){
				for(var y=0; y<_tiles.Height; y++){
					if((_tiles.GetItem(x, y) as Tile).Type == TileTypes.Blank)
					{
						
					}
				}
			}
		}
	}
}
