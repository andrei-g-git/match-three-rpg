using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using Tiles;
using Util;

public partial class MatchingRange : Node, MatchableBounds
{
	[Export] private int _matchRange;
	[Export] private Control _tileRoot;
    public int MatchRange => _matchRange;


    public bool IsMatchGroupInRange(Queue<List<Vector2I>> matchGroupQueue, Grid<Control> board){
        var ownCell = board.GetCellFor(_tileRoot);
		foreach(var group in matchGroupQueue){
            foreach(var cell in group){
                if(ownCell.DistanceTo(cell) <= _matchRange){
                    return true;
                }                
            }
		}
		return false;
    }

}