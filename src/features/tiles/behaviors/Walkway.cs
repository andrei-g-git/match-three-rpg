using Board;
using Godot;
using System;
using Tiles;

public partial class Walkway : Node, Walkable, AccessableBoard
{
	[Export] Node _tileRoot;
    public Node Board { private get; set;}
	[Signal] public delegate void TransferingEventHandler();
	[Signal] public delegate void TrySwappingEventHandler(Control sourceTile);
	public override void _Ready(){}

	public void LeadPlayer(Control tile){
		if(tile is Playable player){
			GD.Print("transfering player over walkway");
			//player.receiveWalkPoint() or player.spendStamina() or something
			(Board as Organizable).TransferTileToTile(tile, _tileRoot as Control);
			EmitSignal(SignalName.Transfering);
		}else{
			EmitSignal(SignalName.TrySwapping, tile);
		}	
	}
}
