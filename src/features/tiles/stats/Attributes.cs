using Godot;
using Stats;
using System;

namespace Stats{}
public class Attributes : Attributive
{
	public int Strength{get;set;}
	public int Agility{get;set;}
	public int Constitution{get;set;}
	public int Intelligence{get;set;}
    public void IncreaseAgility(int amount){
        Agility += amount;
    }

    public void IncreaseConstitution(int amount){
        Constitution += amount;
    }

    public void IncreaseIntelligence(int amount){
        Intelligence += amount;
    }

    public void IncreaseStrength(int amount){
        Strength += amount;
    }

    public void SubtractAgility(int amount){
        Agility -= amount;
    }

    public void SubtractConstitution(int amount){
        Constitution -= amount;
    }

    public void SubtractIntelligence(int amount){
        Intelligence -= amount;
    }

    public void SubtractStrength(int amount){
        Strength -= amount;
    }	

    // public class DerivedStatsSerializable{
    //     public int 
    // }
}    