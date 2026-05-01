using Godot;
using Stats;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ActiveEffects : Node, Effectful
{
	[Export] private Node _stats;

	public List<ActiveEffect> Effects {get;set;} = []; //ActiveEffect is not an interface (but it is abstract). I need this for serialization ..but since activeEffect is already an abstraction ... i don't actually need an interface...

	public void Add(ActiveEffect effect){
		Effects.Add(effect);
	}

    public ActiveEffect GetEffect(Effects effectName){
        return Effects.Find(effect => effect.Type == effectName);
    }

    public int GetEffectDuration(Effects effectName){
		var effect = Effects.Find(effect => effect.Type == effectName);
        if(effect != null){
			return effect.TurnsLeft;
		}
		return -1;
    }


    public void Remove(ActiveEffect effect){
        Effects.Remove(effect);
    }

    public void RemoveAt(int index){
        Effects.RemoveAt(index);
    }

    public void UpdateDurations(){
        foreach(var effect in Effects){
			effect.TurnsLeft--;
			if(effect.TurnsLeft < 0){
				effect.TurnsLeft = 0;
			}
		}
    }

	public void ApplyAll(){
        foreach(var effect in Effects){
			if(effect.TurnsLeft > 0){ //this method sould run after UpdateDurations, but it's better to make sure
				effect.ApplyToStats(_stats);				
			}
		}
	}

}
