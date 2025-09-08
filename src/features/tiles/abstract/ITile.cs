using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Stats;

namespace Tiles{
    public interface Tile{
        public TileTypes Type{get;}

    }

    public interface Movable{
        public void MoveTo(Vector2I target);
        public void MoveOnPath(Stack<Vector2I> path);
        public Task WaitUntilMoved();
    }

    public interface DisappearingTile{
        public void Disappear();
    }

    public interface Creatable{
        public void Pop();
        public Task WaitUntilCreated();
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

    public interface OfPlayer{
        public Playable Player{get;set;}
    }
    public interface WithTileRoot{
        public Control TileRoot{get;set;}
    }

    public interface WhiteFlashable{
        public void FlashOnce();
    }
}