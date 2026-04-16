using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class SkillGroupsDisplay : VBoxContainer //this is actually not needed if I can only have skills for one skill group per match group
{
	[Export] 
	private PackedScene _elementSkillsDisplay;	
	[Signal] public delegate void SkillPickedEventHandler(string name);

	public override void _Ready()
	{
		// SetProcessInput(false);
		// Modulate = new Color(0.6f, 0.6f, 0.6f, 1);
	}

	public void Update(/* SkillNames.All[] skills, */dynamic[] skills, SkillNames.SkillGroups element){
		foreach(var node in GetChildren()){
			RemoveChild(node);
			node.QueueFree();
		}

		//foreach(var skill in skills){
			if(skills.Length > 0){
				var elementSkillsNode = _elementSkillsDisplay.Instantiate();
				AddChild(elementSkillsNode);
				(elementSkillsNode as ElementSkillsDisplay).TestParent = this;
				(elementSkillsNode as ElementSkillsDisplay).Update(skills, element);
			}
		//}
	}

	// public async Task<string> EnableSkillPicking(){
	// 	//make interface clickable
	// 	if (!IsProcessingInput()){
	// 		SetProcessInput(true);

	// 		Modulate = new Color(1, 1, 1, 1);
	// 	}

	// 	// var tcs = new TaskCompletionSource<bool>();
    // 	// void Handler()    {        
	// 	// 	tcs.TrySetResult(true);    
	// 	// }

	// 	var parameters = await ToSignal(this, SignalName.SkillPicked);
	// 	var pickedSkill = (string) parameters[0];


	// 	SetProcessInput(false);
	// 	Modulate = new Color(0.6f, 0.6f, 0.6f, 1);

	// 	return pickedSkill;
	// }

	public void TestEmitSkillPicked(string skillName)
	{
		EmitSignal(SignalName.SkillPicked, skillName);
	}
}
