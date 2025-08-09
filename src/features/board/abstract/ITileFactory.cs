using Godot;
using Tiles;

namespace Board{
    public interface TileMaking{
		public Node Create(TileTypes type);
		public void Initialize();
	}
}