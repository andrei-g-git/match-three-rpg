using Godot;
using System;

public partial class FadeTweener : Node
{
	[Export] private Node _tileRoot;
	[Export] private float _duration;

	[Signal] public delegate void FinishedFadingEventHandler();

	public void FadeOut(){
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(_tileRoot, "modulate:a", 0f, _duration);

		tween.Finished += () => EmitSignal(SignalName.FinishedFading);
	}	
}
