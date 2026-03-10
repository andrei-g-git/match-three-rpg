using Godot;
using System;

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
		Kick				
	}

	public enum Melee{
		Bullrush = All.Bullrush,
		Charge = All.Charge,	
		LeapAttack =  All.LeapAttack,
		Whirlwind = All.Whirlwind,
		Kick = All.Kick
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


