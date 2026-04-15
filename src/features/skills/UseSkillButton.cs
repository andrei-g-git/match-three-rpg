using Godot;
using Skills;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class UseSkillButton : TextureButton
{
	[Export] private Label _label;

	[Signal] public delegate void ClickedSkillEventHandler(string name);

	public override void _Ready()
	{
	}

	public void SetSkillLabel(/* SkillNames.All skillName */string skillName){
		_label.Text = skillName;
	}

	private async void Foo()
	{
		GD.Print("CLICKED SKILL BUTTON");

 		var border = new ColorRect();
		border.MouseFilter = Control.MouseFilterEnum.Ignore;    
		border.Color = new Color(1f, 1f, 0f, 0.5f);    
		AddChild(border);

    	border.AnchorLeft = 0f;    border.AnchorTop = 0f;    border.AnchorRight = 1f;    border.AnchorBottom = 1f;
  
		await Task.Delay(100);
	
		RemoveChild(border);
		border.QueueFree();

		EmitSignal(SignalName.ClickedSkill, _label.Text);
	}

	public void ConnectClickedSkill(Action<string> action){
		Connect(SignalName.ClickedSkill, Callable.From(action));
	}


}
