using Godot;
using System;
using Tiles;

public partial class Ai : Node, AI
{
	[Export] private Node _pieceRoot; ///for debug
	[Signal] public delegate void StartedSearchingEventHandler();


	public void Resume(){
		GD.Print($"{_pieceRoot.Name} with index {(_pieceRoot as Agentive).Index} has switched ON");
		EmitSignal(SignalName.StartedSearching);
	}

    // public void ConnectStartedSearching(Action action){
    //     Connect(SignalName.StartedSearching, Callable.From(action));
    // }
}
