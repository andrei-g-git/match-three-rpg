using System.Collections.Generic;
using Godot;
using Stats;

namespace Tiles{
    public interface Tile{
        public TileTypes Type{get;}

    }

    public interface Movable{
        public void MoveTo(Vector2I target);
        public void MoveOnPath(Stack<Vector2I> path);
    }

    public interface DisappearingTile{
        public void Disappear();
    }

    public interface Creatable{
        public void Pop();
    }

    public interface Collapsable{}

    public interface Empty{}

    public interface Immobile{}

    public interface Environmental{}

    public interface Agentive{}

    public interface Permeable{}

    public interface Playable{}

    public interface WithAttributes{
        public Attributive Attributes{set;}
    }

    public interface Recoiling{
        public void Recoil();
    }
}