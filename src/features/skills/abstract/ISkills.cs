using System.Collections.Generic;
using Godot;
using static Skills.SkillNames;

namespace Skills;

public interface WithEnergyRequirements{
    public int Fire{get;set;}
    public int Wind{get;set;}
    public int Earth{get;set;}
    public int Water{get;set;}
}

public interface CountableSkill{
    public string Name{get;set;}
    public int Uses{get;set;}
    public int Level{get;set;}
    public /* WithEnergyRequirements */ EnergyRequirement EnergyRequirement{get;set;} //serializable, can't have interface here
    public SkillNames.All GetSkillEnum();  
    public int GetFireRequirement();  
    public int GetWindRequirement();
    public int GetEarthRequirement();
    public int GetWaterRequirement();
}

public interface GroupableSkills{
    public string Group{get;set;}
    public SkillWithCount[] Skills{get;set;}   
    public string Selected{get;set;}
    public SkillNames.SkillGroups GetGroupEnum();          
} 

public interface SkillBased{
    public SkillGroups SkillGroup{get;}
}

public interface ManageableSkills{
    public GroupableSkills Melee{get;set;}
    public GroupableSkills Ranged{get;set;}
    public GroupableSkills Defensive{get;set;}
    public GroupableSkills Tech{get;set;}

    public Control MeleeGroupUi{get;set;}
    public Control RangedGroupUi{get;set;}
    public Control DefensiveGroupUi{get;set;}
    public Control TechGroupUi{get;set;}

	public SkillNames.All SelectedMelee{get;}
	public SkillNames.All SelectedRanged{get;}
	public SkillNames.All SelectedDefensive{get;}
	public SkillNames.All SelectedTech{get;}   

    public void SetSelectedSkill(SkillNames.All skill, SkillNames.SkillGroups group);
    public void SetSelectedSkill(string skill, string group);
    public void RegisterAll(Control meleeGroupUi, Control rangedGroupUi, Control defensiveGroupUi, Control techGroupUi);
    public SkillNames.All GetSelectedInGroup(SkillNames.SkillGroups group);
}

public interface SelectableSkills{
    public void UpdateSkills(CountableSkill[] skillsAndUses);
}

public interface ControllableSkillGroups{
    public void OnSkillSelectedFromGroup(string skill, string group);
}

public interface Skillful{
    public Node Skill{set;}
}


public interface Skill{

}

public interface SkillMaking{
    public Node Create(SkillNames.All type);
}

public interface LevelableSkill{
    public string Name{get;set;}
    public int Level{get;set;}

    public SkillNames.All GetSkillEnum();
}

public interface WithEnergy{
    public int FireEnergy{get;set;}
    public int MaxFireEnergy{get;set;} 
    public int WindEnergy{get;set;}
    public int MaxWindEnergy{get;set;}  
    public int EarthEnergy{get;set;}
    public int MaxEarthEnergy{get;set;}    
    public int WaterEnergy{get;set;}
    public int MaxWaterEnergy{get;set;}    
}

// public interface DerivableMaxEnergy{
//     public int CalculateInitialMaxFireEnergy(int strength);
//     public int CalculateInitialMaxWindEnergy(int agility);
//     public int CalculateInitialMaxEarthEnergy(int constitution);
//     public int CalculateInitialMaxWaterEnergy(int intelligencee);
// }



public interface WithFireEnergy{
    public int FireEnergy{get;set;}
    public int MaxFireEnergy{get;set;}
}
public interface WithWindEnergy{
    public int WindEnergy{get;set;}
    public int MaxWindEnergy{get;set;}
}
public interface WithEarthEnergy{
    public int EarthEnergy{get;set;}
    public int MaxEarthEnergy{get;set;}
}
public interface WithWaterEnergy{
    public int WaterEnergy{get;set;}
    public int MaxWaterEnergy{get;set;}
}

public interface RefillableEnergy{
    public void GainEnergyFromElement(SkillGroups element, int howManyTimes);
    public void GainFireEnergy(int howManyTimes);
    public void GainWindEnergy(int howManyTimes);
    public void GainEarthEnergy(int howManyTimes);
    public void GainWaterEnergy(int howManyTimes);
}




