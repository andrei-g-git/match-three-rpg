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
}