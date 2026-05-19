using Godot;
using Stats;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class Bashed : Node, Bashable
{
	[Export] private Node _stats;
	[Export] private Node _activeEffects;
	[Export] private Node _effectTween;

	public /* async Task */ async void BeBashed(int enemyStrength, int enemyConstitution){
		(_activeEffects as Effectful).Add(new DefenseDebuff(1, 2));
		await ToSignal(_effectTween, "EffectDisplayed"); //or I could just await Tween.SignalName.Finished
		//await ToSignal(_effectTween, Tween.SignalName.Finished);
		var strength = (_stats as WithStrength).Strength;
		var constitution = (_stats as WithConstitution).Constitution;
		if(strength < enemyStrength || constitution < enemyConstitution){
			(_activeEffects as Effectful).Add(new Stunned(1));
		}
	}


}
