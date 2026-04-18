using Godot;
using System;
using System.Threading.Tasks;
using UI;

public partial class SpriteStatusBar : VBoxContainer, ProgressableBar
{

	[Export] Label _label;
	[Export] Sprite2D _sprite;
	[Export] Color _filledColor;
	[Export] Color _depletedColor;

	private ShaderMaterial _instanceMaterial;
	public override void _Ready(){
		var material = _sprite.Material as ShaderMaterial;
		if(material != null){
			var instance = material.Duplicate(true) as ShaderMaterial;
			_sprite.Material = instance;
			_instanceMaterial = instance;
		}
	}

	public void Update(int value, int maxValue){
		_label.Text = $"{value}/{maxValue}";

		var shaderMaterial = _sprite.Material as ShaderMaterial;
		_instanceMaterial.SetShaderParameter("energy", (float) value/maxValue );	
		_instanceMaterial.SetShaderParameter("depleted_r", _depletedColor.R);	
		_instanceMaterial.SetShaderParameter("depleted_g", _depletedColor.G);
		_instanceMaterial.SetShaderParameter("depleted_b", _depletedColor.B);
		_instanceMaterial.SetShaderParameter("filled_r", _filledColor.R);
		_instanceMaterial.SetShaderParameter("filled_g", _filledColor.G);
		_instanceMaterial.SetShaderParameter("filled_b", _filledColor.B);

	}

	private async void RunDelayedAction(int value, int maxValue)    {        
		await Task.Delay(5000); 
		GD.Print("UPDATE------------");
		_instanceMaterial.SetShaderParameter("energy", (float) value/maxValue );
		_instanceMaterial.SetShaderParameter("depleted_r", _depletedColor.R/255f);
		_instanceMaterial.SetShaderParameter("depleted_g", _depletedColor.G/255f);
		_instanceMaterial.SetShaderParameter("depleted_b", _depletedColor.B/255f);
		_instanceMaterial.SetShaderParameter("filled_r", _filledColor.R/255f);
		_instanceMaterial.SetShaderParameter("filled_g", _filledColor.G/255f);
		_instanceMaterial.SetShaderParameter("filled_b", _filledColor.B/255f);	
	}
}
