using Godot;
using System;

//THESE SHOULD BASICALLY HAVE ALL THE FIXED/BASED TRAITS OF THE SKILL, NOT JUST THE ENUM. FOR EXAMPLE, ENERGY REQUIREMENTS (so I don't have to update the code in multiple spots)
namespace Skills;
public class SkillNames{
	public enum All{
		Bullrush,
		Charge,	
		LeapAttack,
		Whirlwind,
		None,
		ThrowWeapon, 
		ShieldBash,
		Ensnare,
		Kick,
		Walk,
		Sprint				
	}

	public enum Melee{
		Bullrush = All.Bullrush,
		Charge = All.Charge,	
		LeapAttack =  All.LeapAttack,
		Whirlwind = All.Whirlwind,
		Kick = All.Kick
	}
	public enum Ranged{
		ThrowWeapon = All.ThrowWeapon,
		Sprint = All.Sprint
	}
	public enum Defensive{
		ShieldBash = All.ShieldBash,
		Walk = All.Walk	
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


