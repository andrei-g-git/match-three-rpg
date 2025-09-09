using Godot;
using System;
using Tiles;

public partial class Turn : Node, TurnBased
{

	[Signal] public delegate void RequestedTurnEndEventHandler();
	[Signal] public delegate void RequestedTurnStartEventHandler();


    public void BeginTurn(){
        EmitSignal(SignalName.RequestedTurnStart);
    }

    public void EndTurn(){
        EmitSignal(SignalName.RequestedTurnEnd);
    }

    public void ConnectRequestedTurnEnd(Action action){ //not interface methods
        Connect(SignalName.RequestedTurnEnd, Callable.From(action));
    }

    public void ConnectRequestedTurnStart(Action action){
        Connect(SignalName.RequestedTurnStart, Callable.From(action));
    }	
}
