using Godot;
using Inventory;
using Levels;
using Skills;
using Stats;
using System;
using System.Collections.Generic;
using Tiles;

public partial class MeleSkillDamageCalculator : Node, CalculatableDamage, WithTileRoot, WithRoomModifiers
{
	[Export(PropertyHint.Range, "0,10,0.1")] private float _skillMultiplier;
	[Export(PropertyHint.Range, "0,10,0.1")] private float _verticalMultiplier;
	[Export] private float _momentumSubdivider;
	[Export] private float _strengthSubdivider;
    public Control TileRoot{get;set;}
	public List<string> RoomModifiers{private get; set;}

    public int CalculateDamage() //don't like this...
    {
        throw new NotImplementedException();
    }

    public int CalculateDamageFromMomentum(int tilesCovered){ 
		int weaponDamage = (TileRoot as StatBasedGear).GetTotalGearBaseDamage();
		int strength = (TileRoot as Attributive).Strength;
		var momentumMultiplier = 1 + (float) tilesCovered/_momentumSubdivider;
		var strengthMultiplier = 1 + (float) strength/_strengthSubdivider; //this should be a weapon stat (and they should have an agility bonus as well)
		var verticalMultiplier = _verticalMultiplier * 	_ProcessEffectMagnitudeFromVerticalityModifier(tilesCovered)	
		float rawDamage = weaponDamage * _skillMultiplier * momentumMultiplier * strengthMultiplier;
		GD.Print($"rawDamage : {rawDamage}");
		return (int) Math.Floor(rawDamage);
	}
	
	private float _ProcessEffectMagnitudeFromVerticalityModifier(List<Vector2I> path){
		var pathIsVerticalEncoded = _EncodeBooleanForPathVerticality(path);
		var hasVerticalModifierEncoded = RoomModifiers.FindAll(mod => mod == LevelModifiers.vertical_match_multiplier.ToString()).Count;
        var verticalMultiplier = hasVerticalModifierEncoded * 1.2;
		verticalMultiplier *= pathIsVerticalEncoded;
		return (float)verticalMultiplier;
	}

	private int _EncodeBooleanForPathVerticality(List<Vector2I> path){
		for(int i=0; i<path.Count-1; i++){
			var height = path[i].Y;
			var nextHeight = path[i+1].Y;
			if (height != nextHeight) return 0;
		}	
		return 1;	
	} 
}