using Godot;
using Stats;
using System;
using Tiles;

public partial class Ai : Node, AI
{
	[Export] private Node _activeEffects;
	[Export] private Node _pieceRoot; ///for debug
	[Signal] public delegate void StartedSearchingEventHandler();


	public void Resume(){
		(_activeEffects as Effectful).UpdateDurations();
		var isStunned = (_activeEffects as Effectful).CheckIfStunned();
		if(!isStunned){
			//GD.Print($"{_pieceRoot.Name} with index {(_pieceRoot as Agentive).Index} has switched ON");
			EmitSignal(SignalName.StartedSearching);
		}
		else{
			GD.Print("IT IS STUNNED AND CANNOT ACT THIS TURN");
		}

	}

    // public void ConnectStartedSearching(Action action){
    //     Connect(SignalName.StartedSearching, Callable.From(action));
    // }
}
