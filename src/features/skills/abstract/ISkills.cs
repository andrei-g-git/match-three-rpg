using System.Collections.Generic;
using Godot;
using static Skills.SkillNames;

namespace Skills;

public interface CountableSkill{
    public string Name{get;set;}
    public int Uses{get;set;}
    public SkillNames.All GetSkillEnum();    
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




