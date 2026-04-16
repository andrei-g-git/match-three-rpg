using Common;
using Godot;
using Godot.Collections;
using Skills;
using System;

public partial class ElementSkillsDisplay : /* ItemList */Control, SelectableSkills
{
	
	[Export] private Dictionary<SkillNames.All, Texture2D> _skillMap;
	[Export] private PackedScene _skillButton;
	[Export] private Label _groupName;
	[Export] private Control _skillContainer;
	public CountableSkill[] Skills{get;set;} = [];

	public Node TestParent{get;set;}

	public override void _Ready(){
		GD.Print("group ready");
	}

	public void Update(SkillNames.All[] skills, SkillNames.SkillGroups skillGroup){
		_groupName.Text = skillGroup.ToString();
		foreach(var skillButton in _skillContainer.GetChildren()){
			_skillContainer.RemoveChild(skillButton);
			skillButton.QueueFree();
		}

		foreach(var skill in skills){
			var button = _skillButton.Instantiate() as TextureButton;
			button.TextureNormal = _skillMap[skill];
			(button as UseSkillButton).SetSkillLabel(skill.ToString());

			_skillContainer.AddChild(button);

			(button as DeactivatableButton).Activate();
			(button as UseSkillButton).ConnectClickedSkill(OnSkillClicked);
		}	
	}

	public void UpdateSkills(CountableSkill[] skills){
		Skills = skills;

		//clear all child nodes except the first (it's a line delimiter)
		
		if(GetChildCount() > 1){
			for(var i=1; i<GetChildCount(); i++){
				var node = GetChild(1);
				RemoveChild(node);
				node.QueueFree();
			}			
		}


		foreach(var skill in skills){
			// var index = AddItem(skill.Name, _skillMap[skill.GetSkillEnum()]);
			// SetItemMetadata(index, skill.Name);
			var button = _skillButton.Instantiate();
			var texture = _skillMap[skill.GetSkillEnum()];
			(button as TextureButton).TextureNormal = texture;
			(button as UseSkillButton).SetSkillLabel(skill.Name); //not interface
			(button as UseSkillButton).ConnectClickedSkill(OnSkillClicked);
			


			AddChild(button);
		}
	}

	public void OnSkillClicked(string skillName){
		//var selectedSkill = (string) GetItemMetadata(index);
		//EmitSignal(SignalName.SelectedSkillFromGroup, selectedSkill, _skillGroup.ToString());

		(TestParent as SkillGroupsDisplay).TestEmitSkillPicked(skillName);
	}	
}
