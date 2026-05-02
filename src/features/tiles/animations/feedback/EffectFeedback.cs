using Animations;
using Board;
using Godot;
using System;

public partial class EffectFeedback : Node2D, DisplayableEffect
{
	[Export] private Label label;
	[Export] private AnimationPlayer animationPlayer;
	[Export] private float spread;
	[Export] private float height;


	public void DisplayEffect(string effect){ 
		label.Text = effect;

		animationPlayer.Play("FadeScaleInOut");
		var tween = CreateTween();
		var endPosition = new Vector2(
			new Random().Next((int)-spread, (int)spread),
			-height
		) + Position;
		var duration = animationPlayer.GetAnimation("FadeScaleInOut").Length;

		var pixelStart = MathUtils.InvertVector(Position);
		var pixelEnd = MathUtils.InvertVector(endPosition);
		tween.TweenProperty(
			this,
			"position",
			endPosition,
			duration
		)
			.From(Position);
	}	
}

