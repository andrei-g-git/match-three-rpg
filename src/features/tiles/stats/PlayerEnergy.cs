using Common;
using Godot;
using Skills;
using Stats;
using System;

public partial class PlayerEnergy : Node, RefillableEnergy, WithEnergy, /* DerivableMaxEnergy, */ WithFireEnergy, WithWindEnergy, WithEarthEnergy, WithWaterEnergy, RelayableUIEvents
{
	[Export] private Node _player;
	//[Export] private Node _uiEventBus;
    public RemoteSignaling UIEventBus { private get; set; } //_player already has this, but i should pass only what I need case by case
	
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

	}


    public void GainEnergyFromElement(SkillNames.SkillGroups element, int howManyTimes = 1){

		switch (element){
			case SkillNames.SkillGroups.Melee: 
				_GainFireEnergy(howManyTimes);
				break;
			case SkillNames.SkillGroups.Ranged: 
				_GainWindEnergy(howManyTimes);
				break;
			case SkillNames.SkillGroups.Defensive: 
				_GainEarthEnergy(howManyTimes);
				break;
			case SkillNames.SkillGroups.Tech: 
				_GainWaterEnergy(howManyTimes);
				break;
		}
    }

	//i don't think i need methods for every energy type
    /* public */private void _GainFireEnergy(int howManyTimes = 1){
		var strength = (_player as Attributive).Strength;
        FireEnergy += _GetEnergyGainByAttributeAndCellsMatched(strength, howManyTimes);
		if(FireEnergy > MaxFireEnergy) FireEnergy = MaxFireEnergy;
		(UIEventBus as UIEventBus).Publish(Events.FireChanged, [FireEnergy, MaxFireEnergy]);
    }

    /* public */private void _GainWindEnergy(int howManyTimes = 1){
		var agility = (_player as Attributive).Agility;
        WindEnergy += _GetEnergyGainByAttributeAndCellsMatched(agility, howManyTimes);
		if(WindEnergy > MaxWindEnergy) WindEnergy = MaxWindEnergy;
		(UIEventBus as UIEventBus).Publish(Events.WindChanged, [WindEnergy, MaxWindEnergy]);
    }

    /* public */private void _GainEarthEnergy(int howManyTimes = 1){
		var constitution = (_player as Attributive).Constitution;
        EarthEnergy += _GetEnergyGainByAttributeAndCellsMatched(constitution, howManyTimes);
		if(EarthEnergy > MaxEarthEnergy) EarthEnergy = MaxEarthEnergy;
		(UIEventBus as UIEventBus).Publish(Events.EarthChanged, [EarthEnergy, MaxEarthEnergy]);
    }

    /* public */private void _GainWaterEnergy(int howManyTimes = 1){
		var intelligence = (_player as Attributive).Intelligence;
        WaterEnergy += _GetEnergyGainByAttributeAndCellsMatched(intelligence, howManyTimes);
		if(WaterEnergy > MaxWaterEnergy) WaterEnergy = MaxWaterEnergy;
		(UIEventBus as UIEventBus).Publish(Events.WaterChanged, [WaterEnergy, MaxWaterEnergy]);
    }

	private int _GetEnergyGainByAttributeAndCellsMatched(int attributeValue, int howManyCells){
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
