using Godot;

namespace Tiles{
    public interface Swappable{
        public void SwapWith(Control tile);
    }    

    public interface Removable{
        public void PrepDestroy();
        public void Remove();
    }
}