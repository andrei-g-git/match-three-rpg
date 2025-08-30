using Godot;
using Godot.Collections;
using Skills;
using System;
using Util;

public partial class SkillSelection : ItemList, SelectableSkills
{
	[Export] private SkillNames.SkillGroups _skillGroup;
	[Export] private Dictionary<SkillNames.All, Texture2D> _skillMap;
	[Signal] public delegate void SelectedSkillFromGroupEventHandler(string skill, string group);

    public override void _Ready(){
		//this.ItemSelected += _OnItemSelected;
	}

    public void UpdateSkills(CountableSkill[] skillsAndUses){
		Clear();
		foreach(var skill in skillsAndUses){
			var itemName = StringUtils.SplitPascal(skill.Name) + " " + skill.Uses.ToString() + " left"; 
			var index = AddItem(itemName);
			AddIconItem(_skillMap[skill.GetSkillEnum()]);
			SetItemMetadata(index, skill.Name);
		}
    }

	public void OnItemSelected(/* ItemList itemlist, */ int index){
		var selectedSkill = (string) GetItemMetadata(index);
		// var isSkill = Enum.TryParse(selectedSkill, out SkillNames.All stringToEnum);
		// var skillEnum = isSkill ? stringToEnum : default;
		EmitSignal(SignalName.SelectedSkillFromGroup, selectedSkill, _skillGroup.ToString());
	}
}
