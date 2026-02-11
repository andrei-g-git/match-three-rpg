using Animations;
using Godot;
using Godot.Collections;
using Inventory;
using System;

public partial class GearCustomizer : Node, CustomizableGear
{
	[ExportGroup("Head")] 
	[Export] private Sprite2D _headGear;

	[ExportGroup("Torso")]
	[Export] private Sprite2D _torsoGear;
	[Export] private Sprite2D _armRightUpperGear;
	[Export] private Sprite2D _armRightLowerGear;
	[Export] private Sprite2D _armLefttUpperGear;
	[Export] private Sprite2D _armLefttLowerGear;

	[ExportGroup("Weapon")]
	[Export] private Sprite2D _weapon;

	[ExportGroup("Off Hand")]
	[Export] private Sprite2D _offHand;

	[ExportGroup("All Possible Gear")]
	[Export] private Dictionary<Cutouts, Texture2D> _allEquipment;

	public void ChangeGear(string type, string item){
		if(Enum.TryParse<EquipmentTypes>(type, true, out var enumGearType)){
			Enum.TryParse<Cutouts>(item, true, out var enumItem);
			switch (enumGearType){
				case EquipmentTypes.Head:
					GD.Print("11111");
					var _testTexture = _allEquipment[enumItem];
					GD.Print("22222");
					GD.Print("new texture size:   ", _testTexture.GetSize());
					var bp3 = 1321;
					_headGear.Texture = _allEquipment[enumItem];
					GD.Print("33333");
					var bp = 123;
					break;
				case EquipmentTypes.Torso:
					_torsoGear.Texture = _allEquipment[enumItem];
					var bp2 = 123;
					break;					
			}			
		}

	}
}
