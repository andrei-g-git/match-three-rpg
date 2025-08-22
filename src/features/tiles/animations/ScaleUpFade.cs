using Godot;
using System;
using Tiles;

public partial class ScaleUpFade : Node, DisappearingTile
{
	[Export] private Node _tileRoot;
	[Export] private float _duration;

	[Signal] public delegate void FinishedFadingEventHandler(); //this might cause problems since the fade tween has it, but the two should not be used in conjunction anyway...

	public void Disappear(){
		var tween = CreateTween()
			.SetParallel()
			.SetTrans(Tween.TransitionType.Circ)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(_tileRoot, "scale", new Vector2(1.5f, 1.5f), _duration);			
		tween.TweenProperty(_tileRoot, "modulate:a", 0f, _duration);	



		tween.Finished += () => EmitSignal(SignalName.FinishedFading);
	}
}
