using Godot;
using System;

namespace Skills;
public class SkillNames{
	enum All{
		Charge,	
		LeapAttack,
		None,
		ThrowWeapon, 
		ShieldBash,
		Ensnare				
	}

	public enum Melee{
		Charge = All.Charge,	
		LeapAttack =  All.LeapAttack
	}
	public enum Ranged{
		JavelinThrow = All.ThrowWeapon
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


