using Godot;

public class Stunned : ActiveEffect
{
    public Stunned(int maxDuration){
        MaxDuration = maxDuration;
    }
    public override void ApplyToStats(Node stats){
        //throw new System.NotImplementedException();
    }
}