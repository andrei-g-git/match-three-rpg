using Board;
using Common;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
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
		foreach (var cell in ((TileMapLayer) Map).GetUsedCells()){
			var isNavigable = _CheckIfNavigable(cell); 
			var isSwapable = _CheckIfSwapable(cell); 
			var isActor = _CheckIfActor(cell);
			//var testName = tiles[cell.X][cell.Y] != null ? tiles[cell.X][cell.Y].Name.ToString() : "nah...";
			if(isNavigable && (isSwapable || isActor)){
				(_astar as AstarHex).AddHexPoint(cell);
				(_astar as AstarHex).ConnectHexPoint(cell);				
			}
		}	
		return (_astar as AstarHex).GetPath(source, target);
	}

	private bool _CheckIfNavigable(Vector2I cell){
		var tileData = ((TileMapLayer) Map).GetCellTileData(cell);
		return (bool) tileData.GetCustomData("navigable");
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
