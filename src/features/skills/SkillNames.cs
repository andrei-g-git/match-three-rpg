using Godot;
using System;

namespace Skills;
public class SkillNames{
	public enum All{
		Charge,	
		LeapAttack,
		Whirlwind,
		None,
		ThrowWeapon, 
		ShieldBash,
		Ensnare				
	}

	public enum Melee{
		Charge = All.Charge,	
		LeapAttack =  All.LeapAttack,
		Whirlwind = All.Whirlwind
	}
	public enum Ranged{
		ThrowWeapon = All.ThrowWeapon
	}
	public enum Defensive{
		ShieldBash = All.ShieldBash	
	}
	public enum Tech{
		Ensnare	= All.Ensnare
	}

	public enum SkillGroups{
		Melee,
		Ranged,
		Defensive, 
		Tech
	}	
}


