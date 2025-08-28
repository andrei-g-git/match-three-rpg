using Godot;
using Stats;
using System;

public partial class PlayerDerivedStats : Node, DerivableStats
{
    public Attributive Attributes {private get; set;}
    private int _health;
    public int Health{
        get => _health;
        set => _health = Math.Clamp(value, 0, GetMaxHealth());
    }
    private int _energy;
    public int Energy{
        get => _energy;
        set =>_energy = Math.Clamp(value, 0, GetMaxEnergy());
    }    

    public override void _Ready(){
        //GD.Print("max energy:  ", GetMaxEnergy());
    }

    public int GetMaxEnergy(){
        //provisory
		//GD.Print(Attributes.Intelligence * 2);
		return Attributes.Intelligence * 2;
    }
    public static int GetMaxEnergy(int intelligence){
        return intelligence * 2;
    }
    public int GetMaxHealth(){
        //provisory
		GD.Print(Attributes.Intelligence * 2);
		return Attributes.Constitution * 2;
    }  
    public static int GetMaxHealth(int health){
        return health * 2;
    }  
}
