using Board;
using Godot;
using Godot.Collections;
using System;

public partial class Environment : TileMapLayer, Tileable
{
	public Vector2I CellToPosition(Vector2I cell){
		return (Vector2I) MapToLocal(cell); 
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
