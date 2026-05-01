using Board;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Util;

public partial class TileContainer : Control, Viewable
{
    [Export] private TileMapLayer environment;


    public override void _Draw()
    {
		DrawRect(new Rect2(0, 0, Size), Colors.Green, false, 3);
    }


    public override void _Ready(){
        //Size = environment.GetUsedRect().Size; 
    }

    public void Initialize(Grid<Control> tiles){
		var pixSiz = (environment as Tileable).GetPixelSize();
		GD.Print($"tile container size: {pixSiz.X}, {pixSiz.Y}");
		Size = pixSiz;


		foreach(Node child in GetChildren()){
			RemoveChild(child);
			child.QueueFree();
		}	
		for (int x = 0; x < tiles.Width; x++){
			for (int y = 0; y < tiles.Height; y++){
				var tile = tiles.GetItem(x, y);
				if(tile != null){
					tile.Position = (environment as Tileable).CellToPosition(new Vector2I(x, y)); 
					AddChild(tile);					
				}
			}
		}        
    }

    public void UpdatePositions(Grid<Control> tiles){
		for (int x = 0; x < tiles.Width; x++){
			for (int y = 0; y < tiles.Height; y++){
				var tile = tiles.GetItem(x, y);
				if(tile != null){
					tile.Position = (environment as Tileable).CellToPosition(new Vector2I(x, y)); 
				}
			}
		}
		//Debugging.PrintChildrenTileInitials([.. GetChildren()], 2, "TileContainer children:");
    }

	public void PlaceNew(Control newTile, Control oldTile, Vector2I cell){
		newTile.Position = (environment as Tileable).CellToPosition(cell);
		if(oldTile!= null){
			//should call Removing.PrepDestroy and then await Removing.WaitForRemoved
				//unless I'm doing it inside whatever calls this

			RemoveChild(oldTile);
			oldTile.QueueFree();
		}
		AddChild(newTile);		
	}

	public void Add(Control tile, Vector2I cell){
		tile.Position = (environment as Tileable).CellToPosition(cell);
		AddChild(tile);
	}

}
