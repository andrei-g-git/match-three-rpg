using Godot;
using Skills;
using Stats;
using System;
using System.Collections.Generic;
using Tiles;
using static Skills.SkillNames;

public partial class EnergyCollector : Node, CollectableEnergy, WithAttributes
{
	//[Export] private Node actor;
	//[Export] private Node energyProcessor;
	//[Export] private Node SomeEnergyProcessor; //will have access to player stats and actor(to get class)

	[Export] private Node _derivedStats;
	[Export] private Node _player;
	public Attributive Attributes{private get;set;} //not set!!    set in Player Manager


	[Signal]
	public delegate void EnergyChangedEventHandler(int energy, int maxEnergy);

    public override void _Ready(){
		//EmitSignal(SignalName.EnergyChanged, energy, maxEnergy); this emits before connecting to statusbar update
	}

    public void FillEnergy(int magnitude, SkillGroups skillGroup){
		var energyBeforeMagnitude = _CalculateBySkillGroup(skillGroup);
		var fill = (int) Math.Floor(energyBeforeMagnitude * Math.Pow(1.5f, magnitude - 3));
		var derived = _derivedStats as DerivableStats;
		var energy = derived.Energy;
		derived.Energy = Math.Clamp(energy + fill, 0, derived.GetMaxEnergy());

		EmitSignal(SignalName.EnergyChanged, energy, derived.GetMaxEnergy());
    }

	public void SpendEnergy(int amount){

	}

	private int _CalculateBySkillGroup(SkillGroups skillGroup){
		var charClass = (_player as Classy).Class;
		return _GetValueByClassAndGroup(charClass, skillGroup);
	}

	private int _GetValueByClassAndGroup(Classes charClass, SkillGroups skillGroup){
		var table = new Dictionary<Classes, Dictionary<SkillGroups, int>>();
		table[Classes.Fighter] = new Dictionary<SkillGroups, int>{
			{SkillGroups.Melee, 10},
			{SkillGroups.Ranged, 7},
			{SkillGroups.Defensive, 10},
			{SkillGroups.Tech, 5}
		};
		table[Classes.Ranger] = new Dictionary<SkillGroups, int>{
			{SkillGroups.Melee, 7},
			{SkillGroups.Ranged, 10},
			{SkillGroups.Defensive, 7},
			{SkillGroups.Tech, 7}
		};		
		table[Classes.Sorceress] = new Dictionary<SkillGroups, int>{
			{SkillGroups.Melee, 5},
			{SkillGroups.Ranged, 7},
			{SkillGroups.Defensive, 7},
			{SkillGroups.Tech, 10}
		};

		if(table.TryGetValue(charClass, out var groupDict)){
			if(groupDict.TryGetValue(skillGroup, out var cellValue)){
				return cellValue;
			}
		}
		throw new Exception("row or column not found");
	}

	/* 
				--------------------------------------------------------------
				|	close	|	long	|	defense	|	tech	|
	--------------------------------------------------------------
	fighter		|	10		|	7		|	10		|	5		|
	-----------------------------------------------------------------
	ranger		|	7		|	10		|	7		|	7		|
	----------------------------------------------------------------
	sorcere.	|	5		|	7		|	7		|	10		|
	----------------------------------------------------------------
	
	 */

}