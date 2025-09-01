using System.Collections.Generic;
using Board;
using Godot;
using Skills;
using Tiles;

public partial class Whirlwind : Node, Skill, WithTileRoot, AccessableBoard, Traversing
{
	[Export] private Node _omniCharge;
	
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
			(_omniCharge as WithTileRoot).TileRoot = value;
	}}
	private Node _board;
    public Node Board { 
		get => _board; 
		set{
			(_omniCharge as AccessableBoard).Board = value;
	}}


	public void OnFinishedTransfering(){
		TileRoot.EmitSignal("FinishedTransfering"); //Not great ... not great
	}

    public void ProcessPath(List<Vector2I> path){
        (_omniCharge as Traversing).ProcessPath(path);
    }
}