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
    [Signal] public delegate void GotPropelledEventHandler(Vector2I toCell, int intensity);


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

    //no interface yet
    public async void SwapInvoluntarilyTo(Vector2I toCell, int movementForce){
        var pieceToSwap = (Board as Queriable).GetItemAt(toCell);
        var cell = (Board as Queriable).GetCellFor(_tileRoot as Control);
           //I should make TryMatching return a touple of 2 bools for whether it matched andd for whether it at least swapped
        var (canSwap, matchSuccessful) = await (Board as MatchableBoard).TryMatching(cell, toCell);  
        if(canSwap){ //actually this won't be the case for an enemy since it's not itself swapable
            EmitSignal(SignalName.GotPropelled, toCell, movementForce);
        }
        //should first check the canSwap part of the touple, if yes, emit a mere push signal, if no, then the piece just slammed into an obstacle and should take damage
        //if it matched, then I should make a case for that
        else
        {
            EmitSignal(SignalName.EngagingDirectly, tile);                  
        }

    }
}