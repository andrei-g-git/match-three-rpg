using Board;
using Godot;
using System;
using System.Threading.Tasks;
using Tiles;

namespace SpawnerOfOrcs;
public partial class Manager : Control, Tile, Permeable, Agentive, TurnBased, CanSpawn, AccessableBoard
{
	[ExportGroup("behaviors")]
	[Export] private Node _spawner;
	[Export] private Node _turn;

	public TileTypes Type => TileTypes.Cart;
	public TileTypes AA => Type; //for debugging
    public Sequential TurnQueue { private get; set; }
	public Node Board {
		set {
			(_spawner as AccessableBoard).Board = value;
	}}

	private int _turnsPassed = 0;


	public override void _Ready(){
		//(_popTweener as Creatable).Pop();

		(_turn as Turn).ConnectRequestedTurnEnd(TurnQueue.AdvanceTurn);
										
	}

    public void AdvanceTurn(){
        TurnQueue.AdvanceTurn();
		_turnsPassed++;
    }

    public void BeginTurn(){
       (_turn as TurnBased).BeginTurn();
    }

    public void EndTurn(){
        (_turn as TurnBased).EndTurn();
    }

    public async Task Spawn(){
        await (_spawner as CanSpawn).Spawn();
    }

}
