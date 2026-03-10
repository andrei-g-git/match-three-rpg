using Board;
using Godot;
using System;
using Tiles;

public partial class Engagement : Node, Engageable, AccessableBoard
{
	[Export] private Control _tileRoot;
	// [Export] private Node _offense;
	// [Export] private Node _skillsModel;
	public Node Board{private get;set;}
	//[Signal] public delegate void AttackingEventHandler(Control targetNode);

	public void ProcessEngagementBy(Control engagingNode){ 
		//if(engagingNode is Playable player){ //guess I won't be using this component on the player tile then
			//(player as Offensive).Attack(_tileRoot);
		if(engagingNode is Agentive actor){
			if(actor is Disposition disposition && disposition.IsEnemy && disposition.IsAggressive){
				//EmitSignal(SignalName.Attacking, engagingNode); //no, the node engaging this node is attacking


				(engagingNode as Offensive).Attack(_tileRoot);
				//(engagingNode as Engageable).EngageTarget(_tileRoot);
			}
		}else if(engagingNode is Swappable){
			(Board as Organizable).MoveBySwapping(_tileRoot, engagingNode);
		}
	}

	// public void EngageTarget(Control target){
	// 	//should change this stuff
	// 	if(target is Playable player){
	// 		(_offense as Offensive).Attack(target);
	// 	}
	// 	else{
	// 		var skill = (_skillsModel as )
	// 	}
	// }
}
