using Board;
using Godot;
using Stats;
using System;
using Tiles;

public partial class Pushed : Node, Pushable//, AccessableBoard
{
	[Export] private Control _tileRoot;
	[Export] private Node _stats;
	[Export] private Node _defend;
    //public Node Board { private get; set; } //just get it from the root for now ...
    [Signal] public delegate void GotShovedEventHandler(Vector2I toCell, int movementForce);

    public void GetPushed(Vector2I toCell, int enemyStrength){
        var strength = (_stats as WithStrength).Strength;
		var effectiveStrength = (float) (strength * 3 / 4);
		if(enemyStrength > effectiveStrength){
			var pushForce = enemyStrength - Math.Ceiling(effectiveStrength);
			EmitSignal(SignalName.GotShoved, toCell, pushForce);
		}
    }

    public void InteractWithObstacle( Vector2I atCell, float movementForce){
		//TODO:
		// if(pieceToSwap is Destroyable){
		//     MoveBySwapping
		//     destroy unswappable but destroyable piece eg. table
		//      MetObstacle - take damage, maybe reduced -- decrease movement force
		// } else {
		(_defend as Defensible).TakeDamage((int)Math.Ceiling(movementForce * 3));
    }
}
