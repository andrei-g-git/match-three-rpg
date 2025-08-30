using Common;
using Godot;
using Godot.Collections;
using Skills;
using System;

public partial class SkillGroupsController : HBoxContainer, ControllableSkillGroups
{
	[Export] private Node _playerSkillsModel; 

	[ExportGroup("skill groups")]
	[Export] private ItemList _melee;
	[Export] private ItemList _ranged;
	[Export] private ItemList _defensive;
	[Export] private ItemList _tech;

	public override void _Ready(){
		(_playerSkillsModel as ManageableSkills).RegisterAll(
			_melee,
			_ranged,
			_defensive,
			_tech
		);
		(_playerSkillsModel as Modelable).Notify();
	}

	public void OnSkillSelectedFromGroup(string skill, string group){
		(_playerSkillsModel as ManageableSkills).SetSelectedSkill(skill, group);
	}
}
