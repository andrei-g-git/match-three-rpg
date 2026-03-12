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
    //[Signal] public delegate void GotPropelledEventHandler(Vector2I toCell, int intensity);
    [Signal] public delegate void MetObstacleEventHandler(Vector2I atCell, float intensity);


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
        var matchSuccessful = await (Board as MatchableBoard).TryMatching(tile, _tileRoot as Control);   //come to think of it this is completely useless for enemies since they are not swapable./..  
        if(matchSuccessful){
            
        }else{
            GD.Print("can't match");
            EmitSignal(SignalName.EngagingDirectly, tile);
        }            
    }

    //TODO: this should just be in the Pushed behavior
    public async void SwapInvoluntarilyTo(Vector2I toCell, float movementForce){
        var pieceToSwap = (Board as Queriable).GetItemAt(toCell);
        var cell = (Board as Queriable).GetCellFor(_tileRoot as Control);

        if(pieceToSwap is Swappable swappablePiece){
            _ = (Board as Organizable).MoveBySwapping(_tileRoot as Control, pieceToSwap);
        }else{
            //TODO:
            // if(pieceToSwap is Destroyable){
            //     MoveBySwapping
            //     destroy unswappable but destroyable piece eg. table
            //      MetObstacle - take damage, maybe reduced -- decrease movement force
            // } else {

            EmitSignal(SignalName.MetObstacle, toCell, movementForce);   
        }
    }
}