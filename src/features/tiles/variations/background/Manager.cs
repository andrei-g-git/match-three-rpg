using Godot;
using System;
using Tiles;

namespace Background{
    public partial class Manager : Control, Tile, /* Immobile, */Permeable, Empty, Environmental //Empty might cause problems
    {
        public TileTypes Type => TileTypes.Background;

        public TileTypes AA => Type; //for debugging        

        public Vector2I Coordinates { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void _Ready(){
            GD.Print("Empty immobile tile");
        }

    }
}
