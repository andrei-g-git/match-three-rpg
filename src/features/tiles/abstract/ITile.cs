using System.Collections.Generic;
using Godot;

namespace Tiles{
    public interface Tile{
        public TileTypes Type{get;}

    }

    public interface Movable{
        public void MoveTo(Vector2I target);
        public void MoveOnPath(Stack<Vector2I> path);
    }

    public interface Collapsable{}

    public interface Empty{}
}