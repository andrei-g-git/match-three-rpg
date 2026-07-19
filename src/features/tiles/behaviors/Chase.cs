using System.Linq;
using Board;
using Common;
using Godot;
using Tiles;

public partial class Chase : Node, Pursuing, Mapable, /* WithTiles */AccessableBoard
{
	[Export] private Node _pathfinding;
	[Export] private Control _tileRoot;
	public Tileable Map{private get; set;}
	//public Grid<Control> Tiles {get; set;}
	public Node Board {private get; set;}
	//[Signal] public delegate void TrySwappingEventHandler(Control targetNode);
	[Signal] public delegate void CaughtTargetEventHandler(/* Vector2I target */Control targetNode);


	public void ChaseActor(Vector2I cell){
		//GD.Print("Chasing actor at cell:  ", cell);
		var shortestPath = (_pathfinding as Pathfindable).FindPath(cell);
		GD.Print("shortest path %%%   \n", string.Join("", shortestPath.Select(cell => $"{cell.X}, {cell.Y} |")));
		var next = shortestPath[1]; //shortestPath[0] is the agent, I think
		if(next != cell && shortestPath.Count >= 3){ //otherwise it already reached the actor since index[0] is own self and index[-1] is actor
			var ownCoordinates = Map.PositionToCell(_tileRoot.Position);
			var hasReachedActor = Hex.CheckIfNeighbor(ownCoordinates, cell);
			if(hasReachedActor){
				EmitSignal(SignalName.CaughtTarget, cell); //THIS IS INCONSISTENT, WILL ALLOW ENEMY TO ATTACK AFTER MOVING (not sure if I want that)
			}else if(! _CheckIfEmpty(next)){
				var target = next;
				var targetNode = (Board as Queriable).GetItemAt(new Vector2I(target.X, target.Y));//tiles[target.X][target.Y];
				//EmitSignal(SignalName.TrySwapping, targetNode/* next */);	

				(Board as Organizable).MoveBySwapping(_tileRoot, targetNode); //new

				(Board as MatchableBoard).ProcessMatchesWithoutEffects();
				
			}else{
				(Board as Organizable).MovePiece(_tileRoot, next.X, next.Y); //meh ... if it doens't work I'll replace it
			}

		}else{ //lol wtf... whatever... too lazy
			if(next == cell){
				var targetNode = (Board as Queriable).GetItemAt(new Vector2I(cell.X, cell.Y));//tiles[cell.X][cell.Y];
				EmitSignal(SignalName.CaughtTarget, targetNode/* cell */);
			}
		}
	}	


	private bool _CheckIfEmpty(Vector2I cell){
		var x = cell.X;
		var y = cell.Y;
		return (Board as Queriable).GetItemAt(new Vector2I(x, y)) is Empty;		
	}	
}
