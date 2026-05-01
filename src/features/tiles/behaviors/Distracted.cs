using Godot;
using Stats;
using System;
using Tiles;

public partial class Distracted : Node, Distractable
{
	[Export] private Node _stats;

    public void BecomeDistracted(int distractingActorIntelligence){
        var intelligence = (_stats as WithIntelligence).Intelligence;
		if(intelligence < distractingActorIntelligence){

		}else{
			//emit signal that it's on to you or something
		}
    }

}
