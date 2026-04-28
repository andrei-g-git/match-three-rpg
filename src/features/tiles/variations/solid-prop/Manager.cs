using Godot;
using System;
using Tiles;

namespace SolidProp;
public partial class Manager : Control, Tile, Permeable
{
    public TileTypes Type => TileTypes.SolidProp;
	public TileTypes AA => Type; //for debugging
}
