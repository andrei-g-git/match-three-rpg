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
	[Export] private Array<CutoutEntry> _cutoutEntries;
	/* [Export] */ private Dictionary<Cutouts, Texture2D> _allEquipment;


	public override void _Ready() {
        _allEquipment = new Dictionary<Cutouts, Texture2D>();
        if (_cutoutEntries != null) {
            foreach (var e in _cutoutEntries) {
                if (e != null)
                    _allEquipment[e.Cutout] = e.Texture;
            }
        }
	}


	public void ChangeGear(string type, string item){
		if(Enum.TryParse<EquipmentTypes>(type, true, out var enumGearType)){
			Enum.TryParse<Cutouts>(item, true, out var enumItem);

			var texture = _allEquipment[enumItem];
			switch (enumGearType){
				case EquipmentTypes.Head:
					_headGear.Texture = texture;
					break;
				// case EquipmentTypes.Torso:
				// 	_torsoGear.Texture = texture;
				// 	break;		
				case EquipmentTypes.Weapon:
					_weapon.Texture = texture;
					break;	
				case EquipmentTypes.OffHand:
					_offHand.Texture = texture;
					break;														
			}			
		}

	}
}
