using System.Collections.Generic;
using Godot;

namespace Tiles{
    public interface Swappable{
        public void SwapWith(Control tile);
    }    

    public interface Removable{
        public void PrepDestroy();
        public void Remove();
    }

    public interface Matchable{
        public void BeginPostMatchProcessDependingOnPlayerPosition(Vector2I ownPosition, Node playerTile, bool playerAjacent);
    }

    public interface MatchableBounds{
        public int MatchRange{get;}
        public bool IsMatchGroupInRange(Queue<List<Vector2I>> matchGroupQueue, Grid<Control> board); 
    }
}