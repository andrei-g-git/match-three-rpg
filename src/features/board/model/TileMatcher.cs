using Board;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;
using Util;

public partial class TileMatcher : Node, MatchableBoard, WithTiles
{
    [Export] private Node _tileFactory;
    [Export] private Node _tileContainer;
    public Grid<Control> Tiles{get;set;}	
    private Queue<List<Vector2I>> _matchGroupQueue = [];

    public bool TryMatching(Control sourceTile, Control targetTile){
        Debugging.PrintStackedGridInitials(Tiles.GetGridAs2DList(), 2, 2, "STACKED Grid before current match attempt:");
        var probeGrid = _SwapCellsInTemporaryGrid(sourceTile, targetTile, Tiles);   
        var gotMatches = _CheckNewMatchesAndProcess(probeGrid); //enqueues new match groups
                                //THIS RUNS BEFORE OLD MATCHES ARE REMOVED!!
        if(gotMatches){
            _SwapTileNodes(sourceTile, targetTile/* , Tiles */);
            Tiles = probeGrid;  
            if(_matchGroupQueue.Peek() != null){
                GetTree().CreateTimer(1).Timeout += () => { //temporary ... nothing more permanent eh...
                    _ActivateMatchedTilesAndCollapseGrid(_matchGroupQueue/* , Tiles */);
                };
            }            
        }
        //GD.Print([.._matchGroupQueue.Peek()]);
        return gotMatches;
    }


    private void _SwapTileNodes(Control sourceTile, Control targetTile/* , Grid<Control> grid */){
        var source = Tiles.GetCellFor(sourceTile);
        var target = Tiles.GetCellFor(targetTile);
        (sourceTile as Movable).MoveTo(target);
        (targetTile as Movable).MoveTo(source);
    }


    private bool _CheckNewMatchesAndProcess(Grid<Control> grid){
        var matchGroupsForAllDirections = new List<List<List<Vector2I>>>(){
            _FindMatchingGroupsNorthEast(grid), //THESE RUN BEFORE OLD MATCHES ARE REMOVED!!!
            _FindMatchingGroupsNorthWest(grid),
            _FindMatchingGroupsVertical(grid)
        };
        foreach(var matchGroups1Dir in matchGroupsForAllDirections){
            foreach(var group in matchGroups1Dir){
                if(group.Count > 0){
                    _matchGroupQueue.Enqueue(group);                    
                }

            }
        }
        return matchGroupsForAllDirections[0][0].Count > 0 || matchGroupsForAllDirections[1][0].Count > 0 || matchGroupsForAllDirections[2][0].Count > 0;
    } 


    private void _ActivateMatchedTilesAndCollapseGrid(Queue<List<Vector2I>> matchGroupQueue/* , Grid<Control> grid */){ //all this dependency injection is kind of useless if I hard code helper funcions... this is not a pure function
        var group = matchGroupQueue.Dequeue();
        var matchQueue = new Queue<Vector2I>(group);
        _RunMatchedTileBehaviors(matchQueue/* , grid */);

        //_CollapseTiles(grid/* , false, false, false */);
        var bp = 123;

        Debugging.PrintStackedGridInitials(Tiles.GetGridAs2DList(), 2, 2, "STACKED Grid:");
        bp = 123;
        if(matchGroupQueue.Count > 0){
            _ActivateMatchedTilesAndCollapseGrid(matchGroupQueue/* , grid */);            
        }
    }


    private void _RunMatchedTileBehaviors(Queue<Vector2I>matchQueue/* , Grid<Control> grid */){
        if(matchQueue.Count>0){
            _ActivateMatcedTileAndRemove(matchQueue/* , grid */);

            _RunMatchedTileBehaviors(matchQueue/* , grid */);
        }     
    }    


    private void _ActivateMatcedTileAndRemove(Queue<Vector2I> matches/* , Grid<Control> grid */){
        if(matches.Count > 0){ 
            var cell = matches.Dequeue();

            var tile = Tiles.GetItem(cell);
            Tiles.SetCell(
                (Control) (_tileFactory as TileMaking).Create(TileTypes.Blank), 
                cell
            );
            if(tile is Matchable matchable){
                matchable.BeginPostMatchProcessDependingOnPlayerPosition(cell, null, false);
            }
            _ActivateMatcedTileAndRemove(matches/* , grid */);
        }      
    }


    private Grid<Control> _SwapCellsInTemporaryGrid(Control sourceTile, Control targetTile, Grid<Control> grid){
        var probeGrid = grid.Clone();
        var source = grid.GetCellFor(sourceTile);
        var target = grid.GetCellFor(targetTile);

        probeGrid.SetCell(targetTile, source.X, source.Y); 
        probeGrid.SetCell(sourceTile, target.X, target.Y);   
        return probeGrid;  
    }   


    private List<List<Vector2I>> _FindMatchingGroupsInLine(List<Vector2I> line, List<List<Vector2I>> matchGroups, int groupIndex, Grid<Control> grid){
        var matches = new List<Vector2I>();
        for(int a=0; a<line.Count - 2; a++){
            var c1 = line[a];
            var c2 = line[a+1];
            var c3 = line[a+2];

            var test1 = (grid.GetItem(c1.X, c1.Y) as Tile).Type;
            var test2 = (grid.GetItem(c2.X, c2.Y) as Tile).Type;
            var test3 = (grid.GetItem(c3.X, c3.Y) as Tile).Type;		

            if(
                (grid.GetItem(c1.X, c1.Y) as Tile).Type != TileTypes.Blank &&
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
        
        for(int i=0;i<allMatches.Count;i++)	{
            var cell = allMatches[i];
            if(i<allMatches.Count-1){
                var next = allMatches[i+1];
                var tileType = (grid.GetItem(cell.X, cell.Y) as Tile).Type;
                var nextTileType = (grid.GetItem(next.X, next.Y) as Tile).Type;
                if(tileType == nextTileType){
                    matchGroups[groupIndex].Add(cell);
                }else{
                    groupIndex++;
                    matchGroups[groupIndex].Add(cell);
                }
            }else{//reached end, assume last tile is also a valid match
                matchGroups[groupIndex].Add(cell);
            }
        }
        return matchGroups;
    }    


    private List<List<Vector2I>> _FindMatchingGroupsNorthWest(Grid<Control> grid){
        var matchGroups = new List<List<Vector2I>>(){new List<Vector2I>()};
        var groupIndex = 0;
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
                    (grid.GetItem(xx, yy) is Tile) //&&
                    //(grid.GetItem(xx, yy) as Tile).Type != TileTypes.Blank
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            matchGroups = _FindMatchingGroupsInLine(diagonal, matchGroups, groupIndex, grid);
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsNorthEast(Grid<Control> grid){
        var matchGroups = new List<List<Vector2I>>(){new List<Vector2I>()};
        var groupIndex = 0;
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
                    (grid.GetItem(xx, yy) is Tile) //&&
                    //(grid.GetItem(xx, yy) as Tile).Type != TileTypes.Blank
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            matchGroups = _FindMatchingGroupsInLine(diagonal, matchGroups, groupIndex, grid);
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsVertical(Grid<Control> grid){

        var matchGroups = new List<List<Vector2I>>(){new List<Vector2I>()};
        var groupIndex = 0;        
        for(int x=0; x<grid.Width; x++){ 
            var line = new List<Vector2I>();        
            for(int y=0; y<grid.Height/*  - 2 */; y++){  
                var tile = grid.GetItem(x, y); 
                if(
                    tile != null &&
                    tile is Tile
                ){
                    line.Add(new Vector2I(x, y)); 
                } 
            }
            matchGroups = _FindMatchingGroupsInLine(line, matchGroups, groupIndex, grid);
        } 
        return matchGroups;    
    }        
}