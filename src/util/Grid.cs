using Godot;
using System;
using System.Collections.Generic;

public class Grid<[MustBeVariant] T>
{
	public int Width => _grid.Count;
	public int Height => _grid.Count > 0? _grid[0].Count : 0;
	private List<List<T>> _grid = [];

	public Grid(){
		_grid = [];
	}
	
	public Grid(int width, int height){
		_grid = _Make2DList(width, height);
	}

	// public void Initialize(T item){  //this will set eveny item to the same object reference
	// 	for (int i = 0; i < Width; i++){
	// 		for (int j = 0; j < Height; j++){
	// 			_grid[i][j] = item;
	// 		}
	// 	}		
	// }

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

	public void AddRow(List<T> row){
		_grid.Add(row);
	}

    public Grid<T> Clone(){
		var gridCopy = _Make2DList(Width, Height);
		for(int x=0;x<Width;x++){
			for(int y=0;y<Height;y++){
				gridCopy[x][y] = _grid[x][y];
			}		
		}
        return new Grid<T>(Width, Height){
			_grid = gridCopy
		};
    }


	//public List<List<T>> GetGridAs2DList() => _grid;
	public List<List<T>> GetGridAs2DList(){
		var copy = new List<List<T>>(Width);
		for(int x=0;x<Width;x++){
			copy.Add(new List<T>(_grid[x]));
		}
		return copy;
	}

	private List<List<T>> _Make2DList(int width, int height){
		var grid = new List<List<T>>(width);
		for (int i = 0; i < width; i++){
			var row = new List<T>(height);
			for (int j = 0; j < height; j++){
				row.Add(default); 
			}
			grid.Add(row);
		}
		return grid;		
	}

	public T FindItemByType(Type interfaceOrClass){
		if(!interfaceOrClass.IsInterface && !interfaceOrClass.IsClass){
			throw new ArgumentException("Expected interface or class :", nameof(interfaceOrClass));
		}
		foreach(var column in _grid){
			foreach(var item in column){
				if(item != null && interfaceOrClass.IsInstanceOfType(item)){
					return item;
				}
			}
		}
		return default;
	}
}
