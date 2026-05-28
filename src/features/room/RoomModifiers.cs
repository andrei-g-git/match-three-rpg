using Content;
using Godot;
using Godot.Collections;
using Levels;
using Room;
using System.Collections.Generic;
using UI;

public partial class RoomModifiers : Node, ModifiableRoom
{
	[Export] Node _roomModifiersHud;
    public List<string> Modifiers { get; set; }
	private int _roomIndex = 0;
    public int RoomIndex { 
		get => _roomIndex; 
		set{
			_roomIndex = value;
			_LoadModifiersOfRoom(_roomIndex);
		} 
	}

	//[Signal] public delegate void ModifiersChangedEventHandler(string[] modifiers);

	private void _LoadModifiersOfRoom(int roomIndex){
		Files.LoadJson<List<LevelSchema>>(Files.LevelsPath, "levels.json")
			.ContinueWith(task =>{
				var levels = task.Result;
				var level = levels[roomIndex];
				Modifiers = level.Modifiers;

				//EmitSignal(SignalName.ModifiersChanged,  new Godot.Collections.Array(Modifiers));
				//(_roomModifiersHud as DisplayableElements).Update([.. Modifiers]);
			});	

		GetTree().CreateTimer(1).Timeout += () =>{ //fuck. I can't change node layout and contents outside the main thread so I can't do it asynchronously
			(_roomModifiersHud as DisplayableElements).Update([.. Modifiers]);
		};	
	}
}
