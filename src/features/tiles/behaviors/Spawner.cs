using Board;
using Godot;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class Spawner : Node, CanSpawn, AccessableBoard
{
	[Export] private Control _pieceRoot;
	[Export] private TileTypes _pieceToSpawn;
	[Export] private Directions _whereToSpawn;

    public Node Board {private get; set;}




    // this should probably be an overlay type, not a piece, so spawned actors can sit on top of it


    public override void _Ready(){
		
	}

    public async Task Spawn(){
		var board = Board as Organizable;
		var query = Board as Queriable; 
		var center = query.GetCellFor(_pieceRoot);
        var w = query.GetDimensions().X;
		var h = query.GetDimensions().Y;
        var spawnCell = _whereToSpawn switch //this is rigid, should spawn in the direction of the player
        {
            Directions.Top => Hex.FindTopClamped(center, w, h),
            Directions.TopRight => Hex.FindTopRightClamped(center, w, h),
            Directions.BottomRight => Hex.FindBottomRightClamped(center, w, h),
            Directions.Bottom => Hex.FindBottomClamped(center, w, h),
            Directions.BottomLeft => Hex.FindBottomLeftClamped(center, w, h),
            Directions.TopLeft => Hex.FindTopLeftClamped(center, w, h),
            _ => Hex.FindTopClamped(center, w, h),
        };
		//obviously I'll have to work out what happens if the spawn cell is blocked first...
		await board.ReplaceTileWith(_pieceToSpawn, spawnCell);
    }
}
