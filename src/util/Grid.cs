using Godot;
using Godot.Collections;
using System;

//namespace Util;
public class Grid<[MustBeVariant] T>//: ICloneable
{
	public int Width => _grid.Count;
	public int Height => _grid.Count > 0? _grid[0].Count : 0;
	private Array<Array<T>> _grid = [];

	public Grid(){}
	
	public Grid(int width, int height){
		_grid.Resize(width);
		foreach(var row in _grid){ //logic in the constructor...
			row.Resize(height);
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

	public void AddRow(Array<T> row){
		_grid.Add(row);
	}

    public Grid<T> Clone(){
        return new Grid<T>(Width, Height){
			_grid = _grid.Duplicate(true)
		};
    }

}
