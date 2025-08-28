using Godot;
using Stats;
using System;

public partial class PlayerStats : Node, StatBased, Attributive, DerivableStats //this is kind of a pointless middle man, I can just use the player manager to encapsulate
{
	[Export] private Node _derivedStats;
    public Attributive Attributes{get;set;}
	public DerivableStats DerivedStats => _derivedStats as DerivableStats;
	public int Strength {get;}
	public int Agility {get;}
	public int Constitution {get;}
	public int Intelligence {get;}
    public int Health {get => DerivedStats.Health; set => DerivedStats.Health = value;}
    public int Energy {get => DerivedStats.Energy; set => DerivedStats.Energy = value;}

    public override void _Ready()
	{
	}


	public void IncreaseStrength(int amount){
		Attributes.IncreaseStrength(amount);
	}
	public void IncreaseAgility(int amount){
		Attributes.IncreaseAgility(amount);
	}
	public void IncreaseConstitution(int amount){
		Attributes.IncreaseConstitution(amount);
	}
	public void IncreaseIntelligence(int amount){
		Attributes.IncreaseIntelligence(amount);
	}

	public void SubtractStrength(int amount){
		Attributes.SubtractStrength(amount);
	}
	public void SubtractAgility(int amount){
		Attributes.SubtractAgility(amount);
	}
	public void SubtractConstitution(int amount){
		Attributes.SubtractConstitution(amount);
	}
	public void SubtractIntelligence(int amount){
		Attributes.SubtractIntelligence(amount);
	}	


	public int GetMaxEnergy(){
		return DerivedStats.GetMaxEnergy();
	}

    public int GetMaxHealth(){
        return DerivedStats.GetMaxHealth();
    }

}
