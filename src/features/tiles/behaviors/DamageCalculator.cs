using Godot;
using Inventory;
using Stats;
using System;
using Tiles;

public partial class DamageCalculator : Node, CalculatableDamage //this is a crappy interface, I will need to expand functionality in the future for damage calculation and there will be very poor interface segregation
{
    //[Export] private Node _tileRoot;
	[Export] private Node _equipmentModel;
	[Export] private Node _statsModel;

    public int CalculateDamage(){
		int weaponDamage = (_equipmentModel as StatBasedGear).GetTotalGearBaseDamage();
		int strength = (_statsModel as Attributive).Strength;
		var strengthMultiplier = 1 + (float) strength/20;
		float rawDamage = weaponDamage * strengthMultiplier;
		return (int) Math.Floor(rawDamage);
    }

    public int CalculateDamageFromMomentum(int tilesCovered)
    {
        throw new NotImplementedException();
    }
}