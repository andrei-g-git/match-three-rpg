using Godot;
using System;
using Tiles;

public partial class Engagement : Node, Engageable
{
	[Export] private Control _tileRoot;
	public void ProcessEngagementBy(Control engagingActor){
		if(engagingActor is Playable player){
			(player as Offensive).Attack(_tileRoot);
		}
	}
}
