using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class Charge : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree
{
	[Export] private Node _straightCharge;
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
			(_straightCharge as WithTileRoot).TileRoot = value;
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
}
