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

	}

	public interface AccessableBoard{
		public Node Board {set;}
	}

	public interface MatchableBoard{
		public bool TryMatching(Control sourceTile, Control targetTile); 
	}

	public interface WithTiles{
		public Grid<Control> Tiles{get;set;}		
	}
}