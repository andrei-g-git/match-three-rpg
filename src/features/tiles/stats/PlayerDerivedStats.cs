using Godot;
using Stats;
using System;

public partial class PlayerDerivedStats : Node, DerivableStats
{
    public Attributive Attributes {private get; set;}

    public override void _Ready()
    {
        //GD.Print("max energy:  ", GetMaxEnergy());
    }

    public int GetMaxEnergy(){
        //provisory
		GD.Print(Attributes.Intelligence * 2);
		return Attributes.Intelligence * 2;
    }
}
