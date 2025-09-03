using System.Linq;
using System.Threading.Tasks;
using Godot;
using Tiles;

namespace Board;

public partial class TileOrganizer: Node, Organizable, WithTiles
{
    [Export] private Node _tileFactory;
    [Export] private Node _environment;
    [Export] private Node _tileContainer;
    [Export] private Node _tileMatcher;
    public Grid<Control> Tiles {get; set;}
    private float[] _spawnWeights;
    private TileTypes[] _spawnTiles;
    private System.Collections.Generic.Dictionary<TileTypes, int> _spawnOddsByTileType;
    
    public override void _Ready(){
		_spawnOddsByTileType = new(){ 
			{TileTypes.Defensive, 3},
            {TileTypes.Melee, 3},
            {TileTypes.Ranged, 3},
            {TileTypes.Tech, 3},
            {TileTypes.Walk, 3}, //distinct from what's in tileMatcher
		};
		_spawnWeights = [.._spawnOddsByTileType.Select(item => item.Value)];   
        _spawnTiles = [.._spawnOddsByTileType.Select(item => item.Key)];   
    }

    public void Initialize(Grid<TileTypes> tileTypes){
        (_tileFactory as TileMaking).Initialize();
        Tiles = _MakeTileNodes(tileTypes, _tileFactory as TileMaking);
        (_tileContainer as Viewable).Initialize(Tiles);
    }

    private Grid<Control> _MakeTileNodes(
        Grid<TileTypes> tileTable,
        TileMaking factory
    ){
        var board = new Grid<Control>(tileTable.Width, tileTable.Height);
        for (int x = 0; x < tileTable.Width; x++){
            for (int y = 0; y < tileTable.Height; y++){
                var tileName = tileTable.GetItem(y, x); //REVERSED
                if (tileName != TileTypes.Blank){
                    var tile = (Control) factory.Create(tileName);
                    board.SetCell(tile, x, y);
                }
            }
        }
        return board;				
    }   


    public void RelocateTile(Control tile, Vector2I target){
        var source = Tiles.GetCellFor(tile);
        Tiles.SetCell(tile, target);
        Tiles.SetCell((_tileFactory as TileMaking).Create(TileTypes.Blank) as Control, source);
    }

    public async Task TransferTileToTile(Control sourceTile, Control targetTile){
        var target = Tiles.GetCellFor(targetTile);
        var source = Tiles.GetCellFor(sourceTile);
        Tiles.SetCell(sourceTile, target);
        (sourceTile as Movable).MoveTo(target);

        //result is an array that stores the signal parameters, don't need here
        /* var result = */ await ToSignal(targetTile, "Removed"); 

        //_FillEmptyCell(source, _spawnWeights, _spawnTiles); 

        //(_tileMatcher as MatchableBoard).MatchWithoutSwapping();

        Tiles.SetCell((_tileFactory as TileMaking).Create(TileTypes.Blank) as Control, source);

        (_tileMatcher as MatchableBoard).CollapseGridAndCheckNewMatches();

        GD.Print("former player position is now:  ", (Tiles.GetItem(source) as Tile).Type);
    } 

    public /* async Task */ void TransferTileTo(Control tile, Vector2I target){
        ///
        var watch = System.Diagnostics.Stopwatch.StartNew();
        /// 
        var currentCell = Tiles.GetCellFor(tile);
        Tiles.SetCell(tile, target);
        (tile as Movable).MoveTo(target);
        //await ToSignal(tile, "FinishedTransfering");
        //await (tile as Player.Manager).WaitForTransferFinishedSignal();
        //
        watch.Stop();
        GD.Print("TransferTileTo   milliseconds:  ", watch.ElapsedMilliseconds);
        //
        //_FillEmptyCell(currentCell, _spawnWeights, _spawnTiles); 
        //(_tileMatcher as MatchableBoard).MatchWithoutSwapping();

    }


    private void _FillEmptyCell(Vector2I cell, float[] spawnWeights, TileTypes[] spawnTiles){
        var random = new RandomNumberGenerator();
        var tileType = spawnTiles[random.RandWeighted(spawnWeights)]; 
        var spawnedTile = (_tileFactory as TileMaking).Create(tileType) as Control;
        Tiles.SetCell(spawnedTile, cell);   
        _AddTile(spawnedTile, cell); 
    }

    private void _AddTile(Control tile, Vector2I cell){
        (_tileContainer as Viewable).Add(tile, cell);
    }        
}