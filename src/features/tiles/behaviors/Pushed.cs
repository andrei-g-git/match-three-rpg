using Godot;
using Stats;
using System;
using Tiles;

public partial class Pushed : Node, Pushable
{
	[Export] private Control _tileRoot;
	[Export] private Node _stats;
	[Signal] public delegate void GotShovedEventHandler(Vector2I toCell, int movementForce);
    public void GetPushed(Vector2I toCell, int enemyStrength){
        var strength = (_stats as WithStrength).Strength;
		if(enemyStrength > (strength * 3 / 4)){
			EmitSignal(SignalName.GotShoved, toCell, enemyStrength);
		}
    }

}
