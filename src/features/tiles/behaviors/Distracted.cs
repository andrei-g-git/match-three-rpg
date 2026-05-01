using Godot;
using Stats;
using System;
using Tiles;

public partial class Distracted : Node, Distractable
{
	[Export] private Node _stats;
	[Export] private Node _activeEffects;

    public void BecomeDistractedFor(int turns, int distractingActorIntelligence){
        var intelligence = (_stats as WithIntelligence).Intelligence;
		if(intelligence < distractingActorIntelligence){
			(_activeEffects as Effectful).Add(new Stunned(turns));
			GD.Print("this actor is now stunned");
		}else{
			//emit signal that it's on to you or something
			GD.Print("this actor is too smart to fall for cheap tricks");
		}
    }

}
