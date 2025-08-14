using Board;
using Common;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;

public partial class MoveTweener : Node, Movable, Mapable
{
	[Export] private Node _tileRoot;
	[Export] private float _duration;
    public Tileable Map { private get; set; }
	[Signal] public delegate void FinishedMovingEventHandler();

	public void MoveTo(Vector2I target){
		var pixelTarget = Map.CellToPosition(target);
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		tween.TweenProperty(_tileRoot, "position", (Vector2) pixelTarget, _duration);
		
		tween.Finished += () => EmitSignal(SignalName.FinishedMoving);
	}	

	public void MoveOnPath(Stack<Vector2I> path){
		var bp = 123;
		GD.Print("move on path:  ");
		GD.Print( path.Select(item => item.ToString()).ToArray()); 
		if(path.Count>0){
			var target = path.Pop();
			var pixelTarget = Map.CellToPosition(target);
			Tween tween = CreateTween()
				.SetTrans(Tween.TransitionType.Linear);
				//.SetEase(Tween.EaseType.Out);

			tween.TweenProperty(_tileRoot, "position", (Vector2) pixelTarget, _duration);
			
			tween.Finished += () => {
				if(path.Count>0){
					MoveOnPath(path);
				}
			};				
		}else{
			EmitSignal(SignalName.FinishedMoving);
		}
		bp = 122;
	}	

}
