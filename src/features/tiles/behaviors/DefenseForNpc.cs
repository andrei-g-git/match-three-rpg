using Godot;
using Stats;
using System;
using Tiles;

public partial class DefenseForNpc : Node, Defensible
{
	[Export] private Node _stats;
	[Signal] private delegate void TookDamageEventHandler(int amount);
	[Signal] private delegate void DyingEventHandler(int amount);


	public override void _Ready(){}

	public void TakeDamage(int damage){	
		var healthSystem = _stats as WithHealth;
		var finalDamage = Math.Max(0, damage - (_stats as WithDefense).Defense);
		var health = healthSystem.Health;
		healthSystem.Health = Math.Max(0, health - finalDamage); 
		EmitSignal(SignalName.TookDamage, finalDamage);
		if(healthSystem.Health <= 0){
			EmitSignal(SignalName.Dying);
		}
	}

	public void ConnectTookDamage(Action<int> action){
		Connect(SignalName.TookDamage, Callable.From(action));
	}
}


