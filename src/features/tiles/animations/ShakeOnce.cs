using Godot;
using System;
using Tiles;

public partial class ShakeOnce : Node, Recoiling
{
	[Export] private Control _tileRoot;
	[Export] private float _duration;
	[Export] private int _maxShakes;
	[Export(PropertyHint.Range, "0, 1, 0.01")] private float _shakeMagnitude;
	private int _shakes;
	private Vector2 _originalPosition;
	[Signal] public delegate void RecoiledEventHandler();

	public void Recoil(){
		var size = _tileRoot.Size;
		var shakeDistanceX = size.X * _shakeMagnitude;
		var shakeDistanceY = size.Y * _shakeMagnitude;

		_originalPosition = _tileRoot.Position;
		_shakes = _maxShakes;

		_ShakeOnce((int)shakeDistanceX, (int)shakeDistanceY, _duration/_shakes);
	
		EmitSignal(SignalName.Recoiled);
	}


	private void _ShakeOnce(int shakeDistanceX, int shakeDistanceY, float duration){
		var random = new Random();
		var xShake = random.Next(-shakeDistanceX, shakeDistanceX);
		var yShake = random.Next(-shakeDistanceY, shakeDistanceY);
        var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(
            _tileRoot,
            "position",
            new Vector2(xShake, yShake),
            duration
        )
            .AsRelative();	
        tween.TweenProperty(
            _tileRoot,
            "position",
            _originalPosition,
            duration
        );	
		_shakes--;
		tween.Finished += () => {
			if(_shakes > 0){
				_ShakeOnce(shakeDistanceX, shakeDistanceY, duration);
			}
		};			
	}


	// private void _Shake(int shakeDistanceX, int shakeDistanceY, float duration){
	// 	var random = new Random();
	// 	var xShake = random.Next(-shakeDistanceX, shakeDistanceX);
	// 	var yShake = random.Next(-shakeDistanceY, shakeDistanceY);
    //     var tween = CreateTween()
	// 		.SetTrans(Tween.TransitionType.Elastic)
	// 		.SetEase(Tween.EaseType.Out);
    //     tween.TweenProperty(
    //         _tileRoot,
    //         "position",
    //         new Vector2(xShake, yShake),
    //         duration
    //     )
    //         .AsRelative();
	// 	_shakes--;
	// 	tween.Finished += () => {
	// 		if(_shakes > 0){
	// 			_Shake(shakeDistanceX, shakeDistanceY, duration);
	// 		}else{
	// 			_GoBackToOriginalPosition(0.1f);
	// 		}
	// 	};
	// }	

	// private void _GoBackToOriginalPosition(float duration){
	// 	// var x = _tileRoot.Position.X;
	// 	// var y = _tileRoot.Position.Y;
    //     var tween = CreateTween()
	// 		.SetTrans(Tween.TransitionType.Spring)
	// 		.SetEase(Tween.EaseType.Out);	
    //     tween.TweenProperty(
    //         _tileRoot,
    //         "position",
    //         _originalPosition,//new Vector2(x, y)
    //         duration
    //     );				
	// }
}
