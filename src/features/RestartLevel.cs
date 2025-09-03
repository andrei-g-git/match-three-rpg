using Godot;
using System;

public partial class RestartLevel : Button
{
	[Export] private Node _boardManager;

    public override void _Pressed(){
		(_boardManager as BoardManager).InitializeLevel(); //BOardManager is an implementation
    }

}
