using Godot;
using System;

namespace Inventory{
	public enum EquipmentTypes{
		Head,
		Torso,
		Weapon,
		OffHand
	}

	public enum Helmets{
		RustyHelmet,
		SteelHelmet,
		Empty
	}

	public enum Armors{
		QuiltedArmor,
		LeatherArmor,
		SteelPlateArmor,
		Empty
	}

	public enum Weapons{
		WoodenClub,
		IronSword,
		SteelLongSword,
		SteelWarAxe,
		IronSpear,
		Empty
	}

	public enum OffHands{
		WoodenShield,
		IronShield,
		TowerShield,
		Empty
	}

	public enum Cutouts{
		//should also have body parts


		RustyHelmet,
		SteelHelmet,
		QuiltedArmor,
		QuiltedArmorArmRightUpper,
		QuiltedArmorArmRightLower,
		QuiltedArmorArmLeftUpper,
		QuiltedArmorArmLeftLower,
		LeatherArmor,
		LeatherArmorArmRightUpper,
		LeatherArmorArmRightLower,
		LeatherArmorArmLeftUpper,
		LeatherArmorArmLeftLower,		
		SteelPlateArmor,
		SteelPlateArmorArmRightUpper,
		SteelPlateArmorArmRightLower,
		SteelPlateArmorArmLeftUpper,
		SteelPlateArmorArmLeftLower,		
		WoodenClub,
		IronSword,
		SteelLongSword,
		SteelWarAxe,
		IronSpear,
		WoodenShield,
		IronShield,
		TowerShield,	
	}

	public enum Purpose{
		Armor,
		Weapon
	}


    public class AllGearData{
        public GearData[] Gear{get;} = {
            new GearData{
                Name = Cutouts.RustyHelmet.ToString(),
                Purpose = Purpose.Armor.ToString().ToLower(),
                Slot = EquipmentTypes.Head.ToString().ToLower(),
                Damage = 0,
                Defense = 1
            },
            new GearData{
                Name = Cutouts.SteelHelmet.ToString(),
                Purpose = Purpose.Armor.ToString().ToLower(),
                Slot = EquipmentTypes.Head.ToString().ToLower(),
                Damage = 0,
                Defense = 2
            },
            new GearData{
                Name = Cutouts.QuiltedArmor.ToString(),
                Purpose = Purpose.Armor.ToString().ToLower(),
                Slot = EquipmentTypes.Torso.ToString().ToLower(),
                Damage = 0,
                Defense = 2
            },
            new GearData{
                Name = Cutouts.LeatherArmor.ToString(),
                Purpose = Purpose.Armor.ToString().ToLower(),
                Slot = EquipmentTypes.Torso.ToString().ToLower(),
                Damage = 0,
                Defense = 3
            },
            new GearData{
                Name = Cutouts.SteelPlateArmor.ToString(),
                Purpose = Purpose.Armor.ToString().ToLower(),
                Slot = EquipmentTypes.Torso.ToString().ToLower(),
                Damage = 0,
                Defense = 4
            },
            // new GearData{
            //     Name = "",
            //     Purpose = "",
            //     Slot = "",
            //     Damage = ,
            //     Defense = 
            // },
            // new GearData{
            //     Name = "",
            //     Purpose = "",
            //     Slot = "",
            //     Damage = ,
            //     Defense = 
            // },
            new GearData{
                Name = Cutouts.WoodenShield.ToString(),
                Purpose = Purpose.Armor.ToString().ToLower(),
                Slot = EquipmentTypes.OffHand.ToString().ToLower(),
                Damage = 0,
                Defense = 2
            },			
            new GearData{
                Name = Cutouts.WoodenClub.ToString(),
                Purpose = Purpose.Weapon.ToString().ToLower(),
                Slot = EquipmentTypes.Weapon.ToString().ToLower(),
                Damage = 5,
                Defense = 0
            },                                                                                   
        };	
    }
}