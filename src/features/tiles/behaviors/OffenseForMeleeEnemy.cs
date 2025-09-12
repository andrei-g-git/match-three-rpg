using Godot;
using Stats;
using Tiles;

public partial class OffenseForMeleeEnemy: Node, Offensive
{
    [Export] public Node _stats;
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
                var damage = (_stats as WithDamage).Damage;
                (target as Defensible).TakeDamage(damage);  
                //assume target is adjacent
                EmitSignal(SignalName.Attacked, target);      
            }
        }         
    }    
}