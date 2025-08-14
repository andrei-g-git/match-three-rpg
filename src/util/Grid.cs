using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

//namespace Util;
public class Grid<[MustBeVariant] T>//: ICloneable
{
	public int Width => _grid.Count;
	public int Height => _grid.Count > 0? _grid[0].Count : 0;
	//private Array<Array<T>> _grid = [];
	private List<List<T>> _grid = [];

	public Grid(){
		_grid = [];
	}
	
	public Grid(int width, int height){
		// _grid.Resize(width); //guess I don't need to with a list
		// foreach(var row in _grid){ //logic in the constructor...
		// 	row.Resize(height);
		// }

		// _grid = new(width);
		// for (int i = 0; i < width; i++){
		// 	var row = new List<T>(height);
		// 	for (int j = 0; j < height; j++){
		// 		row.Add(default); //I have no idea what this does, but I can't initialize to null so...
		// 	}
		// 	_grid.Add(row);
		// }
		_grid = _Make2DList(width, height);
	}

	public void Initialize(T item){
		for (int i = 0; i < Width; i++){
			for (int j = 0; j < Height; j++){
				_grid[i][j] = item;
			}
		}		
	}

	public void SetCell(T item, Vector2I cell){
		_grid[cell.X][cell.Y] = item;
	}

	public void SetCell(T item, int x, int y){
		_grid[x][y] = item;
	}

	public T GetItem(Vector2I cell){
		return _grid[cell.X][cell.Y];
	}
	public T GetItem(int x, int y){
		return _grid[x][y];
	}

	public Vector2I GetCellFor(T item){
		for(int x=0; x < _grid.Count; x++){
			for(int y=0; y < _grid[0].Count; y++){
				var itemAtThisPosition = _grid[x][y];
				if(itemAtThisPosition != null && itemAtThisPosition.Equals(item)){
					return new Vector2I(x, y);
				}
			}
		}
		return new Vector2I(-69, -420);		
	}

	public void AddRow(/* Array */List<T> row){
		_grid.Add(row);
	}

    public Grid<T> Clone(){
		var gridCopy = _Make2DList(Width, Height);//new List<List<T>>();
		for(int x=0;x<Width;x++){
			for(int y=0;y<Height;y++){
				gridCopy[x][y] = _grid[x][y];
			}		
		}
        return new Grid<T>(Width, Height){
			//_grid = _grid.Duplicate(true) //this is a deep copy so maybe it could cause issues if it also clones the items instead of assigning their references
			_grid = gridCopy
		};
    }


	public List<List<T>> GetGridAs2DList() => _grid;


	private List<List<T>> _Make2DList(int width, int height){
		var grid = new List<List<T>>(width);
		for (int i = 0; i < width; i++){
			var row = new List<T>(height);
			for (int j = 0; j < height; j++){
				row.Add(default); //I have no idea what this does, but I can't initialize to null so...
			}
			grid.Add(row);
		}
		return grid;		
	}
}
