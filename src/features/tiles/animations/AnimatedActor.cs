using Animations;
using Godot;
using System;
using Tiles;

public partial class AnimatedActor : Node2D, Animatable, CustomizableGear
{
	[Export] private AnimationPlayer _animationPlayer; //might not need
	[Export] private AnimationTree _animationTree;
	[Export] private Node _gearCustomizer; //enemies might not need this...

	public AnimationTree AnimationTree{get;} //not part of an interface. I should just have this node do all sort of player and tree operations and just pass this and use it's interface
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

    public void ChangeGear(string type, string item){
        (_gearCustomizer as CustomizableGear).ChangeGear(type, item);
    }	
}
