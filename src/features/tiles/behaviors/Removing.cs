using Godot;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class Removing : Node, Removable
{
	[Export] private Node _tileRoot;
	[Signal] public delegate void DestroyingEventHandler(); 
    [Signal] public delegate void RemovedEventHandler();
    public void PrepDestroy(){
        EmitSignal(SignalName.Destroying);
    }

    public void Remove(){
        EmitSignal(SignalName.Removed);        
		_tileRoot.QueueFree();

    }

    public async Task WaitForRemoved(){
        await ToSignal(this, SignalName.Removed);
        var bp = 123;
    }
}
