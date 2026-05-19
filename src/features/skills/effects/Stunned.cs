using System;
using Godot;
using Stats;

public class Stunned : ActiveEffect
{
    public Stunned(int maxDuration){
        MaxDuration = maxDuration;
        Type = Effects.Distracted;
    }
    public override void ApplyToStats(Node stats){
        //throw new System.NotImplementedException();
    }

    public override void RemoveFromStats(Node stats)
    {
        //throw new NotImplementedException();
    }
}