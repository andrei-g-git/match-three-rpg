using Animations;
using Common;
using Godot;
using System;
using Tiles;

public partial class Dash : Node, Stateful
{
	[Export] private Node _animatedActor;
	[Export] private float _duration; //this is really bad, since the tweener has this too, but maybe better than coupling it with the tweener
    [Signal] public delegate void StateChangedEventHandler(Node emittingState, TileStates newState);
	
    public void Enter(){
		_PlaySpriteAnimation(_animatedActor as Animatable, TileStates.Dash, _duration);
		EmitSignal(SignalName.StateChanged, this, TileStates.Dash.ToString());
    }

    public void Exit(){
        (_animatedActor as Animatable).Play(TileStates.Idle.ToString());
		EmitSignal(SignalName.StateChanged, this, TileStates.Idle.ToString());
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

	private void _PlaySpriteAnimation(Animatable animatedActor, TileStates animation, float duration){ //this kind of works in theory but the true length of the movement has extra time given by the matcher/organizer I THINK, so the animation ends too soon at only 0.5s...
			animatedActor.Play(animation.ToString());
		// var animationName = animation.ToString();
		// var frames = sprite.SpriteFrames.GetFrameCount(animationName);
		// var fps = frames / duration; 
		// //sprite.SpriteFrames.SetAnimationSpeed(animationName, fps);
		// sprite.Play(animationName, fps);
	}

}
