using Common;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using UI;

public partial class Hud : Control
{
	[Export] Node _uiEventBus;
	//[Export] Node _energyBar;
	[Export] Node _fireBar;
	[Export] Node _windBar;
	[Export] Node _earthBar;
	[Export] Node _waterBar;

	[Export] Node _roomModifiers;

	public override void _Ready(){
		var eventBus = _uiEventBus as RemoteSignaling;
		//eventBus.Subscribe(Callable.From((_energyBar as ProgressableBar).Update), Events.EnergyChanged);

		//eventBus.Subscribe<int, int>((_energyBar as ProgressableBar).Update, Events.EnergyChanged);

		eventBus.Subscribe<int, int>((_fireBar as ProgressableBar).Update, Events.FireChanged);
		eventBus.Subscribe<int, int>((_windBar as ProgressableBar).Update, Events.WindChanged);
		eventBus.Subscribe<int, int>((_earthBar as ProgressableBar).Update, Events.EarthChanged);
		eventBus.Subscribe<int, int>((_waterBar as ProgressableBar).Update, Events.WaterChanged);

		eventBus.Subscribe<Array<string>, bool>((elements, unimportant) => (_roomModifiers as DisplayableElements).Update(elements), Events.RoomModifiersChanged);
		var bp = 123;
	}

}
