using Board;
using Godot;
using System;
using Tiles;

public partial class Engagement : Node, Engageable, AccessableBoard
{
	[Export] private Control _tileRoot;
	public Node Board{private get;set;}
	//[Signal] public delegate void AttackingEventHandler(Control targetNode);

	public void ProcessEngagementBy(Control engagingNode){ 
		//if(engagingNode is Playable player){ //guess I won't be using this component on the player tile then
			//(player as Offensive).Attack(_tileRoot);
		if(engagingNode is Agentive actor){
			if(actor is Disposition disposition && disposition.IsEnemy && disposition.IsAggressive){
				//EmitSignal(SignalName.Attacking, engagingNode); //no, the node engaging this node is attacking

				(engagingNode as Offensive).Attack(_tileRoot);
			}
		}else if(engagingNode is Swappable){
			(Board as Organizable).MoveBySwapping(_tileRoot, engagingNode);
		}
	}
}
