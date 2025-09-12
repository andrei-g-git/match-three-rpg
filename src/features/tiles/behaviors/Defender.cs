using Godot;
using Stats;
using System;
using Tiles;

public partial class Defender : Node, Defensible
{
	[Export] private Node _stats;
	[Signal] private delegate void TookDamageEventHandler(int amount);


	public override void _Ready(){}

	public void TakeDamage(int damage){
		var stats = _stats as DerivableStats;
		var finalDamage = Math.Max(0, damage - stats.Defense);
		stats.Health = Math.Max(0, stats.Health - finalDamage); 
		EmitSignal(SignalName.TookDamage, finalDamage);		
	}

	// public void ConnectTookDamage(Action<int> action){
	// 	Connect(SignalName.TookDamage, Callable.From(action));
	// }
	
}
