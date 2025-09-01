using Godot;
using Stats;
using System;
using Tiles;

public partial class DamageCalculator : Node, CalculatableDamage, WithTileRoot
{
	[Export] private Node skill;
    public Control TileRoot{get;set;}


	public int CalculateDamageFromMomentum(int tilesCovered){
		float skillMulti = 1.2f;//(skill as Skill.IBehavior).DamageMultiplier; 
		int weaponDamage = 4;//(TileRoot as CombatItemry).GetRightHandDamage(); //won't be dual wielding and 2 handers are placed on the right hand 
		int strength = (TileRoot as Attributive).Strength;
		float rawDamage = weaponDamage * skillMulti * (1 + tilesCovered/10) * (1 + strength/20);
		return (int) Math.Floor(rawDamage);
	}
}