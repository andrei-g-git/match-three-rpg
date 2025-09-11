using Board;
using Godot;
using Godot.Collections;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;
using Util;

public partial class TileMatcher : Node, MatchableBoard, WithTiles
{
    [Export] private Node _tileFactory;
    [Export] private Node _tileContainer;
    [Export] private Node _tileQuery;
    [Export] private Node _turns;
    public Grid<Control> Tiles{get;set;}	
    private Queue<List<Vector2I>> _matchGroupQueue = [];
    private System.Collections.Generic.Dictionary<TileTypes, int> _spawnOddsByTileType;
    private float[] _spawnWeights;
    private TileTypes[] _spawnTiles;
    [Signal] public delegate void DoneMatchingEventHandler();

    public override void _Ready(){
		_spawnOddsByTileType = new(){ 
			{TileTypes.Defensive, 3},
            {TileTypes.Melee, 3},
            {TileTypes.Ranged, 3},
            {TileTypes.Tech, 3},
		};
		_spawnWeights = [.._spawnOddsByTileType.Select(item => item.Value)];   
        _spawnTiles = [.._spawnOddsByTileType.Select(item => item.Key)];   
    }

    public /* bool */async Task<bool> TryMatching(Control sourceTile, Control targetTile){
        if(sourceTile is Swappable && targetTile is Swappable){
            var initialSource = Tiles.GetCellFor(sourceTile);
            var initialTarget = Tiles.GetCellFor(targetTile);
            Debugging.PrintStackedGridInitials(Tiles.GetGridAs2DList(), 2, 2, "STACKED Grid before current match attempt:");
            //var probeGrid = _SwapCellsInTemporaryGrid(sourceTile, targetTile, Tiles);   //this makes another grid instance that's not shared with other component models
            _SwapCells(sourceTile, targetTile);
            //EVERYTHING BELOW THIS SHOULD BE A PUBLIC METHOD that I can also use in the tileOrganizer to resolve new matches resulting from transfering tiles
            var gotMatches = _CheckNewMatchesAndProcess(Tiles); //enqueues new match groups
                                    //THIS RUNS BEFORE OLD MATCHES ARE REMOVED!!  (does it still?...)
            if(gotMatches){
                var player = Tiles.FindItemByType(typeof(Playable));

                if(player is not null && player is MatchableBounds matchingRange){
                    var matchGroupsAreInRange = matchingRange.IsMatchGroupInRange(_matchGroupQueue, Tiles);
                    if(matchGroupsAreInRange){
                        /* _ = */ await _SwapTileNodesUsingInitialBoard(sourceTile, targetTile, initialSource, initialTarget);
                        //Tiles = probeGrid;  //already mutated
                        if(_matchGroupQueue.Peek() != null){
                            //GetTree().CreateTimer(0.5).Timeout += () => { //temporary ... nothing more permanent eh...
                                await _ActivateMatchedTilesAndCollapseGrid(_matchGroupQueue);

                                var bp = 123;

                            //};
                        }                         
                    }else{
                        GD.Print("Player is not in rage!");
                        _SwapCells(targetTile, sourceTile); //reverse, hope this works
                    }
                }
                //AdvanceTurn(); //this doens't look so good over here...
                EmitSignal(SignalName.DoneMatching);
            }
            return gotMatches;            
        }
        return false;
    }


    // private void AdvanceTurn(){
    //     (_turns as Sequential).AdvanceTurn();
    // }

    public /* void */async Task MatchWithoutSwapping(){
        var gotMatches = _CheckNewMatchesAndProcess(Tiles);   
        if(gotMatches){
            if(_matchGroupQueue.Peek() != null){
                //GetTree().CreateTimer(1).Timeout += () => { //temporary ... nothing more permanent eh...
                    await _ActivateMatchedTilesAndCollapseGrid(_matchGroupQueue);
                //};
            }                         
        }             
    }


    public /* void */async Task CollapseGridAndCheckNewMatches(){
        await _ActivateMatchedTilesAndCollapseGrid(_matchGroupQueue);
    }

    private void _SwapTileNodes(Control sourceTile, Control targetTile){
        var source = Tiles.GetCellFor(sourceTile);
        var target = Tiles.GetCellFor(targetTile);
        (sourceTile as Movable).MoveTo(target);
        var bp = 123;
        (targetTile as Movable).MoveTo(source);
        bp = 2345;
    }

    private /* void */async Task _SwapTileNodesUsingInitialBoard(Control sourceTile, Control targetTile, Vector2I initialSource, Vector2I initialTarget){
        (sourceTile as Movable).MoveTo(initialTarget);
        (targetTile as Movable).MoveTo(initialSource);
        await (targetTile as Movable).WaitUntilMoved();
        var bp = 123;
    }


    private bool _CheckNewMatchesAndProcess(Grid<Control> grid){
        var matchGroupsForAllDirections = new List<List<List<Vector2I>>>(){
            _FindMatchingGroupsNorthEast(grid), //THESE RUN BEFORE OLD MATCHES ARE REMOVED!!!
            _FindMatchingGroupsNorthWest(grid),
            _FindMatchingGroupsVertical(grid)
        };
        var initialQueueSize = _matchGroupQueue.Count;
        foreach(var matchGroups1Dir in matchGroupsForAllDirections){
            foreach(var group in matchGroups1Dir){
                if(group.Count > 0){
                    _matchGroupQueue.Enqueue(group);                    
                }

            }
        }
        var queueSize = _matchGroupQueue.Count;
        //return matchGroupsForAllDirections[0][0].Count > 0 || matchGroupsForAllDirections[1][0].Count > 0 || matchGroupsForAllDirections[2][0].Count > 0; //nested list does not always get their own lies, even empty
        return initialQueueSize != queueSize;
    } 


    private async /* void */Task _ActivateMatchedTilesAndCollapseGrid(Queue<List<Vector2I>> matchGroupQueue){ //all this dependency injection is kind of useless if I hard code helper funcions... this is not a pure function
        var group = new List<Vector2I>();  
        if(matchGroupQueue.Count > 0){
            group = matchGroupQueue.Dequeue();  

            var matchQueue = new Queue<Vector2I>(group);

            //fill player energy --- assume that the only tiles that can be matched are skill group tiles --- although maybe walkable tiles might also be matched to gains some special benefit like more placeable walk tiles
            var player = Tiles.FindItemByType(typeof(Playable)) as ReactiveToMatches;
            var tile1 = Tiles.GetItem(group[0]) as SkillBased;
            
            var playerCell = Tiles.GetCellFor(player as Control);
            var isAdjacent = (_tileQuery as Queriable).IsCellAdjacentToLine(playerCell, group);

            //player.ReactToMatchesBySkillType(group, tile1.SkillGroup, isAdjacent);

            await _RunMatchedTileBehaviors(matchQueue); ///make this async so it blanks out tiles before player changes his coordinates and replaces the blanks

            var bpp = 123;

            await player.ReactToMatchesBySkillType(group, tile1.SkillGroup, isAdjacent);     

            bpp = 132;                 
        } 

        await _CollapseTiles();
        var bp = 123;

        //GetTree().CreateTimer(1.5).Timeout += () => { //booooo! Also I can't have these running in parallel
            _ = _FillUpEmptyCells(_spawnWeights, _spawnTiles);   

            Debugging.PrintStackedGridInitials(Tiles.GetGridAs2DList(), 2, 2, "STACKED Grid:");
            bp = 123;
            if(matchGroupQueue.Count > 0){ //I dequeue on every match that's found
                //GetTree().CreateTimer(1).Timeout += () => { //I really need to stop doing this
                    (_tileContainer as Viewable).UpdatePositions(Tiles); //<<<<
                    await _ActivateMatchedTilesAndCollapseGrid(matchGroupQueue);  
                //};
            }else{
                _CheckNewMatchesAndProcess(Tiles); //New <<<<<<<<<<<<<<<<<<<
                if(matchGroupQueue.Count > 0){ //this doesn't make much sense but it kind of does...
                    //GetTree().CreateTimer(1).Timeout += () => { 
                        (_tileContainer as Viewable).UpdatePositions(Tiles); //<<<<
                        await _ActivateMatchedTilesAndCollapseGrid(matchGroupQueue);  
                    //};
                }            
            }                      
        //};



    }


    private /* void */async Task _FillUpEmptyCells(float[] spawnWeights, TileTypes[] spawnTiles){ //this doesn't seem to get all of them...
        var random = new RandomNumberGenerator();
        var xx = 0;
        var yy = 0;
        for(int x=0; x<Tiles.Width; x++){
            for(int y=0; y<Tiles.Height; y++){
                var tileNode = Tiles.GetItem(x, y);
                if(tileNode is Empty && tileNode is not Environmental){
                    var tileType = spawnTiles[random.RandWeighted(spawnWeights)]; 
                    var spawnedTile = (_tileFactory as TileMaking).Create(tileType) as Control;
                    Tiles.SetCell(spawnedTile, x, y);   
                    _AddTile(spawnedTile, new Vector2I(x, y));   
                    xx = x;
                    yy = y;          
                }
            }
        }
        await (Tiles.GetItem(xx, yy) as Creatable).WaitUntilCreated();
    }


    private void _AddTile(Control tile, Vector2I cell){
        (_tileContainer as Viewable).Add(tile, cell);
    }


    private /* void */async Task _RunMatchedTileBehaviors(Queue<Vector2I>matchQueue){
        if(matchQueue.Count>0){
            await _ActivateMatchedTileAndRemove(matchQueue); 

            await _RunMatchedTileBehaviors(matchQueue); 
        }     
    }    


    private /* void */async Task _ActivateMatchedTileAndRemove(Queue<Vector2I> matches){ //need to make sure this is awaited
        if(matches.Count > 0){ 
            var cell = matches.Dequeue();

            var tile = Tiles.GetItem(cell);
            Tiles.SetCell(
                (Control) (_tileFactory as TileMaking).Create(TileTypes.Blank), 
                cell
            );
            if(tile is Matchable matchable){
                matchable.BeginPostMatchProcessDependingOnPlayerPosition(cell, null, false);
                //await ToSignal(tile, "Removed");
                await (tile as Removable).WaitForRemoved();
            }
            await _ActivateMatchedTileAndRemove(matches);
        }      
    }


    // private Grid<Control> _SwapCellsInTemporaryGrid(Control sourceTile, Control targetTile, Grid<Control> grid){ //THIS IS BAD, IT MAKES A NEW GRID INSTANCE THAT IT DOES NOT SHARE WITH OTEHR MODELS
    //     var probeGrid = grid.Clone();
    //     var source = grid.GetCellFor(sourceTile);
    //     var target = grid.GetCellFor(targetTile);

    //     probeGrid.SetCell(targetTile, source.X, source.Y); 
    //     probeGrid.SetCell(sourceTile, target.X, target.Y);   
    //     return probeGrid;  
    // }   


    private void _SwapCells(Control sourceTile, Control targetTile){ //THIS IS BAD, IT MAKES A NEW GRID INSTANCE THAT IT DOES NOT SHARE WITH OTEHR MODELS
        var source = Tiles.GetCellFor(sourceTile);
        var target = Tiles.GetCellFor(targetTile);

        Tiles.SetCell(targetTile, source.X, source.Y); 
        Tiles.SetCell(sourceTile, target.X, target.Y);  
    }  


    private void _FindMatchingGroupsInLine(List<Vector2I> line, List<List<Vector2I>> matchGroups, Grid<Control> grid){
        var matches = new List<Vector2I>();
        for(int a=0; a<line.Count - 2; a++){
            var c1 = line[a];
            var c2 = line[a+1];
            var c3 = line[a+2];	

            if(
                (grid.GetItem(c1.X, c1.Y) as Tile).Type != TileTypes.Blank && //don't think I need this first one
                grid.GetItem(c1.X, c1.Y) is Matchable &&
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
        if(allMatches.Count>0){
            matchGroups.Add([]);
        }
        for(int i=0;i<allMatches.Count;i++)	{
            var cell = allMatches[i];
            var index = matchGroups.Count > 0 ? matchGroups.Count-1 : 0;            
            if(i<allMatches.Count-1){
                var next = allMatches[i+1];
                var tileType = (grid.GetItem(cell.X, cell.Y) as Tile).Type;
                var nextTileType = (grid.GetItem(next.X, next.Y) as Tile).Type;

                if(tileType == nextTileType){
                    matchGroups[index].Add(cell);
                }else{
                    matchGroups.Add([]);
                    index++;
                    matchGroups[index].Add(cell);
                }
            }else{//reached end, assume last tile is also a valid match
                matchGroups[index].Add(cell);
            }
        }
    }    


    private List<List<Vector2I>> _FindMatchingGroupsNorthWest(Grid<Control> grid){
        var matchGroups = new List<List<Vector2I>>();
        for(int x=1; x<=grid.Height + 1; x++){		 //!!!! add + 1	
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
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            _FindMatchingGroupsInLine(diagonal, matchGroups, grid);
            var bp = 324;            
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsNorthEast(Grid<Control> grid){
        var matchGroups = new List<List<Vector2I>>(){new List<Vector2I>()};
        for(int x=1; x<=grid.Height + 1; x++){	// !!! add + 1	 	
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
                ){
                    diagonal.Add(new Vector2I(xx, yy)); 
                }										
            }
            _FindMatchingGroupsInLine(diagonal, matchGroups, grid);
        }	
        return matchGroups;		
    }


    private List<List<Vector2I>> _FindMatchingGroupsVertical(Grid<Control> grid){

        var matchGroups = new List<List<Vector2I>>(){new List<Vector2I>()};   
        for(int x=0; x<grid.Width; x++){ 
            var line = new List<Vector2I>();        
            for(int y=0; y<grid.Height; y++){  
                var tile = grid.GetItem(x, y); 
                if(
                    tile != null &&
                    tile is Tile
                ){
                    line.Add(new Vector2I(x, y)); 
                } 
            }
            _FindMatchingGroupsInLine(line, matchGroups, grid);
        } 
        return matchGroups;    
    }  


    private /* void */ async Task _CollapseTiles(){ //will only fall downward. Can pass through solids.
        var collapsing = true;
        var pathGrid = new Grid<Array<Vector2I>>(Tiles.Width, Tiles.Height);
        var list3D = pathGrid.GetGridAs2DList();     

        var originalGrid = Tiles.Clone();
        while(collapsing){
            collapsing = false;
            for(int x=0; x<Tiles.Width; x++){			
                for(int y=Tiles.Height-1; y>=0; y--){
                    var bottom = Hex.FindBottomClamped(new Vector2I(x, y), Tiles.Width, Tiles.Height);  
                    if(bottom.X>=0 && bottom.Y>=0){
                        if(Tiles.GetItem(x, y) is Collapsable collapsable){
                            //if(Tiles.GetItem(bottom.X, bottom.Y) is not Immobile){
                            if(Tiles.GetItem(bottom.X, bottom.Y) is not Permeable){
                                collapsing = _FallToLowerCellAndStorePath(x, y, bottom, list3D, collapsing, originalGrid);
                                var bp = 123;
                            }else{
                                var contiguousSolidCells = _FindContiguousImmovableCellsFurtherDownInColumn(x, bottom);

                                if(contiguousSolidCells.Count>0){
                                    var lastSolidCell = contiguousSolidCells.Last(); 
                                    var newBottom = Hex.FindBottom(lastSolidCell);
                                    if(newBottom.X>=0 && newBottom.Y>=0){
                                        collapsing = _FallToLowerCellAndStorePath(x, y, newBottom, list3D, collapsing, originalGrid);  
                                        var bp = 345;                                     
                                    }                               
                                }                        
                            }
                        }                         
                    }                  
                }
            }   
        }
        var bp2 = 12344;

        /* _ = */await MoveTilesOnTheirPaths(list3D, originalGrid); //not sure if this awaits the move signal...

        var bppp = 1232;
    }   


    private /* void */ async Task MoveTilesOnTheirPaths(List<List<Array<Vector2I>>> pathGrid, Grid<Control> originalGrid){
        var aa = 0;
        var bb = 0;
        for(int a=0;a<pathGrid.Count;a++){
            for(int b=0;b<pathGrid[0].Count;b++){    
                if(pathGrid[a][b] != null){
                    pathGrid[a][b] = Collections.RemoveDuplicates(pathGrid[a][b]);  
                    var path = pathGrid[a][b];                     
                    var tile = originalGrid.GetItem(a, b); 
                    if(tile != null && tile is Collapsable && tile is Movable movable){
                        path.Reverse(); //because I loop columns bottom-up when processing the paths
                        movable.MoveOnPath(new Stack<Vector2I>(path));
                        aa = a;
                        bb = b;
                    }                    
                }
            }
        } 

        var bp = 1123;

        await (originalGrid.GetItem(aa, bb) as Movable).WaitUntilMoved(); //this is dicey, maybe not even all tiles move...    

        bp = 345;  
    }


    private bool _FallToLowerCellAndStorePath(int x, int y, Vector2I bottom, List<List<Array<Vector2I>>> path3DList, bool collapsing, Grid<Control> originalGrid){
        var collapsable = Tiles.GetItem(x, y);
        var lowerTile = Tiles.GetItem(bottom.X, bottom.Y);  
        var originalCell = originalGrid.GetCellFor(collapsable);
        var xx = originalCell.X;
        var yy = originalCell.Y;
        if(lowerTile is Empty){
            Tiles.SetCell(collapsable as Control, bottom.X, bottom.Y);
            Tiles.SetCell(
                (Control) (_tileFactory as TileMaking).Create(TileTypes.Blank), 
                x, 
                y
            );
            if(collapsable is Movable movable){
                if(path3DList[xx][yy] == null){
                    path3DList[xx][yy] = [];                        
                }
                path3DList[xx][yy].Add(bottom);
            } 
            return true;
        }        
        return collapsing;
    }   


    private List<Vector2I> _FindContiguousImmovableCellsFurtherDownInColumn(int column, Vector2I bottom){
        var contiguousSolidCells = new List<Vector2I>();
        for(int i=bottom.Y; i<Tiles.Height; i++){
            var checkedTile = Tiles.GetItem(column, i);
            //if(checkedTile is Immobile || checkedTile is Agentive){ 
            if(checkedTile is Permeable){ 
                contiguousSolidCells.Add(new Vector2I(column, i));
            }else{
                break;
            }
        } 
        return contiguousSolidCells;       
    }      
}