using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Godot;
using Godot.Collections;
using Skills;
using Tiles;

public partial class OmniCharge : Node, /* Movable, */ Traversing, AccessableBoard, WithTileRoot
{
    [Export] private Node _sceneRoot;  
    [Export] private AnimatedSprite2D _sprite;
    public Node Board { get; set; }
    public Control TileRoot {get; set;}
    private int _pathIndex = 0; //I should reset this but normally the skill is removed after use so maybe it's fine
    [Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);    
    [Signal] public delegate void FinishedTransferingEventHandler();
    [Signal] public delegate void FinishedPathEventHandler();

    public void ProcessPath(List<Vector2I> path){ 
        /* _ =  */(Board as Organizable).TransferTileTo(TileRoot, path[_pathIndex]); 
        var neighbors = (Board as Queriable).GetNeighboringTiles(path[_pathIndex]);
        foreach(var tile in neighbors){    
            if(tile is Disposition actor && actor.IsEnemy){
                EmitSignal(SignalName.Attacking, actor as Control, _pathIndex + 1);
            }
        }
        _pathIndex++;
        if(_pathIndex >= path.Count){
            EmitSignal(SignalName.FinishedPath);
        }else{
            ProcessPath(path);
        }
        EmitSignal(SignalName.FinishedTransfering);
    }


    public async Task ProcessPath(List<Vector2I> path, bool testOverload) {
        if (_pathIndex >= path.Count) {
            //EmitSignal(SignalName.FinishedTransfering);
            EmitSignal(SignalName.FinishedPath);
            return;
        }

        _sprite.Play();


        /* await */ (Board as Organizable).TransferTileTo(TileRoot, path[_pathIndex]);
        await (_sprite as WhirlwindSprite).WaitForAnimationFinished();
        //await ToSignal(_sprite, /* _sprite.AnimationFinished */"animation_finished");


        var neighbors = (Board as Queriable).GetNeighboringTiles(path[_pathIndex]);
        foreach (var tile in neighbors) {
            if (tile is Disposition actor && actor.IsEnemy) {
                EmitSignal(SignalName.Attacking, actor as Control, _pathIndex + 1);
            }
        }
        EmitSignal(SignalName.FinishedTransfering);

        _pathIndex++;
        await ProcessPath(path, true);
    }    
}
