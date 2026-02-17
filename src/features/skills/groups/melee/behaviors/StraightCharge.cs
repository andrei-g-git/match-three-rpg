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

	public async Task ProcessPathAsync(List<Vector2I> path){ //should check that path is not empty	
		var foundOneTarget = false;

		for(int i=0; i<path.Count; i++){
			var cellAhead = i < path.Count-1 ? path[i+1] : Hex.FindNextInLine(path);
			if(cellAhead.X >= 0){
				var tileAhead = (Board as Queriable).GetItemAt(cellAhead);
				if (tileAhead is Disposition actor && actor.IsEnemy) {
					EmitSignal(SignalName.Attacking, actor as Control, cellAhead);   

					//(Board as Organizable).TransferTileTo(TileRoot, path[i]);  

					await (Board as BoardModel).TransferTileToAsync(TileRoot, path[i]);

					

					///AnimationTree.Set("parameters/conditions/swing", true);

				
					var animationPlayerPath = AnimationTree.AnimPlayer;
					var animationPlayer = AnimationTree.GetNode(animationPlayerPath) as AnimationPlayer;

					animationPlayer.AnimationFinished += (StringName animationName) =>{
						//GD.Print("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");	
						AnimationTree.Set("parameters/conditions/swing", false); 
						EmitSignal(SignalName.FinishedPath); 
						
						//GD.Print("cccccccccccccccccccccccccccccccccccc");						
					};

					//await Task.Delay(100);
					await ToSignal(GetTree(), "process_frame");
					AnimationTree.Set("parameters/conditions/swing", true);

					foundOneTarget = true; 
					break;
				}				
			}
		}

		if (!foundOneTarget){
			await (Board as BoardModel).TransferTileToAsync(TileRoot, path[^1]);	
			EmitSignal(SignalName.FinishedPath);			
		}


		//I'll need to wait for the animation to finish somehow
	}

	private Task _RunAnimationToCompletion(AnimationTree animationTree){
		var task = new TaskCompletionSource<bool>();

		var animationPlayerPath = animationTree.AnimPlayer;
		var animationPlayer = animationTree.GetNode(animationPlayerPath) as AnimationPlayer;
		//var animationName = animationPlayer.CurrentAnimation;

		animationPlayer.AnimationFinished += (StringName animationName) =>{
			task.SetResult(true);
		};
		return task.Task;
	}
}
