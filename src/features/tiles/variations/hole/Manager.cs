using Godot;
using Tiles;

namespace Hole{
    public partial class Manager : Control, Tile, Permeable
    {
        public TileTypes Type => TileTypes.Hole;
    }
}