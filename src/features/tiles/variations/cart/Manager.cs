using Godot;
using System;
using Tiles;

namespace Cart{
    public partial class Manager : Control, Tile, Permeable// Immobile
    {
        public TileTypes Type => TileTypes.Cart;
        public TileTypes AA => Type; //for debugging
    }

}

