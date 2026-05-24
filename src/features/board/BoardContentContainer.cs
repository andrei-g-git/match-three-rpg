using Godot;
using System;

public partial class BoardContentContainer : MarginContainer
{
	//private Vector2 _sizeSumForPiecesAndUpcoming;
	private int _resizeCount = 0;
	public void Resize(Vector2 size){
		if(_resizeCount < 2){
			CustomMinimumSize += new Vector2(0, size.Y);
			_resizeCount++;
			var bp = 123;			
		}
	}
}
