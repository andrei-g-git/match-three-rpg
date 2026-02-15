using Animations;
using Godot;
using System;

public partial class DamageNumber : Node2D, DisplayableNumber
{
	[Export] private Label label;
	[Export] private AnimationPlayer animationPlayer;
	[Export] private float spread;
	[Export] private float height;


	public void DisplayNumberAt(Vector2I position, int value){
		label.Text = value.ToString();

		animationPlayer.Play("FadeScaleInOut");
		var tween = CreateTween();
		var endPosition = new Vector2(
			new Random().Next((int)-spread, (int)spread),
			-height
		) + position;
		var duration = animationPlayer.GetAnimation("FadeScaleInOut").Length;

		var pixelStart = MathUtils.InvertVector(position * 48);
		var pixelEnd = MathUtils.InvertVector(endPosition * 48);
		tween.TweenProperty(
			this,
			"position",
			pixelStart,
			duration
		)
			.From(pixelEnd);
	}

	public void DisplayNumber(int value){ //DRY
		label.Text = value.ToString();

		animationPlayer.Play("FadeScaleInOut");
		var tween = CreateTween();
		var endPosition = new Vector2(
			new Random().Next((int)-spread, (int)spread),
			-height
		) + Position;
		var duration = animationPlayer.GetAnimation("FadeScaleInOut").Length;

		// var pixelStart = MathUtils.InvertVector(Position * 48);
		// var pixelEnd = MathUtils.InvertVector(endPosition * 48);
		tween.TweenProperty(
			this,
			"position",
			Position * 48,//pixelStart,
			duration
		)
			.From(endPosition * 48/* pixelEnd */);
	}	
}
