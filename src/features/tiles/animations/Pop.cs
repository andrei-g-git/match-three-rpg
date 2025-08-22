using Godot;
using System;
using Tiles;

public partial class Pop : Node, Creatable
{
	[Export] private Node _tileRoot;
	[Export] private float _duration;

    void Creatable.Pop(){
		(_tileRoot as Control).Scale = Vector2.Zero;
		
		var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(_tileRoot, "scale", Vector2.One, _duration);
    }
}
