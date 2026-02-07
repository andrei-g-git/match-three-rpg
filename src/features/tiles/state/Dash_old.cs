using Godot;
using Common;
using Tiles;

public partial class Dash_old : Node, Stateful
{
	[Export] private AnimatedSprite2D _animatedSprite;	
	[Export] private TileStates _animation;
	[Export] private float _duration; //this is really bad, since the tweener has this too, but maybe better than coupling it with the tweener
    [Signal] public delegate void StateChangedEventHandler(Node emittingState, TileStates newState);
	
    public void Enter(){
        //_animatedSprite.Play(TileStates.Dash.ToString());
		_PlaySpriteAnimation(_animatedSprite, _animation, _duration);
    }

    public void Exit(){
        _animatedSprite.Play(TileStates.Idle.ToString());
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

	private void _PlaySpriteAnimation(AnimatedSprite2D sprite, TileStates animation, float duration){ //this kind of works in theory but the true length of the movement has extra time given by the matcher/organizer I THINK, so the animation ends too soon at only 0.5s...
		var animationName = animation.ToString();
		var frames = sprite.SpriteFrames.GetFrameCount(animationName);
		var fps = frames / duration; 
		//sprite.SpriteFrames.SetAnimationSpeed(animationName, fps);
		sprite.Play(animationName, fps);
	}

}