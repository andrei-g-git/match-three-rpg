using Board;
using Godot;
using Skills;
using Tiles;

public partial class Whirlwind : Node, Skill, WithTileRoot, AccessableBoard
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
}