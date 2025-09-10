using System.Threading.Tasks;
using Board;
using Godot;
using Tiles;

namespace Tiles;

public partial class Swapping : Node, Swappable, AccessableBoard
{
    [Export] Node _tileRoot;
    public Node Board { private get; set;}

    public /* void */async Task SwapWith(Control tile){
        var bp = 123;
        var matchSuccessful = await (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control);
        if(matchSuccessful){
            //(_tileRoot as Agentive).AdvanceTurn();
            /* 
                emit signal Matched
             */
        }else{
            GD.Print("can't match");
            /* 
                emnit signal EngagingDirectly ---> need Engage behavior
             */
        }
    }
}