using Animations;
using Godot;
using System;
using Tiles;

public partial class AnimatedActor : Node2D, Animatable
{
	[Export] private AnimationPlayer _animationPlayer;

	public void Play(StringName animationName){
		//should check that the name is a string for the TileStates enum
		_animationPlayer.Play(animationName);
	}
}
