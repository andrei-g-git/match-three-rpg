using System.Threading.Tasks;
using Board;
using Godot;
using Tiles;

namespace Tiles;

public partial class Swapping : Node, Swappable, AccessableBoard
{
    [Export] Node _tileRoot;
    public Node Board { private get; set;}
    [Signal] public delegate void EngagingDirectlyEventHandler(Control targetNode);


    public async void SwapWith(Control tile){
        // (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control)
        //     .ContinueWith(task => {
        //         var matchSuccessful = task.Result;
        //         if(matchSuccessful){
        //         }else{
        //             GD.Print("can't match");
        //             EmitSignal(SignalName.EngagingDirectly, tile);
        //         }                
        //     });      
        var matchSuccessful = await (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control);     
        if(matchSuccessful){
            
        }else{
            GD.Print("can't match");
            EmitSignal(SignalName.EngagingDirectly, tile);
        }            
    }
}