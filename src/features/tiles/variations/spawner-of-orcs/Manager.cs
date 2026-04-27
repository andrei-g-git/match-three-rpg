using Board;
using Godot;
using Stats;
using System;
using System.Threading.Tasks;
using Tiles;

namespace SpawnerOfOrcs;
public partial class Manager : Control, Tile, Permeable, Agentive, TurnBased, CanSpawn, AccessableBoard, WithSpeed
{
	[ExportGroup("behaviors")]
	[Export] private Node _spawner;
	[Export] private Node _turn;

	public TileTypes Type => TileTypes.SpawnerOfOrcs;
	public TileTypes AA => Type; //for debugging
    public Sequential TurnQueue { private get; set; } //i don't need this
	public Node Board {
		set {
			(_spawner as AccessableBoard).Board = value;
	}}

    public int Speed { get; set; } = 1; //should get rid of this or at least change it to initiative

    private int _turnsPassed = 0;

    public int Index{get;set;}


	public override void _Ready(){
		//(_popTweener as Creatable).Pop();

		(_turn as Turn).ConnectRequestedTurnEnd(TurnQueue.AdvanceTurn);
										
	}

    public void AdvanceTurn(){ //this shit should be in the spawner ---- actually it's kind of useless...
        //TurnQueue.AdvanceTurn();
		_turnsPassed++;
		if((_turnsPassed % 4) == 0){
			_ = Spawn();
		}
    }

    public void BeginTurn(){
       (_turn as TurnBased).BeginTurn();
		_turnsPassed++;
		if((_turnsPassed % 4) == 0){
			_ = Spawn();
		}	   
    }

    public void EndTurn(){
        (_turn as TurnBased).EndTurn();
    }

    public async Task Spawn(){
        await (_spawner as CanSpawn).Spawn();
    }

}
