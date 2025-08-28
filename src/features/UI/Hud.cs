using Common;
using Godot;
using System;
using UI;

public partial class Hud : Control
{
	[Export] Node _uiEventBus;
	[Export] Node _energyBar;
	public override void _Ready(){
		var eventBus = _uiEventBus as RemoteSignaling;
		//eventBus.Subscribe(Callable.From((_energyBar as ProgressableBar).Update), Events.EnergyChanged);

		eventBus.Subscribe<int, int>((_energyBar as ProgressableBar).Update, Events.EnergyChanged);
		var bp = 123;
	}

}
