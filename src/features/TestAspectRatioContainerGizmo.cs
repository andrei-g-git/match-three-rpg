using Godot;
using System;

public partial class TestAspectRatioContainerGizmo : AspectRatioContainer
{
	public override void _Draw()
	{
		DrawRect(new Rect2(10f, 10f, 35f, 25f), Colors.Green, false);
	}


}
