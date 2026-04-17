using Godot;
using System;
using UI;

public partial class SpriteStatusBar : HBoxContainer, ProgressableBar
{

	[Export] Sprite2D _sprite;
	[Export] Color _filledColor;
	[Export] Color _depletedColor;

    public override void _Ready(){
		// _sprite.Material.Set("shader_parameter/", 0f);
		// _sprite.Material.Set("shader_parameter/", );
		// _sprite.Material.Set("shader_parameter/", );
		// _sprite.Material.Set("shader_parameter/", );
		// _sprite.Material.Set("shader_parameter/", );
		// _sprite.Material.Set("shader_parameter/", );
		// _sprite.Material.Set("shader_parameter/", );
	}

    public void Update(int value, int maxValue){
		_sprite.Material.Set("shader_parameter/energy", (float) value/maxValue);
		_sprite.Material.Set("shader_parameter/depleted_r", _depletedColor.R/255);
		_sprite.Material.Set("shader_parameter/depleted_g", _depletedColor.G/255);
		_sprite.Material.Set("shader_parameter/depleted_b", _depletedColor.B/255);
		_sprite.Material.Set("shader_parameter/filled_r", _filledColor.R/255);
		_sprite.Material.Set("shader_parameter/filled_g", _filledColor.G/255);
		_sprite.Material.Set("shader_parameter/filled_b", _filledColor.B/255);
    }


}
