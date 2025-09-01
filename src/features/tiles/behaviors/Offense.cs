using Board;
using Common;
using Godot;
using System;
using Tiles;

public partial class Offense : Node, Offensive
{
    [Export] private Node damageCalculator;
    public Tileable Map {private get;set;}
    [Signal] public delegate void AttackedEventHandler(Control targetTile);
    

	public void Attack(Control target){
        _AttackWithMomentum(target, 1);
    }

    public void AttackWithMomentum(Control actor, int momentum){
        _AttackWithMomentum(actor, momentum);
    }

    private void _AttackWithMomentum(Control target, int momentum){
        if(target is Disposition potentialEnemy){
            if(potentialEnemy.IsEnemy){ 
                var damage = (damageCalculator as CalculatableDamage).CalculateDamageFromMomentum(momentum);
                (target as Defensible).TakeDamage(damage);  
                //assume target is adjacent
                EmitSignal(SignalName.Attacked, target);      
            }
        }         
    }    
}
