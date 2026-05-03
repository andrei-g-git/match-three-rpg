using Board;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tiles;

public partial class StraightCharge : Node, Traversing, AccessableBoard, WithTileRoot//, FilterableSkill
{
	public Node Board { get; set; }
	public Control TileRoot {get; set;}
	public AnimationTree AnimationTree{private get; set;} //I need to actually set this
	//private int _pathIndex = 0;
	//private List<Control> _alreadyHit = new List<Control>(); //probably don't need
	[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);
	//[Signal] public delegate void FinishedTransferingEventHandler();
	[Signal] public delegate void FinishedPathEventHandler();


	public void ProcessPath(List<Vector2I> path){} 

	public async Task ProcessPathAsync(List<Vector2I> path){
		var nextCellAtEnd = Hex.FindNextInLine(path);
		if(nextCellAtEnd.X >= 0 && nextCellAtEnd.Y >= 0){
			//var tileAhead = (Board as Queriable).GetItemAt(nextCellAtEnd);
			//if (tileAhead is Disposition actor && actor.IsEnemy) {

				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

				playback.Travel("Dash");

				await (Board as BoardModel).TransferTileToAsync(TileRoot, path[^1]);

				EmitSignal(SignalName.FinishedPath);

				
				playback.Travel("Swing");

				// wait until it actually enters Swing
				while (playback.GetCurrentNode() != "Swing"){
					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				}

				while (playback.GetCurrentNode() == "Swing") {
					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame); 
				}

				var tileAhead = (Board as Queriable).GetItemAt(nextCellAtEnd);
				if (tileAhead is Disposition actor && actor.IsEnemy) {
					EmitSignal(SignalName.Attacking, actor as Control, nextCellAtEnd); 
				}
			// }
			// else{
			// 	await (Board as BoardModel).TransferTileToAsync(TileRoot, path[^1]);	
			// 	EmitSignal(SignalName.FinishedPath);				
			// }

			(Board as Organizable).RelocateTile(TileRoot, path.Last());			
		}
	}

    // public static bool CheckIfUsable(List<Vector2I> matchedGroup, SkillNames.SkillGroups skillGroup, Queriable boardQuery){
	// 	var playerPosition = boardQuery.GetPlayerPosition();
    //     var playerIsAdjacent = boardQuery.IsCellAdjacentToLine(playerPosition, matchedGroup);
	// 	return playerIsAdjacent; //the player is allowed to waste the skill if there's no eligible enemy
    // }

}
