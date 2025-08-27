using Godot;
using Skills;
using System;

public partial class EnergyCollector : Node//, CollectableEnergy
{
	[Export] private int energy; //should be initialized in accordance to class, not here   ADD DERIVED STATS!
	[Export] private int maxEnergy;
	[Export] private Node actor;
	[Export] private Node energyProcessor;
	//[Export] private Node SomeEnergyProcessor; //will have access to player stats and actor(to get class)
	[Signal]
	public delegate void EnergyChangedEventHandler(int energy, int maxEnergy);

    public override void _Ready(){
		//EmitSignal(SignalName.EnergyChanged, energy, maxEnergy); this emits before connecting to statusbar update
	}

    // public void FillEnergy(int magnitude, SkillNames.SkillGroups skillGroup){
	// 	var energyBeforeMagnitude = (energyProcessor as ProcessableEnergy).CalculateBySkillGroup(skillGroup);
	// 	var fill = (int) Math.Floor(energyBeforeMagnitude * Math.Pow(1.5f, magnitude - 3));
	// 	energy = Math.Clamp(energy + fill, 0, maxEnergy);

	// 	EmitSignal(SignalName.EnergyChanged, energy, maxEnergy);
    // }

	public void SpendEnergy(int amount){

	}

    public void ConnectEnergyChanged(Action</* float */int, /* float */int> action){
        Connect(SignalName.EnergyChanged, Callable.From(action));

		EmitSignal(SignalName.EnergyChanged, energy, maxEnergy); //for some reason this signal is picked up and calls the callback but the mana bar doesn't change this time, only when collecting mana
    }

}