using Common;
using Godot;
using System;
using UI;

public partial class Hud : Control
{
	[Export] Node _uiEventBus;
	//[Export] Node _energyBar;
	[Export] Node _fireBar;
	[Export] Node _windBar;
	[Export] Node _earthBar;
	[Export] Node _waterBar;

	public override void _Ready(){
		var eventBus = _uiEventBus as RemoteSignaling;
		//eventBus.Subscribe(Callable.From((_energyBar as ProgressableBar).Update), Events.EnergyChanged);

		//eventBus.Subscribe<int, int>((_energyBar as ProgressableBar).Update, Events.EnergyChanged);

		eventBus.Subscribe<int, int>((_fireBar as ProgressableBar).Update, Events.FireChanged);
		eventBus.Subscribe<int, int>((_windBar as ProgressableBar).Update, Events.WindChanged);
		eventBus.Subscribe<int, int>((_earthBar as ProgressableBar).Update, Events.EarthChanged);
		eventBus.Subscribe<int, int>((_waterBar as ProgressableBar).Update, Events.WaterChanged);
		var bp = 123;
	}

}
