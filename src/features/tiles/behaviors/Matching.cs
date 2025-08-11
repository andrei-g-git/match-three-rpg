using Godot;
using Tiles;

public partial class Matching : Node, Matchable
{
    [Signal] public delegate void MatchedRemotelyEventHandler();
    public void BeginPostMatchProcessDependingOnPlayerPosition(Vector2I ownPosition, Node playerTile, bool playerAjacent){
        if(playerAjacent){

        }else{
            EmitSignal(SignalName.MatchedRemotely);
        }
    }
}