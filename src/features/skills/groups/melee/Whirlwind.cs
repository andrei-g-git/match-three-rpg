using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Godot;
using Skills;
using Tiles;

public partial class Whirlwind : Node, Skill, WithTileRoot, AccessableBoard, Traversing
{
	[Export] private Node _omniCharge;
	[Export] private Node _damageCalculator;
	
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
			(_omniCharge as WithTileRoot).TileRoot = value;
			(_damageCalculator as WithTileRoot).TileRoot = value;
	}}
	private Node _board;
    public Node Board { 
		get => _board; 
		set{
			(_omniCharge as AccessableBoard).Board = value;
	}}


	public void OnFinishedTransfering(){
		//TileRoot.EmitSignal("FinishedTransfering"); //Not great ... not great
		(TileRoot as Player.Manager).EmitTransferFinished();
	}

	public void OnFinishedPath(){
		(TileRoot as Player.Manager).EmitPathFinished();
	}

    public void ProcessPath(List<Vector2I> path){
        (_omniCharge as Traversing).ProcessPath(path);
    }

    public async Task ProcessPath(List<Vector2I> path, bool testOverload){
        await (_omniCharge as Traversing).ProcessPath(path, true);
    }
}