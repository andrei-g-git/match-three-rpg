using Godot;
using System;
using Tiles;

public partial class Ai : Node, AI
{
	[Signal] public delegate void StartedSearchingEventHandler();


	public void Resume(){
		EmitSignal(SignalName.StartedSearching);
	}

    // public void ConnectStartedSearching(Action action){
    //     Connect(SignalName.StartedSearching, Callable.From(action));
    // }
}
