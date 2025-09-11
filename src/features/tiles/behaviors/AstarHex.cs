using System.Collections.Generic;
using Godot;

public partial class AstarHex: Node
{
    public List<Vector2I> _path = [];
    public AStar2D _astar;

    public override void _Ready(){
        _astar = new AStar2D();
    }

    public void AddHexPoint(Vector2I cell){
        var currentId = _path.Count;
        _astar.AddPoint(currentId, cell);
        _path.Add(cell);
    }


    public void ConnectHexPoint(Vector2I cell){
        var x = cell.X;
        var y = cell.Y; 

        ConnectOnePoint(cell, Hex.FindTop(cell));
        ConnectOnePoint(cell, Hex.FindTopRight(cell));
        ConnectOnePoint(cell, Hex.FindBottomRight(cell));
        ConnectOnePoint(cell, Hex.FindBottom(cell));
        ConnectOnePoint(cell, Hex.FindBottomLeft(cell));
        ConnectOnePoint(cell, Hex.FindTopLeft(cell));        
    }   


    private void ConnectOnePoint(Vector2I cell, Vector2I neighbor){
        var cellId = _path.IndexOf(cell);
        var neighborId = _path.IndexOf(neighbor);
        if(_astar.HasPoint(neighborId)){
            _astar.ConnectPoints(cellId, neighborId); 
        }
    }     


    public List<Vector2I> GetPath(Vector2I source, Vector2I target){
        var vectorPath = _astar.GetPointPath(
            _path.IndexOf(source),
            _path.IndexOf(target)
        );
        List<Vector2I> vectorIntegerPath = [];
        foreach(var cell in vectorPath){
            vectorIntegerPath.Add((Vector2I) cell);
        }
        return vectorIntegerPath;
    }        
}