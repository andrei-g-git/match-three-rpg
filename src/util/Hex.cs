using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
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
		for (int a = 0; a < stringGrid.Width; a++){
			for (int b = 0; b < stringGrid.Height; b++){
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

	public static void PrintGridItemsInitials__(Array<Array<string>> grid, int howManyLetters, string header)
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

	public static void IterateOverRowsNorthEast<[MustBeVariant]T>(List<List<T>> grid, Action<List<List<T>>, List<Vector2I>> IterateNorthEastDiagonal){ //will need to change the parameter into a 2d array. I don't want to couple a utility class to a specific data type I made
		for(int x=1; x<=grid.Count; x++){			
            var diagonal = new List<Vector2I>();
            for(int y=0; y<grid[x].Count; y++){
				var xx = y; 
				var yy = x - (y - (y / 2));  //integer division will floor the result automatically, leaving ODD loops having the same yy value as the last loop
                if(
                    xx >= 0 &&
                    yy >= 0	&&			
                    yy < grid.Count &&	
                    grid[xx][yy] != null && 
                    (grid[xx][yy] is Tile) &&
                    (grid[xx][yy] as Tile).Type != TileTypes.Blank
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            IterateNorthEastDiagonal(grid, diagonal);
        }
	}
}