using Board;
using Godot;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class Engagement : Node, Engageable, AccessableBoard
{
	[Export] private Control _tileRoot;
	// [Export] private Node _offense;
	// [Export] private Node _skillsModel;
	public Node Board{private get;set;}
	//[Signal] public delegate void AttackingEventHandler(Control targetNode);

	//TODO: should check that engaging and this nodes are not the same type, since enemies can otherwise attack eachother if they find themselves adjacent and the pathfinding is buggy
	public /* async Task */ void ProcessEngagementBy(Control engagingNode){ //handled by signal, can't be called asynchronously
		if(engagingNode is Agentive actor){
			if(actor is Disposition disposition && disposition.IsEnemy && disposition.IsAggressive){

				GD.Print($"{engagingNode.Name} with index {(engagingNode as Agentive).Index} will attack {_tileRoot.Name} with index {(_tileRoot as Agentive).Index}");
				(engagingNode as Offensive).Attack(_tileRoot);
				var bp = 234;
			}
		}else if(engagingNode is Swappable){
			/* await  */_ = (Board as Organizable).MoveBySwapping(_tileRoot, engagingNode);
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
