using Board;
using Common;
using Godot;
using Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;

public partial class Turns : Node, Sequential, Initializable, /* AccessableBoard, */ WithTiles
{
    //public Node Board {private get; set;}
    public Grid<Control> Tiles { get; set; }
	public Control CurrentActor{get; private set;}
	private Queue<Control> _turnQueue;
	private List<Control> _actors;


    public void Initialize(){
		_actors = Tiles.FindAllItemsOfType(typeof(Agentive));
		ArrangeActorsBySpeed();
		_turnQueue = new Queue<Control>();
		PopulateQueue();
		var bp = 123;
	}

	public void AdvanceTurn(){
		//(inputBlocker as FlickableInput).BlockInput(); //players can swithc it on locally with a passed reference
		UpdateActors();
		CurrentActor = _turnQueue.Dequeue();
		_turnQueue.Enqueue(CurrentActor);
		// new
		// foreach(var actor in _actors){ //I don't need to do this, plus, EndTurn is connected to this method wthich causes a stack overflow
		// 	(actor as TurnBased).EndTurn();
		// }
		//
		(CurrentActor as TurnBased).BeginTurn();
		GD.Print((CurrentActor as Tile).Type.ToString(), "'s turn!");
		var bp = 123;
	}

	public bool IsPlayerTurn(){
		return CurrentActor as Playable != null;
	}

	public void AddActor(Control actor){
		_actors.Add(actor); //might need to add to turnQueue too
		UpdateActors();
	}

	public void RemoveActor(Control actor){
		_actors.Remove(actor);
		// //test
		// bool foundSameReference = _turnQueue.Any(item => ReferenceEquals(item, actor));
		// // or
		// var matching = _turnQueue.Where(item => ReferenceEquals(item, actor)).ToList();
		// GD.Print($"found same reference: {foundSameReference}  or  mathich size:  {matching.Count}");

		//_turnQueue = new Queue<Control>(_turnQueue.Where(item => !ReferenceEquals(item, actor)));
		_turnQueue = new Queue<Control>(_turnQueue.Where(item => !EqualityComparer<Control>.Default.Equals(item, actor)));		
		UpdateActors();
	}

	private void UpdateActors(){ //DRY
		foreach(var actor in _actors){
			var cell = Tiles.GetCellFor(actor);
			if(cell.X < 0 || cell.Y < 0){
				_actors.Remove(actor);
				_turnQueue = new Queue<Control>(_turnQueue.Where(_actor => _actor != actor));
			}
	
		}
	}

	private void PopulateQueue(){
		foreach(var actor in _actors){
			var cell = Tiles.GetCellFor(actor);
			if(cell.X < 0 || cell.Y < 0){
				_actors.Remove(actor);
			}
			_turnQueue.Enqueue(actor);		
		}
	}	

	private void ArrangeActorsBySpeed(){
		_actors = _actors
			.OrderBy(actor => 
				(actor as DerivableStats)?.Speed ??
				(actor as WithSpeed)?.Speed ??
				0//default
			)
			.Reverse()
			.ToList();

		_actors.RemoveAll(actor => 
			(actor as DerivableStats)?.Speed == 0 || 
			(actor as WithSpeed)?.Speed == 0);			
	}
}
