using Board;
using Godot;
using System;
using System.Collections.Generic;
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
        _ProcessNewMatches(probeGrid); //enqueues new match groups
        var _hasMatches = false;
        if(_matchGroupQueue.Peek() != null){
            _hasMatches = true;       
            //var group = _matchGroupQueue.Dequeue(); //the calling tile will wait a long time to get a return value but if the return is True then it does not need
                                                //to emit a signal to initiate another kind of swap so it doesn't matter...
            _SwapTileNodes(sourceTile, targetTile, Tiles);
            Tiles = probeGrid;  

            GetTree().CreateTimer(1).Timeout += () => { //temporary ... nothing more permanent eh...
                var group = _matchGroupQueue.Dequeue();
                var matchQueue = new Queue<Vector2I>(group);
                _RunMatchedTileBehaviors(/* _matchGroupQueue */matchQueue, Tiles);

                _CollapseTiles(Tiles, false, false, false);
                var bp = 123;

                GetTree().CreateTimer(1).Timeout += () => { //this sucks
                    (_tileContainer as Viewable).UpdatePositions(Tiles);
                };
                Debugging.PrintStackedGridInitials(Tiles.GetGridAs2DList(), 2, 2, "STACKED Grid:");
                bp = 123;
            };
        }
        GD.Print([.._matchGroupQueue.Peek()]);
        return _hasMatches;
    }

    private void _SwapTileNodes(Control sourceTile, Control targetTile, Grid<Control> grid){
        var source = grid.GetCellFor(sourceTile);
        var target = grid.GetCellFor(targetTile);
        (sourceTile as Movable).MoveTo(target);
        (targetTile as Movable).MoveTo(source);
    }

    private Grid<Control> _SwapCellsInTemporaryGrid(Control sourceTile, Control targetTile, Grid<Control> grid){
        var probeGrid = grid.Clone();
        var source = grid.GetCellFor(sourceTile);
        var target = grid.GetCellFor(targetTile);

        probeGrid.SetCell(targetTile, source.X, source.Y); 
        probeGrid.SetCell(sourceTile, target.X, target.Y);   
        return probeGrid;  
    }
    private void _ProcessNewMatches(Grid<Control> grid){
        var matchGroupsForAllDirections = new List<List<List<Vector2I>>>(){
            _FindMatchingGroupsNorthEast(grid),
            _FindMatchingGroupsNorthWest(grid),
            _FindMatchingGroupsVertical(grid)
        };
        // foreach(var directionGroups in matchGroupsForAllDirections){
        //     foreach(var matches in directionGroups){
        //         GD.Print("matches:  ", new Godot.Collections.Array([..matches]));
        //     }
        // }
        foreach(var matchGroups1Dir in matchGroupsForAllDirections){
            foreach(var group in matchGroups1Dir){
                if(group.Count > 0){
                    _matchGroupQueue.Enqueue(group);                    
                }

            }
        }
        // if(_matchGroupQueue.Peek() != null){
        //     var group = _matchGroupQueue.Dequeue();
        // }
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
        //var groupIndex = 0;
        //var group = new List<Vector2I>();        
        for(int i=0;i<allMatches.Count;i++)	{
            var cell = allMatches[i];
            if(i<allMatches.Count-1){
                var next = allMatches[i+1];
                var tileType = (grid.GetItem(cell.X, cell.Y) as Tile).Type;
                var nextTileType = (grid.GetItem(next.X, next.Y) as Tile).Type;
                if(tileType == nextTileType){
                    //group.Add(cell);
                    matchGroups[groupIndex].Add(cell);
                }else{
                    groupIndex++;
                    matchGroups[groupIndex].Add(cell);
                    // matchGroups.Add(group);					
                    // group = [cell]; //was using godot array instead of c# List, no idea if this code is still good...
                }
            }else{//reached end, assume last tile is also a valid match
                //group.Add(cell);
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
                    (grid.GetItem(xx, yy) is Tile) &&
                    (grid.GetItem(xx, yy) as Tile).Type != TileTypes.Blank
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
                    (grid.GetItem(xx, yy) is Tile) &&
                    (grid.GetItem(xx, yy) as Tile).Type != TileTypes.Blank
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            matchGroups = _FindMatchingGroupsInLine(diagonal, matchGroups, groupIndex, grid);
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsVertical(Grid<Control> grid){
        var line = new List<Vector2I>();
        var matchGroups = new List<List<Vector2I>>(){new List<Vector2I>()};
        var groupIndex = 0;        
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
        return _FindMatchingGroupsInLine(line, matchGroups, groupIndex, grid);    
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


    private void _RunMatchedTileBehaviors(/* Queue<List<Vector2I>> */ Queue<Vector2I>/* matchGroupQueue */matchQueue, Grid<Control> grid){
        //GD.Print([..matchGroupQueue]);
        if(/* matchGroupQueue */matchQueue.Count>0/* .Peek() != null *//*  && matchGroupQueue.Peek().Count > 0 */){
            // var group = matchGroupQueue.Dequeue();   

            // var matchQueue = new Queue<Vector2I>(group);

            _ActivateMatcedTileAndRemove(matchQueue, grid);

            //_CollapseTiles(grid, false, false, false);

            _RunMatchedTileBehaviors(/* matchGroupQueue */matchQueue, grid);
        }     
    }


    private void _ActivateMatcedTileAndRemove(Queue<Vector2I> matches, Grid<Control> grid){
        if(matches.Count > 0/* .Peek() != null */){ 
            var cell = matches.Dequeue();

            var tile = grid.GetItem(cell);
            grid.SetCell(
                (Control) (_tileFactory as TileMaking).Create(TileTypes.Blank), 
                cell
            );
            if(tile is Matchable matchable){
                matchable.BeginPostMatchProcessDependingOnPlayerPosition(cell, null, false);
            }

            _ActivateMatcedTileAndRemove(matches, grid);
        }      
    }

    private void _CollapseTiles(Grid<Control> grid, bool bottomFilled, bool bottomLeftFilled, bool bottomRightFilled){
        var filled1 = _TrickleDownTilesToFillGap(grid, bottomFilled, Hex.FindBottomClamped);   
        var filled2 = _TrickleDownTilesToFillGap(grid, bottomLeftFilled, Hex.FindBottomLeftClamped);
        var filled3 = _TrickleDownTilesToFillGap(grid, bottomRightFilled, Hex.FindBottomRightClamped);

        GD.Print(/* bottomFilled */filled1, "  ", /* bottomLeftFilled */filled2, "  ", /* bottomRightFilled */filled3);

        Debugging.PrintItemsInitials(grid.GetGridAs2DList(), 2, 2, "collapsing to fill remaining gaps, GRID RIGHT NOW: ");

        if(!filled1 &&/* || */ !filled2 &&/* || */ !filled3){
            _CollapseTiles(grid, false, false, false);
        }            
    }  


    private bool _TrickleDownTilesToFillGap(
        Grid<Control> grid, 
        bool cellBelowInChosenDirectionFilled, 
        Func<Vector2I, int, int, Vector2I> FindCellBelowInOneOfThreeDirectionsClamped
    ){
        var isFilled = cellBelowInChosenDirectionFilled; //man I dunno I don't feel like thinking ...
        //for(int y=0;y<Tiles.Count;y++){ //REVERSED 
        for(int x=0;x<grid.Width;x++){
            //for(int x=0;x<Tiles[0].Count;x++){					
            for(int y=0;y<grid.Height;y++){
                if(grid.GetItem(x, y) is Collapsable collapsable){
                    var cell = FindCellBelowInOneOfThreeDirectionsClamped(new Vector2I(x, y), grid.Width, grid.Height);
                    if(cell.X > 0 && cell.Y > 0){
                        var lowerTile = grid.GetItem(cell.X, cell.Y);
                        if(lowerTile is Empty blank){
                            grid.SetCell(collapsable as Control, cell.X, cell.Y);
                            grid.SetCell(
                                (Control) (_tileFactory as TileMaking).Create(TileTypes.Blank), 
                                x, 
                                y
                            );
                            if(collapsable is Movable movable){
                                movable.MoveTo(cell);									
                            } 
                            //break;	!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        } else {
                            /* cellBelowInChosenDirectionFilled */ isFilled = true; //the tile underneath might need to fall too, and before this ... not sure what effect this might have
                        }
                    }						
                }
            }
        }  
        return isFilled;              
    }
}


