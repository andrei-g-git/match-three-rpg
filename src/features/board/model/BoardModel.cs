using Godot;
using Tiles;

namespace Board;

public partial class BoardModel : Node, Organizable, MatchableBoard
{
    [Export] private Node _tileOrganizer;
    [Export] private Node _tileMatcher;
    private Grid<Control> _tiles;
    public Grid<Control> Tiles {
        get => _tiles; 
        set{
            (_tileOrganizer as Organizable).Tiles = value;
        }
    }

    public void Initialize(Grid<TileTypes> tileTypes){
        (_tileOrganizer as Organizable).Initialize(tileTypes);
    }

    public bool TryMatching(Control sourceTile, Control targetTile){
        return (_tileMatcher as MatchableBoard).TryMatching(sourceTile, targetTile);
    }
}