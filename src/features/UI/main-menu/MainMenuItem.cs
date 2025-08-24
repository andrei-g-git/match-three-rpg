using Godot;
using System;
using UI;

public partial class MainMenuItem : Button
{
	[Export] MainMenuItems _menuItem;
	[Signal] public delegate void MenuItemPressedEventHandler(string menuItem);

    public override void _Pressed(){
		EmitSignal(SignalName.MenuItemPressed, _menuItem.ToString());
    }

}
