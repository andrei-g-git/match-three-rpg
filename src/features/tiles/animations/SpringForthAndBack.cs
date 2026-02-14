using Animations;
using Board;
using Common;
using Godot;
using System;
using Tiles;

public partial class SpringForthAndBack : Node, Mapable
{
	[Export] private float _duration;
	[Export(PropertyHint.Range, "0, 1, 0.01")] private float _lungeMagnitude;
	[Export] private Control _tileRoot;
	[Export] private Node _animatedActor;
    public Tileable Map {private get;set;} //don't need this
	[Signal] public delegate void FinishesedAttackingEventHandler();

    public void Attack(Control targetTile){
		var source = _tileRoot.Position;
		var target = targetTile.Position;

		var diff = target - source;

		var lungeTo = new Vector2(
			source.X + diff.X * _lungeMagnitude,
			source.Y + diff.Y * _lungeMagnitude			
		);
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);		
		tween.TweenProperty(
			_tileRoot, 
			"position", 
			lungeTo, 
			_duration
		);
			//.AsRelative();	
		tween.TweenProperty(
			_tileRoot, 
			"position", 
			source, 
			_duration
		);
			//.AsRelative();		


		(_animatedActor as Animatable).Play(TileStates.Swing.ToString().ToLower());

		tween.Finished += () => {
			EmitSignal(SignalName.FinishesedAttacking);
			(_animatedActor as Animatable).Stop(TileStates.Swing.ToString().ToLower());
		};
		var bp = 13454;						
	}
}
