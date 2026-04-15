using Godot;
using Godot.Collections;
using Skills;
using System;

public partial class ElementSkillsDisplay : ItemList, SelectableSkills
{
	
	[Export] private Dictionary<SkillNames.All, Texture2D> _skillMap;
	public CountableSkill[] Skills{get;set;} = [];


	public override void _Ready()
	{
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
			var index = AddItem(skill.Name, _skillMap[skill.GetSkillEnum()]);
			SetItemMetadata(index, skill.Name);
		}
	}

	public void OnItemSelected(int index){
		var selectedSkill = (string) GetItemMetadata(index);
		//EmitSignal(SignalName.SelectedSkillFromGroup, selectedSkill, _skillGroup.ToString());
	}	
}
