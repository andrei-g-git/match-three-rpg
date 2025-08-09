using Godot;
using Tiles;

namespace Board;

public partial class BoardModel : Node, Organizable
{
    [Export] private Node _tileOrganizer;
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
}