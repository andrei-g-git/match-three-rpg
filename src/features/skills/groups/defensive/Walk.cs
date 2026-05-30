using Board;
using Common;
using Godot;
using Levels;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class Walk : Control, Skill, WithTileRoot, AccessableBoard, WithAnimationTree, Traversing, FilterableSkill, WithRoomModifiers
{
    public Node Board { private get; set; }
    public Control TileRoot { get; set; }
    public AnimationTree AnimationTree { private get; set; }
	public List<string> RoomModifiers{private get; set;}
	[Signal] public delegate void FinishedPathEventHandler();

    public async Task ProcessPathAsync(List<Vector2I> path){
		var playerCell = (Board as Queriable).GetCellFor(TileRoot);
		var isAdjacent = (Board as Queriable).IsCellAdjacentToLine(playerCell, path);
		if (isAdjacent){
			var index = _ProcessEffectMagnitudeFromVerticalityModifier();

			var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

			playback.Travel("Dash");

			await (Board as BoardModel).TransferTileToAsync(TileRoot, path[index]);

			EmitSignal(SignalName.FinishedPath);	

			(Board as Organizable).RelocateTile(TileRoot, path[index]);		
		}else{
			//gain energy
		}
	}

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

    public bool CheckIfUsable(List<Vector2I> matchedGroup, SkillNames.SkillGroups skillGroup, Queriable boardQuery){
		var playerPosition = boardQuery.GetPlayerPosition();
        var playerIsAdjacent = boardQuery.IsCellAdjacentToLine(playerPosition, matchedGroup);
		return playerIsAdjacent; 
    }

	private int _ProcessEffectMagnitudeFromVerticalityModifier(){
		var pathIndexToStopAt = RoomModifiers.FindAll(mod => mod == LevelModifiers.vertical_match_multiplier.ToString()).Count;
		return pathIndexToStopAt;
	}
}
