using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Godot;
using Stats;

namespace Tiles{
    public interface Tile{
        public TileTypes Type{get;}

    }

    public interface Movable{
        public void MoveTo(Vector2I target);
        public void MoveOnPath(Stack<Vector2I> path);
        public void MoveToEndOfPath(List<Vector2I> path);
        public void MoveOverDistance(Vector2I target, int distance);
         public void MoveOverDistanceDelayed(Vector2I target, int distance, int delayInCells);
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

    public interface Agentive{
        public Sequential TurnQueue{set;}
        public void AdvanceTurn();

        //debug
        public int Index{get;set;}
    }

    public interface Permeable{}

    public interface Playable{}

    public interface WithAttributes{
        public Attributive Attributes{set;}
    }

    public interface Recoiling{
        public void Recoil(int magnitude);
    }

    public interface OfPlayer{
        public Playable Player{get;set;}
    }
    public interface WithTileRoot{
        public Control TileRoot{get;set;}
    }

    public interface WhiteFlashable{
        public void FlashOnce(int magnitude);
    }

    public interface WithAnimatedActor{
        public Node2D AnimatedActor{set;}
    }

    public interface CanSpawn{
        public Task Spawn();
    }
}