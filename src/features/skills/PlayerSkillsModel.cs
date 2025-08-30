using Common;
using Godot;
using Skills;
using System;

public partial class PlayerSkillsModel : Node, ManageableSkills, Modelable
{
	public GroupableSkills Melee{get;set;}
    public GroupableSkills Ranged{get;set;}
    public GroupableSkills Defensive{get;set;}
    public GroupableSkills Tech{get;set;}

    public Control MeleeGroupUi{get;set;}
    public Control RangedGroupUi{get;set;}
    public Control DefensiveGroupUi{get;set;}
    public Control TechGroupUi{get;set;}	

	public SkillNames.All SelectedMelee{get; private set;}
	public SkillNames.All SelectedRanged{get; private set;}
	public SkillNames.All SelectedDefensive{get; private set;}
	public SkillNames.All SelectedTech{get; private set;}


	public void Notify(){
		(MeleeGroupUi as SelectableSkills).UpdateSkills(Melee.Skills);
		(RangedGroupUi as SelectableSkills).UpdateSkills(Ranged.Skills);
		(DefensiveGroupUi as SelectableSkills).UpdateSkills(Defensive.Skills);
		(TechGroupUi as SelectableSkills).UpdateSkills(Tech.Skills);		
	}
	
    public void SetSelectedSkill(SkillNames.All skill, SkillNames.SkillGroups group){

	}

    public void SetSelectedSkill(string skill, string group){
		var isSkill = Enum.TryParse(skill, out SkillNames.All skillEnum);
		var isGroup = Enum.TryParse(group, out SkillNames.SkillGroups groupEnum);
		switch(groupEnum){
			case SkillNames.SkillGroups.Melee: 
				SelectedMelee = isSkill? skillEnum : default;
				break;
			case SkillNames.SkillGroups.Ranged: 
				SelectedRanged = isSkill? skillEnum : default;
				break;
			case SkillNames.SkillGroups.Defensive: 
				SelectedDefensive = isSkill? skillEnum : default;
				break;
			case SkillNames.SkillGroups.Tech: 
				SelectedTech = isSkill? skillEnum : default;
				break;											
		}
	}

	public void RegisterAll(Control meleeGroupUi, Control rangedGroupUi, Control defensiveGroupUi, Control techGroupUi){
        MeleeGroupUi = meleeGroupUi;
        RangedGroupUi = rangedGroupUi;
        DefensiveGroupUi = defensiveGroupUi;
        TechGroupUi = techGroupUi;        
    }
}
