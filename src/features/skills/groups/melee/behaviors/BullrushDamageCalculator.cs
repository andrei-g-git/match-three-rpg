using Godot;
using Inventory;
using Stats;
using System;
using Tiles;

public partial class BullrushDamageCalculator : Node, CalculatableDamage, WithTileRoot
{
    public Control TileRoot{get;set;}

    public int CalculateDamage()
    {
        throw new NotImplementedException();
    }

    public int CalculateDamageFromMomentum(int tilesCovered){ //should make this standard, export the skill multiplier and maybe other multipleiers and call it skillDamageCalculator or something
		float skillMultiplier = 1.5f;
		int weaponDamage = (TileRoot as StatBasedGear).GetTotalGearBaseDamage();
		int strength = (TileRoot as Attributive).Strength;
		var momentumMultiplier = 1 + (float) tilesCovered/10;
		var strengthMultiplier = 1 + (float) strength/20;
		float rawDamage = weaponDamage * skillMultiplier * momentumMultiplier * strengthMultiplier;
		GD.Print($"rawDamage : {rawDamage}");
		return (int) Math.Floor(rawDamage);
	}
}

