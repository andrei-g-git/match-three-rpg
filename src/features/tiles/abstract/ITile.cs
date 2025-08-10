using Godot;

namespace Tiles{
    public interface Tile{
        public TileTypes Type{get;}

    }

    public interface Movable{
        public void MoveTo(Vector2I target);
    }
}