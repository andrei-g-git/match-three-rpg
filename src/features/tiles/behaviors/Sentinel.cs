using Board;
using Common;
using Godot;
using Godot.Collections;
using System;
using Tiles;

public partial class Sentinel : Node, Vigilance, Mapable, WithTiles
{
	[Export] private int _watchRadius;
	[Export] private Control _tileRoot;
	public Tileable Map{private get; set;}
	public Grid<Control> Tiles{get; set;}
	[Signal] public delegate void FoundPlayerEventHandler(Vector2I coordinates); 


    public void StandWatch(){
		var ownCoordinates = Tiles.GetCellFor(_tileRoot);
        var watchedCells = Map.GetCellsInRadius(ownCoordinates, _watchRadius);
		foreach(var cell in watchedCells){
			var tileNode = Tiles.GetItem(cell.X, cell.Y);
			if(tileNode is Playable player){
				EmitSignal(SignalName.FoundPlayer, cell);
				GD.Print("found player at   ", cell);
			}
		}
    }
}
