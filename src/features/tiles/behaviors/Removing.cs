using Godot;
using System;
using Tiles;

public partial class Removing : Node, Removable
{
	[Signal] public delegate void DestroyingEventHandler();
    public void PrepDestroy(){
        EmitSignal(SignalName.Destroying);
    }

    public void Remove(){
		QueueFree();
    }
}
