using Godot;
using Stats;

public class DefenseDebuff: ActiveEffect
{
    private int _amount = 0;
    public DefenseDebuff(int maxDuration, int amount){
        MaxDuration = maxDuration;
        _amount = amount;
        Type = Effects.LoweredDefense;
    }
    public override void ApplyToStats(Node stats){
        if((stats as WithDefense).Defense >= _amount){
            (stats as WithDefense).Defense -= _amount;             
        }
    }
}