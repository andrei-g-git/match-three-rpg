using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Tiles;

//for VERTICAL offset, STACKED
public static class Hex{

	public static Vector2I FindTop(Vector2I cell){
		var x = cell.X; var y = cell.Y;
		return new Vector2I(x, y - 1);
	}

	public static Vector2I FindBottom(Vector2I cell){
		var x = cell.X; var y = cell.Y;
		return new Vector2I(x, y + 1);
	}

	public static Vector2I FindTopRight(Vector2I cell){
		var x = cell.X; var y = cell.Y;
		return x % 2 == 0 ? 
			new Vector2I(x + 1, y - 1)
		:
			new Vector2I(x + 1, y);
	}

	public static Vector2I FindBottomRight(Vector2I cell){
		var x = cell.X; var y = cell.Y;
		return x % 2 == 0 ? 
			new Vector2I(x + 1, y)
		:
			new Vector2I(x + 1, y + 1);
	}

	public static Vector2I FindBottomLeft(Vector2I cell){
		var x = cell.X; var y = cell.Y;
		return x % 2 == 0 ? 
			new Vector2I(x - 1, y)
		:
			new Vector2I(x - 1, y + 1);
	}

	public static Vector2I FindTopLeft(Vector2I cell){
		var x = cell.X; var y = cell.Y;
		return x % 2 == 0 ? 
			new Vector2I(x - 1, y -1)
		:
			new Vector2I(x - 1, y);
	}


	public static Vector2I FindTopClamped(Vector2I cell, int maxWidth, int maxHeight){
		var neighbor = FindTop(cell);
		if(neighbor.X >= 0 && neighbor.Y >=0 && neighbor.X < maxWidth && neighbor.Y < maxHeight){
			return neighbor;
		}
		return new Vector2I(-69, -420);
	}

	public static Vector2I FindBottomClamped(Vector2I cell, int maxWidth, int maxHeight){
		var neighbor = FindBottom(cell);
		if(neighbor.X >= 0 && neighbor.Y >=0 && neighbor.X < maxWidth && neighbor.Y < maxHeight){
			return neighbor;
		}
		return new Vector2I(-69, -420);
	}
	public static Vector2I FindTopRightClamped(Vector2I cell, int maxWidth, int maxHeight){
		var neighbor = FindTopRight(cell);
		if(neighbor.X >= 0 && neighbor.Y >=0 && neighbor.X < maxWidth && neighbor.Y < maxHeight){
			return neighbor;
		}
		return new Vector2I(-69, -420);
	}
	public static Vector2I FindBottomRightClamped(Vector2I cell, int maxWidth, int maxHeight){
		var neighbor = FindBottomRight(cell);
		if(neighbor.X >= 0 && neighbor.Y >=0 && neighbor.X < maxWidth && neighbor.Y < maxHeight){
			return neighbor;
		}
		return new Vector2I(-69, -420);
	}
	public static Vector2I FindTopLeftClamped(Vector2I cell, int maxWidth, int maxHeight){
		var neighbor = FindTopLeft(cell);
		if(neighbor.X >= 0 && neighbor.Y >=0 && neighbor.X < maxWidth && neighbor.Y < maxHeight){
			return neighbor;
		}
		return new Vector2I(-69, -420);
	}
	public static Vector2I FindBottomLeftClamped(Vector2I cell, int maxWidth, int maxHeight){
		var neighbor = FindBottomLeft(cell);
		if(neighbor.X >= 0 && neighbor.Y >=0 && neighbor.X < maxWidth && neighbor.Y < maxHeight){
			return neighbor;
		}
		return new Vector2I(-69, -420);
	}

	
	public static Vector2I FindNextInLine(Array<Vector2I> line){
		var _length = line.Count;
		if(_length >=2){
			var secondToLast = line[_length-2];
			var last = line[_length-1];
			if(last == FindTop(secondToLast)){
				return FindTop(last);
			}else if(last == FindBottom(secondToLast)){
				return FindBottom(last);
			}else if(last == FindTopRight(secondToLast)){
				return FindTopRight(last);
			}else if(last == FindBottomRight(secondToLast)){
				return FindBottomRight(last);
			}else if(last == FindTopLeft(secondToLast)){
				return FindTopLeft(last);
			}else if(last == FindBottomLeft(secondToLast)){
				return FindBottomLeft(last);
			}
		}
		return new Vector2I(-69, -420);
	}

	public static bool CheckIfNeighbor(Vector2I center, Vector2I celllToCheck){
		var neighbors = new Array<Vector2I>(){
			FindTop(center),
			FindBottom(center),
			FindTopRight(center),
			FindBottomRight(center),
			FindBottomLeft(center),
			FindTopLeft(center)		
		};
		return neighbors.Contains(celllToCheck);
	}

	// public static Array<Array<TileTypes>> NodeGridToEnums(Array<Array<Control>> grid){
	// 	var enumGrid = new Array<Array<TileTypes>>();
	// 	enumGrid.Resize(grid.Count);
	// 	for(int x=0; x<grid.Count; x++){
	// 		enumGrid[x].Resize(grid[x].Count);
	// 		for(int y=0; y<grid[x].Count; y++){
	// 			var potentialNode = grid[x][y];
	// 			if(potentialNode is Tile tile){
	// 				enumGrid[x][y] = tile.Type;					
	// 			}

	// 		}			
	// 	}
	// 	return enumGrid;
	// }

	public static Grid<TileTypes> StringGridToEnums(Grid<string> stringGrid){ //these shouldn't be here
		var enumGrid = new Grid<TileTypes>(stringGrid.Width, stringGrid.Height);
		for (int a = 0; a < enumGrid.Width; a++){
			for (int b = 0; b < enumGrid.Height; b++){
				var stringName = stringGrid.GetItem(a, b);
				enumGrid.SetCell(TileDict.GetEnum(stringName), a, b);
			}
		}
		return enumGrid;
	}
	public static Array<Array<TileTypes>> StringGridToEnums_test(Array<Array<string>> stringGrid){ //these shouldn't be here
		var enumGrid = new Array<Array<TileTypes>>();
        enumGrid.Resize(stringGrid.Count);
		for (int a = 0; a < stringGrid.Count; a++){
            enumGrid[a].Resize(stringGrid[a].Count);
			for (int b = 0; b < stringGrid[a].Count; b++){
				var stringName = stringGrid[a][b];
				enumGrid[a][b] = TileDict.GetEnum(stringName);
			}
		}
		return enumGrid;
	}

	public static void PrintGridItemsInitials(Array<Array<string>> grid, int howManyLetters, string header)
	{
		int cols = grid.Count;
		int rows = grid[0].Count;
		var initialsGrid = new Array<Array<string>>();
		initialsGrid.Resize(cols);
		GD.Print(header, "\n");

		for(int i = 0; i < cols; i++){
			if (rows != 0)
			{
				var extraSpace = " ";
				var cSharpArray = grid[i].Select(item => item.Length > howManyLetters ? item[..howManyLetters] + extraSpace : item + extraSpace);
				initialsGrid[i] = [.. cSharpArray];
				var computedNewLines = howManyLetters % 2;// + 1;
				GD.Print(initialsGrid[i]/* .ToString() */);	 //ToString() so it doesn't also print the quotation marks -- nope it doesn't work...
				for (int a = 0; a < computedNewLines; a++)
				{
					GD.Print("\n");
				}	
			}

		}
		Console.WriteLine("\n");
	}
}