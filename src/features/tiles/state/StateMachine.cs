using Godot;
using Common;
using System.Collections.Generic;
using Tiles;

public partial class StateMachine : Node
{
	[Export] private Node _initialState;
	private Stateful currentState;
	private Dictionary<string, Stateful> states = [];
	public override void _Ready(){
		foreach(var child in GetChildren()){
			if(child is Stateful tileState){
				states[child.Name.ToString()] = tileState;
				//tileState.ConnectStateChanged(OnChildStateChanged);
			}
		}
		(_initialState as Stateful).Enter();
		currentState = _initialState as Stateful;
	}

	public void OnChildStateChanged(Node state, /* TileStates */ string newStateName){ //make interface
		if(state != currentState){ //this makes no sense, it should be == ...
			return;
		}
		var newState = states[newStateName/* .ToString() */];
		if(newState == null){
			return;
		}
		if(currentState != null){
			currentState.Exit();
		}
		newState.Enter();
		currentState = newState;
	}

	// public void AddState(Node state){ //I dunno, it doesn't seem like such a hot idea, but the alternative is for actors to have 100 animation states for different skills...
	// 	AddChild(state);
	// 	if(state is TileState tileState){
	// 		states[state.Name.ToString()] = tileState;
	// 		tileState.ConnectStateChanged(OnChildStateChanged);
	// 	}		
	// }
}
