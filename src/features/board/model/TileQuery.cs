using Board;
using Common;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;

public partial class TileQuery : Node, Queriable, Mapable, WithTiles
{
	
    public Grid<Control> Tiles {get; set;}
    public Tileable Map {private get;set;} //should be an export

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

	public Vector2I GetCellFor(Control tile){
		return Tiles.GetCellFor(tile);
	}

	public Control GetItemAt(Vector2I cell){
		if(cell.X >= 0 && cell.Y >= 0 && cell.X < Tiles.Width && cell.Y < Tiles.Height){
			return Tiles.GetItem(cell);
		}		
		return null;//default;
	}

	public List<Vector2I> GetCellsInRadiusAroundTileNode(int radius, Control tileAtCenter){
		var tileCoordinates = Tiles.GetCellFor(tileAtCenter);
		var watchedCells = Map.GetCellsInRadius(tileCoordinates, radius);
		return [.. watchedCells];
	}

	public List<Vector2I> GetCellsInRadius(int radius, Vector2I cell){
		var watchedCells = Map.GetCellsInRadius(cell, radius);
		return [.. watchedCells];
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

	public List<Vector2I> FindAllTilesOfType(TileTypes type){
		var listGrid = Tiles.GetGridAs2DList();
		var matchingTileCells = new List<Vector2I>();
		for(int x=0; x<listGrid.Count; x++){
			for(int y=0; y<listGrid[0].Count; y++){
				if(listGrid[x][y] is Tile tile && tile.Type == type){
					matchingTileCells.Add(new Vector2I(x, y));
				}
			}
		}
		return matchingTileCells;
	}	

	public Vector2I GetDimensions(){
		return new Vector2I(Tiles.Width, Tiles.Height);
	}	

	public List<Control> GetPiecesInRadius(int radius, Vector2I cell){
		var cells = Map.GetCellsInRadius(cell, radius);
		return cells.Select(cell => Tiles.GetItem(cell)).ToList();
	}

	public List<Control> GetPiecesAroundLine(List<Vector2I> line){
		var surroundingPieces = new List<Control>();
		foreach(var cell in line){
			var neighbouringPieces = GetNeighboringTiles(cell);
			surroundingPieces.AddRange(neighbouringPieces);
		}
		return surroundingPieces.Distinct().ToList();
	}

	public List</* T */Control> GetPiecesAroundLineOfType<T>(List<Vector2I> line/* , Type interfaceType */){
		// if(interfaceType.IsInterface || interfaceType.IsAbstract){
		// 	var surroundingPieces = GetPiecesAroundLine(line);
		// 	return surroundingPieces.Where(piece => interfaceType.IsAssignableFrom(piece.GetType())).ToList();			
		// }
		// GD.Print($"no pieces of type {interfaceType.Name} were found around the matches line");
		// return [];
		var surroundingPieces = GetPiecesAroundLine(line);
		var eligible = new List</* T */Control>();
		foreach(var piece in surroundingPieces){
			if(piece is T t){
				eligible.Add(/* t */piece);
			}
		}
		return eligible;
	}	

	public Control GetClosestPieceToCellInList(Vector2I cell, List<Control> pieces){
		//var distances = pieces.Select(piece => cell.DistanceTo(piece))
		//var distances = new List<float>();
		var shortestDistance = 9999f; //this is dangerous, I should just make a full list and get the smallest value 
		Control closestPiece = null;
		foreach(var piece in pieces){
			var thisCell = GetCellFor(piece);
			var distance = cell.DistanceTo(thisCell);
			if(distance < shortestDistance){
				shortestDistance = distance;
				closestPiece = piece;
			}
		}
		return closestPiece;
	}
}
