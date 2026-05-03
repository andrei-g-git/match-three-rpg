using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class DelayedSmash : Control, Skill, DelayableSkill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, FilterableSkill
{
    public Control TileRoot { get; set; }
    public Node Board { private get; set; }
    public AnimationTree AnimationTree { private get; set; }

	private int _builtUpMagnitude;

	//[Export] Node _delete;

	//[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);

	public async Task ProcessPathAsync(List<Vector2I> path){
		var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

		playback.Travel("SwingStart");

		_builtUpMagnitude = path.Count;
	}

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

    public async Task ActivateDelayedSkill(){
        //skill conclusion will be handled here
		//await conclusion

		//await ToSignal(_delete, "timeout"); //delete
		var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

		playback.Travel("SwingEnd");	
		var playerCell = (Board as Queriable).GetCellFor(TileRoot);
        var neighbors = (Board as Queriable).GetNeighboringTiles(playerCell);
        neighbors.Remove(TileRoot); 
        foreach (var tile in neighbors) {
            if (tile is Disposition actor && actor.IsEnemy) {
                //EmitSignal(SignalName.Attacking, actor as Control, _pathIndex + 1);       

				(actor as Defensible).TakeDamage(69420);             
            }
        }			
    }

    public /* static */ bool CheckIfUsable(List<Vector2I> matchedGroup, SkillNames.SkillGroups skillGroup, Queriable boardQuery){
		var playerPosition = boardQuery.GetPlayerPosition();
        var playerIsAdjacent = boardQuery.IsCellAdjacentToLine(playerPosition, matchedGroup);
		return playerIsAdjacent; //the player is allowed to waste the skill if there's no eligible enemy
    }	
}
