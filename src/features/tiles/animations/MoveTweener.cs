using Board;
using Common;
using Godot;
using System;
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

}
