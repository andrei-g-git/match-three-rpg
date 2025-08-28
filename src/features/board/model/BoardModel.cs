using System.Threading.Tasks;
using Godot;
using Tiles;

namespace Board;

public partial class BoardModel : Node, Organizable, MatchableBoard, WithTiles
{
    [Export] private Node _tileOrganizer;
    [Export] private Node _tileMatcher;
    [Export] private Node _tileFactory;
    [Export] private Node _tileContainer;

    public Grid<Control> Tiles { get => (_tileOrganizer as WithTiles).Tiles; set => (_tileOrganizer as WithTiles).Tiles = value; }

    // private Grid<Control> _tiles;
    // public Grid<Control> Tiles {
    //     get => _tiles; 
    //     set{
    //         (_tileMatcher as WithTiles).Tiles = value;
    //         _tiles = value;
    //     }
    // }


    public void Initialize(Grid<TileTypes> tileTypes){
        (_tileOrganizer as Organizable).Initialize(tileTypes);
        //Tiles = (_tileOrganizer as WithTiles).Tiles;
        (_tileMatcher as WithTiles).Tiles = (_tileOrganizer as WithTiles).Tiles;
    }


    public bool TryMatching(Control sourceTile, Control targetTile){
        return (_tileMatcher as MatchableBoard).TryMatching(sourceTile, targetTile);
    }

    public async Task TransferTileToTile(Control sourceTile, Control targetTile){
        await (_tileOrganizer as Organizable).TransferTileToTile(sourceTile, targetTile);
    }
}