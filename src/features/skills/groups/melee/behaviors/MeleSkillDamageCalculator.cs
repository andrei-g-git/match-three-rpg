using Godot;
using Inventory;
using Stats;
using System;
using Tiles;

public partial class MeleSkillDamageCalculator : Node, CalculatableDamage, WithTileRoot
{
	[Export(PropertyHint.Range, "0,10,0.1")] private float _skillMultiplier;
	[Export] private float _momentumSubdivider;
	[Export] private float _strengthSubdivider;
    public Control TileRoot{get;set;}

    public int CalculateDamage() //don't like this...
    {
        throw new NotImplementedException();
    }

    public int CalculateDamageFromMomentum(int tilesCovered){ 
		int weaponDamage = (TileRoot as StatBasedGear).GetTotalGearBaseDamage();
		int strength = (TileRoot as Attributive).Strength;
		var momentumMultiplier = 1 + (float) tilesCovered/_momentumSubdivider;
		var strengthMultiplier = 1 + (float) strength/_strengthSubdivider; //this should be a weapon stat (and they should have an agility bonus as well)
		float rawDamage = weaponDamage * _skillMultiplier * momentumMultiplier * strengthMultiplier;
		GD.Print($"rawDamage : {rawDamage}");
		return (int) Math.Floor(rawDamage);
	}
}