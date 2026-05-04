using Godot;
using Stats;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class Bashed : Node, Bashable
{
	[Export] private Node _stats;
	[Export] private Node _activeEffects;

	public /* async Task */ void BeBashed(int enemyStrength, int enemyConstitution){
		(_activeEffects as Effectful).Add(new DefenseDebuff(1, 2));
		var strength = (_stats as WithStrength).Strength;
		var constitution = (_stats as WithConstitution).Constitution;
		if(strength < enemyStrength || constitution < enemyConstitution){
			(_activeEffects as Effectful).Add(new Stunned(1));
		}
	}
}
