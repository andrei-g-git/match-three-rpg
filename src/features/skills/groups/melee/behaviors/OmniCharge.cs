using Board;
using Godot;
using Godot.Collections;
using Tiles;

public partial class OmniCharge : Node, /* Traversing.IBehavior, */ AccessableBoard
{
    [Export] private Node _sceneRoot;  
    public Node Board { get; set; }
    private int _pathIndex = 0;
    [Signal]
    public delegate void AttackingEventHandler(Node enemy, int coveredTileDistance);    
    [Signal]
    public delegate void FinishedMovingEventHandler();

    public void ProcessPath(Array<Vector2I> path){
        (_sceneRoot as Dashing.IAnimator).DashToNext(path, _pathIndex, ProcessPath);
        var neighbors = (Board as Queriable).GetNeighboringTiles(path[_pathIndex]);
        foreach(var tile in neighbors){    
            if(tile is Disposition actor && actor.IsEnemy){
                EmitSignal(SignalName.Attacking, actor as Node, _pathIndex + 1);
            }
        }
        _pathIndex++;
        if(_pathIndex >= path.Count){
            EmitSignal(SignalName.FinishedMoving);
        }
    }
}
