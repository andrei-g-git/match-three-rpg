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
			var index = _ProcessEffectMagnitudeFromVerticalityModifier(path);

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

	private int _ProcessEffectMagnitudeFromVerticalityModifier(List<Vector2I> path){
		var pathIsVerticalMultiplier = _EncodeBooleanForPathVerticality(path);
		var pathIndexToStopAt = RoomModifiers.FindAll(mod => mod == LevelModifiers.vertical_match_multiplier.ToString()).Count;
		pathIndexToStopAt *= pathIsVerticalMultiplier;
		return pathIndexToStopAt;
	}

	private int _EncodeBooleanForPathVerticality(List<Vector2I> path){
		for(int i=0; i<path.Count-1; i++){
			var height = path[i].Y;
			var nextHeight = path[i+1].Y;
			if (height != nextHeight) return 0;
		}	
		return 1;	
	}
}
