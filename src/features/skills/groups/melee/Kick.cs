using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class Kick : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree
{
    public Control TileRoot { get; set; }
    public Node Board { private get; set; }
    public AnimationTree AnimationTree { private get; set; }

	[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);



    public async Task ProcessPathAsync(List<Vector2I> path){
		//matches pieces at the direct back of the player, who then kicks forward if enemy RIGHT in front, on a line with player and matches eg. m,m,m,p,f
		//received path direction is away from player as always, need to reverse 
		path.Reverse();
        var nextCellAtEnd = Hex.FindNextInLine(path);
		var playerCell = (Board as Queriable).GetCellFor(TileRoot);
		if(nextCellAtEnd.Equals(playerCell)){
			path.Add(playerCell);
			nextCellAtEnd = Hex.FindNextInLine(path);
			var nextPiece = (Board as Queriable).GetItemAt(nextCellAtEnd);
			if(nextPiece is Disposition actor && actor.IsEnemy){
				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

				playback.Travel("Kick");

				while (playback.GetCurrentNode() != "Swing"){
					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				}

				while (playback.GetCurrentNode() == "Swing") {
					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame); 
				}
				//this is stupid but it might just be the best option -- it waits for the next animation, which should be Idle
				while (playback.GetCurrentNode() != "Swing"){
					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				}	
				
							
			}
		}

    }

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }
}
