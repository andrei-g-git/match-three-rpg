using Animations;
using Godot;
using System;
using Tiles;

public partial class AnimatedActor : Node2D, Animatable
{
	[Export] private AnimationPlayer _animationPlayer; //might not need
	[Export] private AnimationTree _animationTree;
	private string conditionPath = "parameters/conditions/";
	
	public void Play(StringName animationName){
		if(Enum.TryParse<TileStates>(animationName, true, out var state)){	
			_animationTree.Set(conditionPath + animationName.ToString().ToLower(), true);	
		}
	}

	public void Stop(StringName animationName){
		if(Enum.TryParse<TileStates>(animationName, true, out var state)){	
			_animationTree.Set(conditionPath + animationName.ToString().ToLower(), false);	
		}
	}	
}
