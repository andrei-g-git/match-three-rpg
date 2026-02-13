using Animations;
using Board;
using Common;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;

public partial class MoveTweener : Node, Movable, Mapable
{
	[Export] private Node _tileRoot;
	[Export] private float _duration;
	//[Export] private Node _dashState;
	[Export] private Node _animatedActor;
	//[Export] private AnimatedSprite2D _sprite;
	//[Export] private TileStates _animation;
    public Tileable Map { private get; set; }
	[Signal] public delegate void FinishedMovingEventHandler();

	public void MoveTo(Vector2I target){
		var ownPosTest = (_tileRoot as Control).Position;
		var pixelTarget = Map.CellToPosition(target);
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		tween.TweenProperty(_tileRoot, "position", (Vector2) pixelTarget, _duration);

		if(_animatedActor !=null){ //because skill tiles currently have this tweener too...
			(_animatedActor as Animatable).Play(TileStates.Dash.ToString().ToLower());
		}
		//(_dashState as Stateful).Enter(); //I'm ditcheing states, I'll just use AnimatedActor Play and Stop interfaces, they're already set up to default back to idle when actions complete 
		
		tween.Finished += () => {
			EmitSignal(SignalName.FinishedMoving); //this finishes before it finishes completely, leaving the animation too short...
		if(_animatedActor !=null){ //because skill tiles currently have this tweener too...
			(_animatedActor as Animatable).Stop(TileStates.Dash.ToString().ToLower());
		}
			//(_dashState as Stateful).Exit();
		};
	}	

	public void MoveOnPath(Stack<Vector2I> path){
		var bp = 123;
		GD.Print("move on path:  ");
		GD.Print( path.Select(item => item.ToString()).ToArray()); 
		if(path.Count>0){
			var target = path.Pop();
			var pixelTarget = Map.CellToPosition(target);
			Tween tween = CreateTween()
				.SetTrans(Tween.TransitionType.Linear);
				//.SetEase(Tween.EaseType.Out);

			tween.TweenProperty(_tileRoot, "position", (Vector2) pixelTarget, _duration);
			
			tween.Finished += () => {
				//if(path.Count>0){
					MoveOnPath(path);
				//}
			};				
		}else{
			EmitSignal(SignalName.FinishedMoving);
		}
		bp = 122;
	}	

	public async Task WaitUntilMoved(){
		var bp = 123;
		await ToSignal(this, SignalName.FinishedMoving);
		bp = 1123;
	}

	//moved to individual states e.g. Dash
	// private void _PlaySpriteAnimation(AnimatedSprite2D sprite, TileStates animation, float duration){
	// 	var animationName = animation.ToString();
	// 	var frames = sprite.SpriteFrames.GetFrameCount(animationName);
	// 	var fps = frames * duration; 
	// 	//sprite.SpriteFrames.SetAnimationSpeed(animationName, fps);
	// 	sprite.Play(animationName, fps);
	// }
}
