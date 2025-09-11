using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using Tiles;

namespace Board{
	public interface Tileable{
		public Vector2I CellToPosition(Vector2I cell);
		public Vector2I PositionToCell(Vector2 position);
		public Array<Vector2I> GetCellsInRadius(Vector2I center, int radiusInCells); 
	}

	public interface Viewable{
		public void Initialize(Grid<Control> tiles);
		public void UpdatePositions(Grid<Control> tiles);
		public void PlaceNew(Control newTile, Control oldTile, Vector2I cell);		
		public void Add(Control tile, Vector2I cell);
	}

	public interface Organizable{
		public void Initialize(Grid<TileTypes> tileTypes);
		//public Grid<Control> Tiles{get;set;}	
		public Task TransferTileToTile(Control sourceTile, Control targetTile);
		public /* Task */ void TransferTileTo(Control tile, Vector2I target);
		public void RelocateTile(Control tile, Vector2I target);

	}

	public interface AccessableBoard{
		public Node Board {set;}
	}

	public interface MatchableBoard{
		//public bool TryMatching(Control sourceTile, Control targetTile); 
		public Task<bool> TryMatching(Control sourceTile, Control targetTile); 
		// public void MatchWithoutSwapping();
		// public void CollapseGridAndCheckNewMatches();
		public Task MatchWithoutSwapping();
		public Task CollapseGridAndCheckNewMatches();
	}

	public interface WithTiles{
		public Grid<Control> Tiles{get;set;}		
	}

	public interface Queriable{
		public List<Control> GetNeighboringTiles(Vector2I center);
		public List<Control> GetAllActors();
		public Control FindNextTileInLine(List<Vector2I> line);
		public bool IsCellAdjacentToLine(Vector2I cell, List<Vector2I> line);
		public List<Vector2I> GetCellsInRadiusAroundTileNode(int radius, Control tileAtCenter);
		public Control GetItemAt(Vector2I cell);
	}

	public interface Sequential{
		public Control CurrentActor{get;}
		public void AdvanceTurn();
		public bool IsPlayerTurn();
		public void AddActor(Control actor);		
	}

}