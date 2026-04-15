using Godot;
using Godot.Collections;
using Skills;
using System;

public partial class ElementSkillsDisplay : /* ItemList */GridContainer, SelectableSkills
{
	
	[Export] private Dictionary<SkillNames.All, Texture2D> _skillMap;
	[Export] private PackedScene _skillButton;
	public CountableSkill[] Skills{get;set;} = [];


	public override void _Ready(){
		GD.Print("group ready");
	}

	public void UpdateSkills(CountableSkill[] skills){
		Skills = skills;

		//clear all child nodes except the first (it's a line delimiter)
		
		if(GetChildCount() > 1){
			for(var i=0; i<GetChildCount(); i++){
				//GD.Print("ElementSkillsDisplay child count:   ", GetChildCount());
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
			AddChild(button);
		}
	}

	public void OnItemSelected(int index){
		//var selectedSkill = (string) GetItemMetadata(index);
		//EmitSignal(SignalName.SelectedSkillFromGroup, selectedSkill, _skillGroup.ToString());
	}	
}
