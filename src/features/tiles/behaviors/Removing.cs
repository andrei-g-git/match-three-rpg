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
		_tileRoot.QueueFree(); //this might pose a problem if I need to do things in the board model with it ...

    }

    public async Task WaitForRemoved(){
        await ToSignal(this, SignalName.Removed);
        var bp = 123;
    }
}
