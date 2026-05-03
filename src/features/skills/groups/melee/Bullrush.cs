using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class Bullrush : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, FilterableSkill
{
	[Export] private Node _straightCharge;
	[Export] private Node _damageCalculator;
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
			(_straightCharge as WithTileRoot).TileRoot = value;
			(_damageCalculator as WithTileRoot).TileRoot = value;
	}}	
	private Node _board;
    public Node Board { 
		get => _board; 
		set{
			(_straightCharge as AccessableBoard).Board = value;
	}}	

	private AnimationTree _animationTree;
    public AnimationTree AnimationTree { 
		private get => _animationTree; 
		set{
			(_straightCharge as StraightCharge).AnimationTree = value; //StraightCharge is not an interface!
	}}	


	public void OnFinishedTransfering(){
		(TileRoot as Player.Manager).EmitTransferFinished();
	}

	public void OnFinishedPath(){
		(TileRoot as Player.Manager).EmitPathFinished();
	}

    public void ProcessPath(List<Vector2I> path){
        (_straightCharge as Traversing).ProcessPath(path);
    }

    public async Task ProcessPathAsync(List<Vector2I> path){
        await (_straightCharge as Traversing).ProcessPathAsync(path);
    }	

    public /* static */ bool CheckIfUsable(List<Vector2I> matchedGroup, SkillNames.SkillGroups skillGroup, Queriable boardQuery){
		var playerPosition = boardQuery.GetPlayerPosition();
        var playerIsAdjacent = boardQuery.IsCellAdjacentToLine(playerPosition, matchedGroup);
		return playerIsAdjacent; //the player is allowed to waste the skill if there's no eligible enemy
    }	
}
