using Board;
using Common;
using Godot;
using Tiles;

public partial class Chase : Node, Pursuing, Mapable, WithTiles
{
	[Export] private Node _pathfinding;
	[Export] private Control _tileRoot;
	public Tileable Map{private get; set;}
	public Grid<Control> Tiles {get; set;}
	[Signal] public delegate void TrySwappingEventHandler(Control targetNode);
	[Signal] public delegate void CaughtTargetEventHandler(Vector2I target);


	public void ChaseActor(Vector2I cell){
		GD.Print("Chasing actor at cell:  ", cell);
		// var shortestPath = (pathfinding as IPathfinding).FindPath(cell);
		// GD.Print("shortest path   \n", shortestPath);
		// if(shortestPath[1] != cell && shortestPath.Count >= 3){ //otherwise it already reached the actor since index[0] is own self and index[-1] is actor
		// 	var ownCoordinates = map.PositionToCell(tileRoot.Position);
		// 	var hasReachedActor = Hex.CheckIfNeighbor(ownCoordinates, cell);
		// 	if(hasReachedActor){
		// 		EmitSignal(SignalName.CaughtTarget, cell);
		// 	}else{
		// 		var target = shortestPath[1];
		// 		var targetNode = tiles[target.X][target.Y];
		// 		EmitSignal(SignalName.TrySwapping, targetNode/* shortestPath[1] */);				
		// 	}

		// }else{ //lol wtf... whatever... too lazy
		// 	if(shortestPath[1] == cell){
		// 		var targetNode = tiles[cell.X][cell.Y];
		// 		EmitSignal(SignalName.CaughtTarget, /* targetNode */cell);
		// 	}
		// }
	}	
}
