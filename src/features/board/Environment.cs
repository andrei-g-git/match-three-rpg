using Board;
using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

public partial class Environment : TileMapLayer, Tileable
{

	public Vector2I Size{
		get{
			var cells = GetUsedCells();
			var pattern = GetPattern(cells);
			return pattern.GetSize();
		}
	}
    public override void _Ready(){
        GetTree().CreateTimer(1.0).Timeout += () =>{
            GD.Print($"there are {GetUsedCells().Count} used cells");
		};
    }

    // public override void _Draw()
    // {
	// 	var rect = GetUsedRect();
    //     //DrawCircle(Vector2.Zero, 4, Colors.Red);
	// 	DrawRect(rect, Colors.Red, false, 2);

	// 	for(int i=0; i<Size.Y; i++)
	// 	{
	// 		DrawLine(new Vector2(10 + i * 30, 0), new Vector2(10 + i * 30, 50), Colors.Olive, 3f);
	// 	}
    // }

	public Vector2I GetPixelSize(){
		var mX = TileSet.TileSize.X * 3/4;
		var mY = TileSet.TileSize.Y;
		var mostOfIt = new Vector2I(
			Size.X * mX,
			Size.Y * mY
		);
		return mostOfIt + new Vector2I(TileSet.TileSize.X / 4, TileSet.TileSize.Y / 2);

	}

	//not interface
	public Vector2I GetCellSize(){
		return new Vector2I(
			TileSet.TileSize.X,
			TileSet.TileSize.Y
		);		
	}

	public Vector2I CellToPosition(Vector2I cell){
		return (Vector2I) MapToLocal(cell)/*  + new Vector2I(-32, -27) */; 
	}

	public Array<Vector2I> GetCellsInRadius(Vector2I center, int radiusInCells){
		var cellsInRadius = new Array<Vector2I>();
		var tileMinorDiameter = TileSet.TileSize.Y; 
		var pixelRadius = radiusInCells * tileMinorDiameter + 1;//- ((float)tileMinorDiameter/2 + 1); //+1px to ensure that the radius isn't just shy of the cell's center origin
		var pixelCenter = MapToLocal(center);
		foreach(var cell in GetUsedCells()){
			var cellPosition = MapToLocal(cell);
			var distanceToCell = pixelCenter.DistanceTo(cellPosition);
			//if(distanceToCell >= pixelRadius){
			if(pixelRadius >= distanceToCell){
				cellsInRadius.Add(cell);
			}
		}
		return cellsInRadius;
	}

    public Vector2I PositionToCell(Vector2 position){
        return LocalToMap(position);
    }
}
