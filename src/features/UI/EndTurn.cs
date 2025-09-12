using Board;
using Godot;
using System;

public partial class EndTurn : Button
{
	[Export] private Node _turnQueue;

    public override void _Pressed(){
        (_turnQueue as Sequential).AdvanceTurn();
    }

}
