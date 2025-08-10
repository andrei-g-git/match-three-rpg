using Board;
using Godot;
using System;
using System.Collections.Generic;
using Tiles;
using Util;

public partial class TileMatcher : Node, MatchableBoard
{

    private Queue<List<Vector2I>> _matchGroupQueue = [];
    public bool TryMatching(Control sourceTile, Control targetTile){
        throw new NotImplementedException();
    }


    private void _CheckForMatchesThenCollapse(Grid<Control> grid){
        var matchGroupsForAllDirections = new List<List<List<Vector2I>>>(){
            _FindMatchingGroupsNorthEast(grid),
            _FindMatchingGroupsNorthWest(grid),
            _FindMatchingGroupsVertical(grid)
        };
        foreach(var matchGroups1Dir in matchGroupsForAllDirections){
            foreach(var group in matchGroups1Dir){
                 _matchGroupQueue.Enqueue(group);
            }
        }
        if(_matchGroupQueue.Peek() != null){
            var group = _matchGroupQueue.Dequeue();
        }
    }

    private List<List<Vector2I>> _FindMatchingGroupsInLine(List<Vector2I> line, Grid<Control> grid){
        var matches = new List<Vector2I>();
        for(int a=0; a<line.Count - 2; a++){
            var c1 = line[a];
            var c2 = line[a+1];
            var c3 = line[a+2];

            var test1 = (grid.GetItem(c1.X, c1.Y) as Tile).Type;
            var test2 = (grid.GetItem(c2.X, c2.Y) as Tile).Type;
            var test3 = (grid.GetItem(c3.X, c3.Y) as Tile).Type;		

            if(
                (grid.GetItem(c1.X, c1.Y) as Tile).Type  == (grid.GetItem(c2.X, c2.Y) as Tile).Type && 
                (grid.GetItem(c2.X, c2.Y) as Tile).Type == (grid.GetItem(c3.X, c3.Y) as Tile).Type 
            ){
                matches.Add(c1);
                matches.Add(c2);
                matches.Add(c3);
            }
        }
        var allMatches = Collections.RemoveDuplicates(matches); //there could be 2 match groups	....	
                //I have no idea how this removes the duplicates and how the List will look like for multiple colors...
        var matchGroups = new List<List<Vector2I>>();
        for(int i=0;i<allMatches.Count;i++)	{
            var group = new List<Vector2I>();
            var cell = allMatches[i];
            if(i<allMatches.Count-1){
                var next = allMatches[i+1];
                var tileType = (grid.GetItem(cell.X, cell.Y) as Tile).Type;
                var nextTileType = (grid.GetItem(next.X, next.Y) as Tile).Type;
                if(tileType == nextTileType){
                    group.Add(cell);
                }else{
                    matchGroups.Add(group);					
                    group = [cell]; //was using godot array instead of c# List, no idea if this code is still good...
                }
            }else{//reached end, assume last tile is also a valid match
                group.Add(cell);
            }
        }
        return matchGroups;
    }    


    private List<List<Vector2I>> _FindMatchingGroupsNorthWest(Grid<Control> grid){
        var matchGroups = new List<List<Vector2I>>();
        for(int x=1; x<=grid.Height; x++){			
            var diagonal = new List<Vector2I>();
            for(int y=0; y<grid.Width; y++){
                var xx = grid.Width - 1 - y;
                var yy = x - (y - (y / 2));
                if(
                    xx >= 0 &&
                    yy >= 0	&&			
                    yy < grid.Width &&	
                    grid.GetItem(xx, yy) != null && 
                    (grid.GetItem(xx, yy) is Tile) &&
                    (grid.GetItem(xx, yy) as Tile).Type != TileTypes.Blank
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            matchGroups = _FindMatchingGroupsInLine(diagonal, grid);
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsNorthEast(Grid<Control> grid){
        var matchGroups = new List<List<Vector2I>>();
        for(int x=1; x<=grid.Height; x++){			
            var diagonal = new List<Vector2I>();
            for(int y=0; y<grid.Width; y++){
				var xx = y; 
				var yy = x - (y - (y / 2));  //integer division will floor the result automatically, leaving ODD loops having the same yy value as the last loop
                if(
                    xx >= 0 &&
                    yy >= 0	&&			
                    yy < grid.Width &&	
                    grid.GetItem(xx, yy) != null && 
                    (grid.GetItem(xx, yy) is Tile) &&
                    (grid.GetItem(xx, yy) as Tile).Type != TileTypes.Blank
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            matchGroups = _FindMatchingGroupsInLine(diagonal, grid);
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsVertical(Grid<Control> grid){
        var line = new List<Vector2I>();
        for(int y=0; y<grid.Height; y++){ //Reversed looping
            for(int x=0; x<grid.Width - 2; x++){  
                var tile = grid.GetItem(x, y);
                if(
                    tile != null &&
                    tile is Tile
                ){
                    line.Add(new Vector2I(x, y));
                } 
            }
        } 
        return _FindMatchingGroupsInLine(line, grid);    
    }

    private List<Vector2I> FindMatchingLinesVertical(Grid<Control> grid, TileTypes type){
        var matchGroups = new List<List<Vector2I>>();
        for(int x=0; x<grid.Width; x++){
            for(int y=0; y<grid.Height - 2; y++){
                var first = grid.GetItem(x, y) as Tile;
                var second = grid.GetItem(x, y+1) as Tile;
                var third = grid.GetItem(x, y+2) as Tile;
                if(
                    first != null &&
                    second != null &&
                    third != null &&						
                    first.Type == type &&
                    second.Type == type &&
                    third.Type == type
                ){
                    return [
                        new Vector2I(x, y),
                        new Vector2I(x, y + 1),
                        new Vector2I(x, y + 2),
                    ];
                }				
            }
        }
        return [];
    }    
}


