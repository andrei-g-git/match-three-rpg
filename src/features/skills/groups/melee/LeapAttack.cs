using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;

public partial class LeapAttack : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree
{
	[Export] private Node _damageCalculator;
	
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
			//(_damageCalculator as WithTileRoot).TileRoot = value;
	}}
	private Node _board;
    public Node Board {get; set;}

	private AnimationTree _animationTree;
    public AnimationTree AnimationTree {get; set;}	



	public async Task ProcessPathAsync(List<Vector2I> path){
		// var lastTileNodeInPath = (Board as Queriable).GetItemAt(path[^1]);
		// var matchType = (lastTileNodeInPath as Tile).Type;
		var allMatchingTileCells = (Board as Queriable).FindAllTilesOfType(TileTypes.Melee/* matchType */);
		var allMatchingTileCellsWithAdjacentEnemies = new List<Vector2I>();
		foreach(var cell in allMatchingTileCells){
			var neighbours = (Board as Queriable).GetNeighboringTiles(cell);
			var enemies = neighbours.Where(neighbour => (neighbour is Disposition actor && actor.IsEnemy)).ToList();
			// allMatchingTileCellsWithAdjacentEnemies.AddRange(enemies.Select(enemy => (Board as Queriable).GetCellFor(enemy)).ToList()); //wtf
			// allMatchingTileCellsWithAdjacentEnemies = allMatchingTileCellsWithAdjacentEnemies.Distinct().ToList(); //this whole thing looks wasteful...
			if(enemies.Count > 0){
				allMatchingTileCellsWithAdjacentEnemies.Add(cell);				
			}

		}
		
		float previousShortestDistance = 99999f; //this might cause problems
		var closestSameTypeCellWithAdjacentEnemy = allMatchingTileCellsWithAdjacentEnemies[0];
		foreach(var cell in allMatchingTileCellsWithAdjacentEnemies){
			var distance = Hex.FindDistanceBetweenCells(path[^1], cell);
			if(distance < previousShortestDistance){
				previousShortestDistance = distance;
				closestSameTypeCellWithAdjacentEnemy = cell;
			}
		}


		var timeMultipleier = previousShortestDistance >= 99999f ? (int)path.Count/64 : (int)previousShortestDistance/64;

AnimationTree.Set("parameters/JumpMiddleeeeeeeee/TimeScale/scale", (float) 1 / timeMultipleier);

			var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
			playback.Travel("Jump2");	

		await (Board as BoardModel).TransferTileToAsync(TileRoot, closestSameTypeCellWithAdjacentEnemy, timeMultipleier);







// var ap = GetNode<AnimationPlayer>("AnimationPlayer");
// var anim = ap.GetAnimation("jump_middle");
// float baseLen = anim.Length;
// float desired = Mathf.Max(0.001f, desiredSeconds);
// ap.Play("jump_middle");
// ap.PlaybackSpeed = baseLen / desired; // speeds up if <1, slows if >1
	}


	// public void OnFinishedTransfering(){
	// 	//TileRoot.EmitSignal("FinishedTransfering"); //Not great ... not great
	// 	(TileRoot as Player.Manager).EmitTransferFinished();
	// }

	// public void OnFinishedPath(){
	// 	(TileRoot as Player.Manager).EmitPathFinished();
	// }

    public void ProcessPath(List<Vector2I> path){
        
    }


}
