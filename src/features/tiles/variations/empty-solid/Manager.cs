using Godot;
using System;
using Tiles;

namespace EmptySolid{
    public partial class Manager : Control, Tile, Immobile, Empty //Empty might cause problems
    {
        public TileTypes Type => TileTypes.EmptySolid;

        public TileTypes AA => Type; //for debugging        

        public Vector2I Coordinates { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
