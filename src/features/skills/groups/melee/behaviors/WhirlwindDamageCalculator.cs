using Godot;
using Inventory;
using Stats;
using System;
using Tiles;

public partial class WhirlwindDamageCalculator : Node, CalculatableDamage, WithTileRoot
{
	[Export] private Node skill;
    public Control TileRoot{get;set;}

    public int CalculateDamage()
    {
        throw new NotImplementedException();
    }

    public int CalculateDamageFromMomentum(int tilesCovered){
		float skillMultiplier = 1.2f;//(skill as Skill.IBehavior).DamageMultiplier; 
		int weaponDamage = (TileRoot as StatBasedGear).GetTotalGearBaseDamage();//4;//(TileRoot as CombatItemry).GetRightHandDamage(); //won't be dual wielding and 2 handers are placed on the right hand 
		int strength = (TileRoot as Attributive).Strength;
		var momentumMultiplier = 1 + (float) tilesCovered/10;
		var strengthMultiplier = 1 + (float) strength/20;
		float rawDamage = weaponDamage * skillMultiplier * momentumMultiplier * strengthMultiplier;/* (float) (1 + tilesCovered/10) * (float) (1 + strength/20) */;
		GD.Print($"rawDamage : {rawDamage}");
		return (int) Math.Floor(rawDamage);
	}

	
}