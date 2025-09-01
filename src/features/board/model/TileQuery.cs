using Board;
using Common;
using Godot;
using System;
using System.Collections.Generic;
using Tiles;

public partial class TileQuery : Node, Queriable, Mapable, WithTiles
{
	
    public Grid<Control> Tiles {get; set;}
    public Tileable Map {private get;set;}

    private List<Vector2I> _GetNeighboringCells(Vector2I center){
		return [..(Map as TileMapLayer).GetSurroundingCells(center)];
	}
	public List<Control> GetNeighboringTiles(Vector2I center){
		var neighboringCells = _GetNeighboringCells(center);
		var neighboringTiles = new List<Control>();
		foreach(var cell in neighboringCells){
			if(cell.X >= 0 && cell.Y >= 0 && cell.X < Tiles.Width && cell.Y < Tiles.Height){
				neighboringTiles.Add(Tiles.GetItem(cell.X, cell.Y));
			}
		}
		return neighboringTiles;
	}

	public List<Control> GetAllActors(){
		var actors = new List<Control>();
		for(int x=0; x<Tiles.Width; x++){
			for(int y=0; y<Tiles.Height; y++){
				if(Tiles.GetItem(x, y) is Agentive actor){
					actors.Add(actor as Control);
				}
			}
		}
		return actors;
	}	

	public Control FindNextTileInLine(List<Vector2I> line){
		var next = Hex.FindNextInLine(line);
		if(
			next.X >= 0 &&
			next.Y >= 0 &&
			next.X < Tiles.Width &&
			next.Y < Tiles.Height
		){
			return Tiles.GetItem(next.X, next.Y);				
		}
		return null;
	}


	public bool IsCellAdjacentToLine(Vector2I cell, List<Vector2I> line){
		for(int i = 0; i < line.Count; i++){
			var neighbors = _GetNeighboringCells(line[i]);
			foreach(var neighbor in neighbors){
				if(neighbor == cell){
					return true;
				}
			}	
		}	
		return false;		
	}		
}
