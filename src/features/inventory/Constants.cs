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
}