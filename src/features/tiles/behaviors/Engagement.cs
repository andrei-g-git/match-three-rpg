using Board;
using Godot;
using System;
using Tiles;

public partial class Engagement : Node, Engageable, AccessableBoard
{
	[Export] private Control _tileRoot;
	public Node Board{private get;set;}

	public void ProcessEngagementBy(Control engagedNode){
		if(engagedNode is Playable player){
			(player as Offensive).Attack(_tileRoot);
		}else if(engagedNode is Swappable){
			(Board as Organizable).MoveBySwapping(_tileRoot, engagedNode);
		}
	}
}
