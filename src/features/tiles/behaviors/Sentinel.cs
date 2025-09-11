using Board;
using Common;
using Godot;
using Godot.Collections;
using System;
using Tiles;

public partial class Sentinel : Node, Vigilance, Mapable, AccessableBoard//, WithTiles
{
	[Export] private int _watchRadius;
	[Export] private Control _tileRoot;
	public Tileable Map{private get; set;}
	//private Grid<Control> _tiles;
	// public Grid<Control> Tiles{
	// 	get => _tiles; 
	// 	set{
	// 		_tiles = value;
	// 		var bp = 123;
	// }}
	public Node Board {private get; set;}	
	[Signal] public delegate void FoundPlayerEventHandler(Vector2I coordinates); 


    public void StandWatch(){
		// var ownCoordinates = Tiles.GetCellFor(_tileRoot); //I have no idea why but Tiles changes it's contents by the time the code gets here (it's fine when I set Tiles, but not by this breakpoint)
		// 	//it's filled with Variation.Manager instances as opposed to <Control34546> type instances
        // var watchedCells = Map.GetCellsInRadius(ownCoordinates, _watchRadius);
		var watchedCells = (Board as Queriable).GetCellsInRadiusAroundTileNode(_watchRadius, _tileRoot);
		foreach(var cell in watchedCells){
			//var tileNode = Tiles.GetItem(cell.X, cell.Y);
			var tileNode = (Board as Queriable).GetItemAt(cell);
			if(tileNode is Playable player){
				EmitSignal(SignalName.FoundPlayer, cell);
				GD.Print("found player at   ", cell);
			}
		}
    }
}
