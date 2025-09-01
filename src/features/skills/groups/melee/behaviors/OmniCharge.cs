using System.Collections.Generic;
using Board;
using Godot;
using Godot.Collections;
using Skills;
using Tiles;

public partial class OmniCharge : Node, /* Movable, */ Traversing, AccessableBoard, WithTileRoot
{
    [Export] private Node _sceneRoot;  
    public Node Board { get; set; }
    public Control TileRoot {get; set;}
    private int _pathIndex = 0; //I should reset this but normally the skill is removed after use so maybe it's fine
    [Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);    
    [Signal] public delegate void FinishedTransferingEventHandler();

    public void ProcessPath(List<Vector2I> path){ //the async inside transfertileto doesn't work
        _ = (Board as Organizable).TransferTileTo(TileRoot, path[_pathIndex]); //has ToSignal that awaits end of all animations for that cell stage
        var neighbors = (Board as Queriable).GetNeighboringTiles(path[_pathIndex]);
        foreach(var tile in neighbors){    
            if(tile is Disposition actor && actor.IsEnemy){
                EmitSignal(SignalName.Attacking, actor as Control, _pathIndex + 1);
            }
        }
        _pathIndex++;
        if(_pathIndex >= path.Count){
            EmitSignal(SignalName.FinishedTransfering);
        }else{
            ProcessPath(path);
        }
    }
}
