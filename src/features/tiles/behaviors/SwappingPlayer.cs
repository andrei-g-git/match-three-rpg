using System.Threading.Tasks;
using Board;
using Godot;
using Tiles;

namespace Tiles;

public partial class SwappingPlayer : Node, Swappable, AccessableBoard
{
    [Export] Node _tileRoot;
    public Node Board { private get; set;}

    public void/* async Task */ SwapWith(Control tile){
        //I was about to check if the tile that engaged is an enemy, but enemies don't swap to attack, so I prob won't use this. should delete
        (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control)
            .ContinueWith(task => {
                var matchSuccessful = task.Result;
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
            }); 

        
    }
}