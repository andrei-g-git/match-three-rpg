using Godot;
using Skills;
using Stats;
using System;
using Tiles;

public partial class EnergyCollector : Node, /* CollectableEnergy, */ WithAttributes
{
	//[Export] private Node actor;
	//[Export] private Node energyProcessor;
	//[Export] private Node SomeEnergyProcessor; //will have access to player stats and actor(to get class)

	[Export] private Node _derivedStats;
	public Attributive Attributes{private get;set;} //not set!!    set in Player Manager


	[Signal]
	public delegate void EnergyChangedEventHandler(int energy, int maxEnergy);

    public override void _Ready(){
		//EmitSignal(SignalName.EnergyChanged, energy, maxEnergy); this emits before connecting to statusbar update
	}

    public void FillEnergy(int magnitude, SkillNames.SkillGroups skillGroup){
		var energyBeforeMagnitude = (energyProcessor as ProcessableEnergy).CalculateBySkillGroup(skillGroup);
		var fill = (int) Math.Floor(energyBeforeMagnitude * Math.Pow(1.5f, magnitude - 3));
		energy = Math.Clamp(energy + fill, 0, maxEnergy);

		EmitSignal(SignalName.EnergyChanged, energy, maxEnergy);
    }

	public void SpendEnergy(int amount){

	}

	private int CalculateBySkillGroup(SkillGroups skillGroup){
		var charClass = (characterClass as Classy).Class;
		return GetValueByClassAndGroup(charClass, skillGroup);
	}

	private int GetValueByClassAndGroup(CharacterClasses charClass, SkillGroups skillGroup){
		var table = new Dictionary<CharacterClasses, Dictionary<SkillGroups, int>>();
		table[CharacterClasses.Fighter] = new Dictionary<SkillGroups, int>{
			{SkillGroups.CloseRange, 10},
			{SkillGroups.LongRange, 7},
			{SkillGroups.Defensive, 10},
			{SkillGroups.Tech, 5}
		};
		table[CharacterClasses.Ranger] = new Dictionary<SkillGroups, int>{
			{SkillGroups.CloseRange, 7},
			{SkillGroups.LongRange, 10},
			{SkillGroups.Defensive, 7},
			{SkillGroups.Tech, 7}
		};		
		table[CharacterClasses.Sorceress] = new Dictionary<SkillGroups, int>{
			{SkillGroups.CloseRange, 5},
			{SkillGroups.LongRange, 7},
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