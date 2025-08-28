using Godot;
using System;
using UI;

public partial class StatusBar : ProgressBar, ProgressableBar
{

    public override void _Ready(){
		MinValue = 0;
		MaxValue = 0;
		Value = 0;		
	}

	public void Update(int value, int maxValue){
		MaxValue = maxValue;
		Value = value;
		var bp = 123;
    }
}
