using Board;
using Godot;
using Tiles;

namespace Tiles;

public partial class SwappingPlayer : Node, Swappable, AccessableBoard
{
    [Export] Node _tileRoot;
    public Node Board { private get; set;}

    public void SwapWith(Control tile){
        //I was about to check if the tile that engaged is an enemy, but enemies don't swap to attack, so I prob won't use this. should delete
        var matchSuccessful = (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control);
        if(!matchSuccessful){
            GD.Print("can't match");
        }
    }
}