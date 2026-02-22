using Board;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Tiles;

public partial class StraightCharge : Node, Traversing, AccessableBoard, WithTileRoot
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
			var tileAhead = (Board as Queriable).GetItemAt(nextCellAtEnd);
			if (tileAhead is Disposition actor && actor.IsEnemy) {

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

				EmitSignal(SignalName.Attacking, actor as Control, nextCellAtEnd); 
			}
			else{
				await (Board as BoardModel).TransferTileToAsync(TileRoot, path[^1]);	
				EmitSignal(SignalName.FinishedPath);				
			}			
		}
	}

	// public async Task ProcessPathAsync_old(List<Vector2I> path){ //should check that path is not empty	
	// 	var foundOneTarget = false;
	
	// 	for(int i=0; i<path.Count; i++){
	// 		var cellAhead = i < path.Count-1 ? path[i+1] : Hex.FindNextInLine(path);
	// 		if(cellAhead.X >= 0){
	// 			var tileAhead = (Board as Queriable).GetItemAt(cellAhead);
	// 			if (tileAhead is Disposition actor && actor.IsEnemy) {

	// 				await (Board as BoardModel).TransferTileToAsync(TileRoot, path[i]);

	// 				EmitSignal(SignalName.FinishedPath);

	// 				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
	// 				playback.Travel("Swing");

	// 				// wait until it actually enters Swing
	// 				while (playback.GetCurrentNode() != "Swing"){
	// 					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
	// 				}

	// 				// var sw = new Stopwatch();
	// 				// sw.Start();
	// 				while (playback.GetCurrentNode() == "Swing") {
    // 					await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame); 
	// 				}

	// 				// sw.Stop();
	// 				// GD.Print($"TIME ELAPSED:   {sw.Elapsed}");

	// 				EmitSignal(SignalName.Attacking, actor as Control, cellAhead); 

	// 				foundOneTarget = true; 
	// 				break;
	// 			}				
	// 		}
	// 	}

	// 	if (!foundOneTarget){
	// 		await (Board as BoardModel).TransferTileToAsync(TileRoot, path[^1]);	
	// 		EmitSignal(SignalName.FinishedPath);			
	// 	}
	// }

}
