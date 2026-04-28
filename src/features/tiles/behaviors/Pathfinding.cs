using Board;
using Common;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;

public partial class Pathfinding : Control, Pathfindable /* they can't all be gold... */, AccessableBoard, Mapable
{
	[Export] private Control _tileRoot; 
	[Export] private Node _astar;
	public Tileable Map{private get; set;}
	//public Grid<Control> Tiles{get; set;}
	public Node Board{private get;set;}

	public List<Vector2I> FindPath(Vector2I target){
		var pixelPosition = _tileRoot.Position;
		var source = ((TileMapLayer) Map).LocalToMap(pixelPosition); 
		var eligibleCellsDebug = new List<Vector2I>();
		eligibleCellsDebug.Add(new Vector2I(-6, -9));
		foreach (var cell in ((TileMapLayer) Map).GetUsedCells()){
			var isNavigable = _CheckIfNavigable(cell); 
			var isSwapable = _CheckIfSwapable(cell); 
			//var isActor = _CheckIfActor(cell);
			//var testName = tiles[cell.X][cell.Y] != null ? tiles[cell.X][cell.Y].Name.ToString() : "nah...";

			if(isNavigable && (isSwapable || /* isActor - no, it makes it attempt to pass through other enemies */(cell == source) || (cell == target))){
				(_astar as AstarHex).AddHexPoint(cell);
				(_astar as AstarHex).ConnectHexPoint(cell);	
				eligibleCellsDebug.Add(cell);			
			}
		}	
		//GD.Print($"there are now {(_astar as AstarHex).GetPointCount()} point connections");
		//GD.Print("&&&&&&& debug eligible cells ^^^^^^^^^^^^^^^^^^^^^  \n :  ", string.Join("", eligibleCellsDebug.Select(cell => $"|{cell.X},{cell.Y}")));

		var path = (_astar as AstarHex).GetPath(source, target);
		var bp = 1123;

		(_astar as AstarHex).Clear(); //otherwise it keeps the points that were only previously walkable, even though that might have changed with actors switching positions

		return path;
	}

	private bool _CheckIfNavigable(Vector2I cell){
		var tileData = ((TileMapLayer) Map).GetCellTileData(cell);
		//new as of april '26
		var isNavigable = (bool) tileData.GetCustomData("navigable");
		//var isSwapable = _CheckIfSwapable(cell);
		//  /
		return isNavigable/*  && isSwapable */; //won't be enough once I add more kinds of tiles
	}

	private bool _CheckIfSwapable(Vector2I cell){
		var x = cell.X;
		var y = cell.Y;
		return (Board as Queriable).GetItemAt(new Vector2I(x, y)) is Swappable;
	}

	private bool _CheckIfActor(Vector2I cell){
		var x = cell.X;
		var y = cell.Y;
		return (Board as Queriable).GetItemAt(new Vector2I(x, y)) is Agentive;
	}

}
