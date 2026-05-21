using Godot;
using System;

public partial class BoardContentContainer : MarginContainer
{
	public void Resize(Vector2 size){
		CustomMinimumSize = size;
	}
}
