using Animations;
using Common;
using Godot;
using Tiles;

public partial class Idle : Node, Stateful
{
	[Export] private Node _animatedActor;
    [Signal] public delegate void StateChangedEventHandler(Node emittingState, TileStates newState);
	
    public void Enter(){
        (_animatedActor as Animatable).Play(TileStates.Idle.ToString());
        EmitSignal(SignalName.StateChanged, this, TileStates.Idle.ToString());
    }

    public void Exit(){
        
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}
