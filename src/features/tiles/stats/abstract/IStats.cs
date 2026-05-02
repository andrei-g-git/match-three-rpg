using Godot;
using System;
using System.Collections.Generic;

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
	public int Health{get;set;}
	public int Energy{get;set;}
	public int Speed{get;set;}
	public int Defense{get;set;}
	public int GetMaxEnergy();
	public int GetMaxHealth();
	public int GetSpeed();
	public int GetDefense();
}

public interface Classy{
	public Classes Class{get;set;}
}

public interface WithHealth{
    public int Health{get;set;} 
    public int MaxHealth{get;}
}

public interface WithDefense{
	public int Defense{get;set;}
}

public interface WithDamage{
	public int Damage{get;set;}
}

public interface WithSpeed{
	public int Speed{get;set;}
}

public interface WithStrength{
	public int Strength{get;set;}
}

public interface WithIntelligence{
	public int Intelligence{get;set;}
}

public interface WithAgility{
	public int Agility{get;set;}
}

// public interface Effectful{ //nah this is a waste of a perfectly good interface name that I can replace with an abstract class, which is serializable
// 	public int MaxDuration{get;set;}
// 	public int TurnsLeft{get;set;}
// 	public Effects Type{get;set;}
// 	public void ApplyToStats(Node stats); //this means enemies must have the same stat objects as the player, which sucks. Also some effects don't need to call this so interface segregation is broken
// }

public interface Effectful{
	public List<ActiveEffect> Effects {get;set;}
	public void Add(ActiveEffect effect);
	public void Remove(ActiveEffect effect);
	public void RemoveAt(int index);
	public void RemoveAllOfType<T>() where T: ActiveEffect;
	public void UpdateDurations();
	public ActiveEffect GetEffect(Effects effectName);
	public int GetEffectDuration(Effects effectName);
	public void ApplyAll();
	public bool CheckIfStunned();
}

public interface WithEffects{
	public void Add(ActiveEffect effect);
	public void Remove(ActiveEffect effect);	
	public void RemoveAllOfType<T>() where T: ActiveEffect;
}
