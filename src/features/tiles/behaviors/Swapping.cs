using Board;
using Godot;
using Tiles;

namespace Tiles;

public partial class Swapping : Node, Swappable, AccessableBoard
{
    [Export] Node _tileRoot;
    public Node Board { private get; set;}

    public void SwapWith(Control tile){
        var matchSuccessful = (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control);
        if(!matchSuccessful){
            GD.Print("can't match");
        }
    }
}