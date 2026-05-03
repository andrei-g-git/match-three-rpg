using Common;
using Godot;
using Skills;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class UseSkillButton : VBoxContainer, DeactivatableButton
{
	[Export] private Label _label;
    [Export] private TextureButton _button;

	[Export] private Sprite2D _hexagonLine;

	[ExportGroup("Energy Requirements")]
	[Export] private Sprite2D _fireIcon;
	[Export] private Label _fireEnergyRequirement;
	[Export] private Sprite2D _windIcon;
	[Export] private Label _windEnergyRequirement;
	[Export] private Sprite2D _earthIcon;
	[Export] private Label _earthEnergyRequirement;
	[Export] private Sprite2D _waterIcon;
	[Export] private Label _waterEnergyRequirement;

	public TextureButton Button => _button;
	public Texture2D Texture{
		private get => _button.TextureNormal;
		set {_button.TextureNormal = value;}
	}
	public bool Active{get;set;} = false;

	[Signal] public delegate void ClickedSkillEventHandler(string name);

	public override void _Ready(){
		SetProcessInput(false);
		Modulate = new Color(0.6f, 0.6f, 0.6f, 1);		
	}

	public void Activate(){
		SetProcessInput(true);
		Modulate = new Color(1, 1, 1, 1);	
		Active = true;	

		_hexagonLine.Visible = false;	
	}

	public void Deactivate(){
		SetProcessInput(false);
		Modulate = new Color(0.6f, 0.6f, 0.6f, 1);	
		Active = false;	

		_hexagonLine.Modulate = Colors.Red;
		_hexagonLine.Visible = true;	
	}

	public void SetSkillLabel(/* SkillNames.All skillName */string skillName){
		_label.Text = skillName;
	}

	public void SetEnergyRequirements(int fire, int wind, int earth, int water){
		_fireEnergyRequirement.Text = fire.ToString();
		_windEnergyRequirement.Text = wind.ToString();
		_earthEnergyRequirement.Text = earth.ToString();
		_waterEnergyRequirement.Text = water.ToString();
	}


	public void DecideActivation(bool enoughFire, bool enoughWind, bool enoughEarth, bool enoughWater, bool meetsBoardRequirements){
		//I don't like branching
		// _fireIcon.Modulate = new Color(1, Convert.ToSingle(enoughFire), Convert.ToSingle(enoughFire), 1);
		// _windIcon.Modulate = new Color(1, Convert.ToSingle(enoughWind), Convert.ToSingle(enoughWind), 1);
		// _earthIcon.Modulate = new Color(1, Convert.ToSingle(enoughEarth), Convert.ToSingle(enoughEarth), 1);
		// _waterIcon.Modulate = new Color(1, Convert.ToSingle(enoughWater), Convert.ToSingle(enoughWater), 1);
		_fireEnergyRequirement.Modulate = new Color(1, Convert.ToSingle(enoughFire), Convert.ToSingle(enoughFire), 1);
		_windEnergyRequirement.Modulate = new Color(1, Convert.ToSingle(enoughWind), Convert.ToSingle(enoughWind), 1);
		_earthEnergyRequirement.Modulate = new Color(1, Convert.ToSingle(enoughEarth), Convert.ToSingle(enoughEarth), 1);
		_waterEnergyRequirement.Modulate = new Color(1, Convert.ToSingle(enoughWater), Convert.ToSingle(enoughWater), 1);	

		if(enoughFire && enoughWind && enoughEarth && enoughWater && meetsBoardRequirements){
			Activate();
		}
		else{
			Deactivate();
		}
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
