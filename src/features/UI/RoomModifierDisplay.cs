using System;
using Godot;
using Godot.Collections;
//using System.Collections.Generic;
using UI;
using Levels;

public partial class RoomModifierDisplay : HBoxContainer, DisplayableElements/* DisplayableNodeListFromStrings */
{
	[Export] private Dictionary<LevelModifiers, Texture2D> _modifierMappings;

    public void Update<[MustBeVariant]T>(Array<T> elementsData){
        foreach(var modifierName in elementsData){
			var stringModifier = modifierName.ToString();
			if (stringModifier.Length > 0 && Enum.TryParse<LevelModifiers>(modifierName as string, out var modifierEnum)){
				var sprite = new Sprite2D(){
					Texture =  _modifierMappings[modifierEnum]
				};
				AddChild(sprite);
			}

		}
    }
}
