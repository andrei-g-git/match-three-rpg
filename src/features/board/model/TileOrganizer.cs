using Godot;
using Tiles;

namespace Board;

public partial class TileOrganizer: Node, Organizable
{
    [Export] private Node _tileFactory;
    [Export] private Node _environment;
    [Export] private Node _tileContainer;
    public Grid<Control> Tiles {get; set;}

    public void Initialize(Grid<TileTypes> tileTypes){
        Tiles = _MakeTileNodes(tileTypes, _tileFactory as TileMaking);
        (_tileContainer as Viewable).Initialize(Tiles);
    }

    private Grid<Control> _MakeTileNodes(
        Grid<TileTypes> tileTable,
        TileMaking factory
    ){
        var _board = new Grid<Control>(tileTable.Width, tileTable.Height);
        for (int x = 0; x < tileTable.Width; x++){
            for (int y = 0; y < tileTable.Width; y++){
                var tileName = tileTable.GetItem(y, x); //REVERSED
                if (tileName != TileTypes.Blank){
                    var tile = (Control) factory.Create(tileName);
                    _board.SetCell(tile, x, y);
                }
            }
        }
        return _board;				
    }    
}