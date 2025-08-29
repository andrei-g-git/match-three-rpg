using Board;
using Common;
using Godot;
using System;

public partial class SpringForthAndBack : Node, Mapable
{
	[Export] private float _duration;
	[Export(PropertyHint.Range, "0, 1, 0.01")] private float _lungeMagnitude;
	[Export] private Control _tileRoot;
    public Tileable Map {private get;set;}
	[Signal] public delegate void FinishesedAttackingEventHandler();

    public void Attack(Control targetTile){
		var source = _tileRoot.Position;
		var target = Map.PositionToCell(targetTile.Position);
		var diff = source - target; //or the other way around?

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
		)
			.AsRelative();	
		tween.TweenProperty(
			_tileRoot, 
			"position", 
			source, 
			_duration
		);
			//.AsRelative();		
		tween.Finished += () => {
			EmitSignal(SignalName.FinishesedAttacking);
		};
		var bp = 13454;						
	}
}
