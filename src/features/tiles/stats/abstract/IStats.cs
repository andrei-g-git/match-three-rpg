using Godot;
using System;

namespace Stats;
public interface StatBased{ //not sure I need this, I can just encapsulate it's constituents
    public Attributive Attributes{get;set;}
	public DerivableStats DerivedStats{get;}
}

public interface Attributive{
	public int Strength {get;}
	public int Agility {get;}
	public int Constitution {get;}
	public int Intelligence {get;}

	public void IncreaseStrength(int amount);
	public void IncreaseAgility(int amount);
	public void IncreaseConstitution(int amount);
	public void IncreaseIntelligence(int amount);

	public void SubtractStrength(int amount);
	public void SubtractAgility(int amount);
	public void SubtractConstitution(int amount);
	public void SubtractIntelligence(int amount);	
}

public interface DerivableStats{
	public Attributive Attributes{set;}
	public int GetMaxEnergy();
}
