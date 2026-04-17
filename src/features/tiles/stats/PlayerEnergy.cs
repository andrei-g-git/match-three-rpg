using Godot;
using Skills;
using Stats;
using System;

public partial class PlayerEnergy : Node, RefillableEnergy, WithEnergy, /* DerivableMaxEnergy, */ WithFireEnergy, WithWindEnergy, WithEarthEnergy, WithWaterEnergy
{
	[Export] private Node _derivedStats;
	
	public int MaxFireEnergy{get;set;} 
	private int _FireEnergy;
	public int FireEnergy{
		get => _FireEnergy;
		set { _FireEnergy = Math.Clamp(value, 0, MaxFireEnergy); }
	}
	public int MaxWindEnergy{get;set;}
	private int _WindEnergy;
	public int WindEnergy{
		get => _WindEnergy;
		set { _WindEnergy = Math.Clamp(value, 0, MaxWindEnergy); }
	}
	public int MaxEarthEnergy{get;set;}
	private int _EarthEnergy;
	public int EarthEnergy{
		get => _EarthEnergy;
		set { _EarthEnergy = Math.Clamp(value, 0, MaxEarthEnergy); }
	}
	public int MaxWaterEnergy{get;set;}
	private int _WaterEnergy;
	public int WaterEnergy{
		get => _WaterEnergy;
		set { _WaterEnergy = Math.Clamp(value, 0, MaxWaterEnergy); }
	}


	public override void _Ready(){
				//test
		// MaxFireEnergy = 10;		
		// FireEnergy = 5;
		// MaxWindEnergy = 10;
		// WindEnergy = 3;
		// MaxEarthEnergy = 10;
		// EarthEnergy = 1;
		// MaxWaterEnergy = 10;
		// WaterEnergy = 0;
	}


    public void GainEnergyFromElement(SkillNames.SkillGroups element, int howManyTimes = 1){

		switch (element){
			case SkillNames.SkillGroups.Melee: 
				GainFireEnergy(howManyTimes);
				break;
			case SkillNames.SkillGroups.Ranged: 
				GainWindEnergy(howManyTimes);
				break;
			case SkillNames.SkillGroups.Defensive: 
				GainEarthEnergy(howManyTimes);
				break;
			case SkillNames.SkillGroups.Tech: 
				GainWaterEnergy(howManyTimes)		;
				break;
		}
    }

	//i don't think i need methods for every energy type
    public void GainFireEnergy(int howManyTimes = 1){
		var strength = (_derivedStats as Attributive).Strength;
        FireEnergy = _GetEnergyGainMultiplierByAttributeAndCellsMatched(strength, howManyTimes);
		if(FireEnergy > MaxFireEnergy) FireEnergy = MaxFireEnergy;
    }

    public void GainWindEnergy(int howManyTimes = 1){
		var agility = (_derivedStats as Attributive).Agility;
        WindEnergy = _GetEnergyGainMultiplierByAttributeAndCellsMatched(agility, howManyTimes);
		if(WindEnergy > MaxWindEnergy) WindEnergy = MaxWindEnergy;
    }

    public void GainEarthEnergy(int howManyTimes = 1){
		var constitution = (_derivedStats as Attributive).Constitution;
        EarthEnergy = _GetEnergyGainMultiplierByAttributeAndCellsMatched(constitution, howManyTimes);
		if(EarthEnergy > MaxEarthEnergy) EarthEnergy = MaxEarthEnergy;
    }

    public void GainWaterEnergy(int howManyTimes = 1){
		var intelligence = (_derivedStats as Attributive).Intelligence;
        WaterEnergy = _GetEnergyGainMultiplierByAttributeAndCellsMatched(intelligence, howManyTimes);
		if(WaterEnergy > MaxWaterEnergy) WaterEnergy = MaxWaterEnergy;
    }

	private int _GetEnergyGainMultiplierByAttributeAndCellsMatched(int attributeValue, int howManyCells){
        var cellsTraveledMultiplier = howManyCells switch{
            3 => 1,
            4 => 1.35f,
            5 => 2,
            6 or 7 or 8 => 3,
            _ => 1f,
        };
        if (attributeValue >= 16) return (int) Math.Ceiling(cellsTraveledMultiplier * 7);
		if(attributeValue >= 12) return (int) Math.Ceiling(cellsTraveledMultiplier * 6);
		if(attributeValue >= 8) return (int) Math.Ceiling(cellsTraveledMultiplier * 5);
		if(attributeValue >= 5) return (int) Math.Ceiling(cellsTraveledMultiplier * 3);
		return (int) Math.Floor(cellsTraveledMultiplier * 2);
	}



	//these are initia
	public static int CalculateInitialMaxEnergy(int attributeValue){
		return (int) Math.Ceiling(3 + Math.Pow(attributeValue, 1.2));
	}
    public static int CalculateInitialMaxFireEnergy(int strength){
		return (int) Math.Ceiling(3 + Math.Pow(strength, 1.2));
	}
    public static int CalculateInitialMaxWindEnergy(int agility){
		return (int) Math.Ceiling(3 + Math.Pow(agility, 1.2));
	}
    public static int CalculateInitialMaxEarthEnergy(int constitution){
		return (int) Math.Ceiling(3 + Math.Pow(constitution, 1.2));
	}
    public static int CalculateInitialMaxWaterEnergy(int intelligence){
		return (int) Math.Ceiling(3 + Math.Pow(intelligence, 1.2));
	}	
}
