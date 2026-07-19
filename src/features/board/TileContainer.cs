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

	[Signal] public delegate void ResizedEventHandler(Vector2 size);

    // public override void _Draw()
    // {
	// 	//DrawRect(new Rect2(0, 0, Size), Colors.Green, false, 3);
	// 	//GD.Print($"TileContainer size: {Size.X}, {Size.Y} <---------------");
    // }


    public override void _Ready(){
        //Size = environment.GetUsedRect().Size; 
		Connect(Control.SignalName.Resized, new Callable(this, nameof(_OnResized)));
    }

	private void _OnResized()
	{
		var pixSiz = (environment as Tileable).GetPixelSize();
		EmitSignal(SignalName.Resized, pixSiz);
		GD.Print($"tile container RESIZED: {pixSiz.X}, {pixSiz.Y}");
    }
	

    public void Initialize(Grid<Control> tiles){
		var pixSiz = (environment as Tileable).GetPixelSize();
		//GD.Print($"tile container size: {pixSiz.X}, {pixSiz.Y}");
		Size = pixSiz;

//GD.Print($"Code executed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");



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

		// EmitSignal(SignalName.Resized, Size);     
		// GD.Print($"tile container RESIZED: {pixSiz.X}, {pixSiz.Y}");
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

			if(oldTile.GetParent() == this){ //if this doesn't work I could also iterate through GetChildren
				RemoveChild(oldTile);
			}
			
			oldTile.QueueFree();
		}
		AddChild(newTile);		
	}

	public void Add(Control tile, Vector2I cell){
		tile.Position = (environment as Tileable).CellToPosition(cell);
		AddChild(tile);
	}

}
