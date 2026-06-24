using Godot;
using Tiles;

namespace Hole{
    public partial class Manager : Control, Tile, Permeable, FatalArea
    {
        public TileTypes Type => TileTypes.Hole;
    }
}