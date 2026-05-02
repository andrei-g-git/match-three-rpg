using Godot;
using Stats;

public class FocusedDefenseDebuff: ActiveEffect
{
    private int _amount = 0;
    public FocusedDefenseDebuff(int maxDuration, int amount){
        MaxDuration = maxDuration;
        _amount = amount;
    }
    public override void ApplyToStats(Node stats){
        if((stats as WithDefense).Defense >= _amount){
            (stats as WithDefense).Defense -= _amount;             
        }
    }
}