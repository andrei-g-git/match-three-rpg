using Common;
using Godot;
using Skills;
using System;

public partial class PlayerSkillsModel : Node, ManageableSkills, Modelable, SkillMaking
{
	[Export] Node _skillFactory;
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
		var bp = 123;	
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
		var bp = 123;
	}

	public void RegisterAll(Control meleeGroupUi, Control rangedGroupUi, Control defensiveGroupUi, Control techGroupUi){
        MeleeGroupUi = meleeGroupUi;
        RangedGroupUi = rangedGroupUi;
        DefensiveGroupUi = defensiveGroupUi;
        TechGroupUi = techGroupUi;        
    }

    public Node Create(SkillNames.All type){
        return (_skillFactory as SkillMaking).Create(type);
    }

	public SkillNames.All GetSelectedInGroup(SkillNames.SkillGroups group){
		switch(group){
			case SkillNames.SkillGroups.Melee:
				return SelectedMelee;
			case SkillNames.SkillGroups.Ranged:
				return SelectedRanged;
			case SkillNames.SkillGroups.Defensive:
				return SelectedDefensive;
			case SkillNames.SkillGroups.Tech:
				return SelectedTech;	
			default:
				return default;						
		}
	}
}
