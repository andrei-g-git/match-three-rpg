using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using UI;

public partial class RoomModifierDisplay : HBoxContainer, DisplayableElements
{

    public void Update<[MustBeVariant]T>(Array<T> elementsData){
        foreach(var modifierName in elementsData){
			var label = new Label{
				Text = modifierName as string
			};
			AddChild(label);
		}
    }
}
