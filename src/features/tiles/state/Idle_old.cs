using Godot;
using Common;
using Tiles;

public partial class Idle_old : Node, Stateful
{
	[Export] private AnimatedSprite2D _animatedSprite;
    [Signal] public delegate void StateChangedEventHandler(Node emittingState, TileStates newState);
	
    public void Enter(){
        _animatedSprite.Play(TileStates.Idle.ToString());
    }

    public void Exit(){
        
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

}
