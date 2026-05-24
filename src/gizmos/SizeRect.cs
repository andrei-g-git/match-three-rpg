using Godot;
using System;

public partial class SizeRect : PanelContainer
{
	[Export] private int _maxResizeCount = 2;
	[Export] private Color _color = Colors.Black; 
	private int _resizeCount = 0;
	public override void _Draw(){
		DrawRect(new Rect2(0, 0, Size), _color, false, 3);
	}

	public void Resize(Vector2 size){
		if(_resizeCount < _maxResizeCount){
			CustomMinimumSize = size;
			_resizeCount++;
			var bp = 123;			
		}
		//SizeFlagsVertical = (int)SizeFlags.ShrinkBegin;
	}	
}
