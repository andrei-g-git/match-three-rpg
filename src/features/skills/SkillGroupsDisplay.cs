using Godot;
using System;
using System.Threading.Tasks;

public partial class SkillGroupsDisplay : VBoxContainer
{
	[Signal] public delegate void SkillPickedEventHandler(string name);

	public override void _Ready()
	{
		SetProcessInput(false);
		Modulate = new Color(0.6f, 0.6f, 0.6f, 1);
	}

	public async Task<string> EnableSkillPicking(){
		//make interface clickable
		if (!IsProcessingInput()){
			SetProcessInput(true);

			Modulate = new Color(1, 1, 1, 1);
		}

		// var tcs = new TaskCompletionSource<bool>();
    	// void Handler()    {        
		// 	tcs.TrySetResult(true);    
		// }

		var parameters = await ToSignal(this, SignalName.SkillPicked);
		var pickedSkill = (string) parameters[0];


		SetProcessInput(false);
		Modulate = new Color(0.6f, 0.6f, 0.6f, 1);

		return pickedSkill;
	}

	public void TestEmitSkillPicked(string skillName)
	{
		EmitSignal(SignalName.SkillPicked, skillName);
	}
}
