using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class SkillGroupsDisplay : Control/* VBoxContainer */ //this is actually not needed if I can only have skills for one skill group per match group
{
	[Export] private PackedScene _elementSkillsDisplay;	
	//[Export] private Control _elementSkillsDisplay;
	[Signal] public delegate void SkillPickedEventHandler(string name);

	public override void _Ready()
	{
		Visible = false;
		// SetProcessInput(false);
		// Modulate = new Color(0.6f, 0.6f, 0.6f, 1);
	}

	public void Update(/* SkillNames.All[] skills, */dynamic[] skills, SkillNames.SkillGroups element){

		foreach(var node in GetChildren()){
			RemoveChild(node);
			node.QueueFree();
		}

		if(skills.Length > 0){
			var elementSkillsNode = _elementSkillsDisplay.Instantiate();
			AddChild(elementSkillsNode);
			(elementSkillsNode /* _elementSkillsDisplay */ as ElementSkillsDisplay).TestParent = this;
			(elementSkillsNode /* _elementSkillsDisplay */ as ElementSkillsDisplay).Update(skills, element);
		}

		PopIn();	
	}

	private void PopIn(){
		Visible = true;

		var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "scale", Vector2.One, 0.5f)
			.From(Vector2.Zero);
		// tween.Join()
		// 	.TweenProperty(this, "modulate", new Color(1, 1, 1, 1), 0.5f)
		// 	.From(new Color(1, 1, 1, 0));
	}

	private void PopOut(string skillName){ //when Update runs, PopIn overrides this while it's still playing...
		Visible = false;

		var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "scale",  Vector2.Zero, 0.5f)
			.From(Vector2.One);
		//tween.TweenProperty(this, "modulate.a", 1.0f, 0.0f); //apparently modulate.a isn't for Panel

		tween.Finished += () => EmitSignal(SignalName.SkillPicked, skillName);
		
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
		PopOut(skillName);
	}
}
