using Board;
using Common;
using Godot;
using System;
using Tiles;

public partial class Offense : Node, Offensive
{
    public Tileable Map {private get;set;}

    [Signal] public delegate void AttackedEventHandler(Control targetTile);
	public void Attack(Control target/* , int momentum */){
            //if(target is Disposition dfdg){
                //if((actor as Disposition).IsEnemy){ 
                    var damage = 4; //provisory
                    (target as Defensible).TakeDamage(damage);  
                    //assume actor is adjacent
                    EmitSignal(SignalName.Attacked, target);      
                //}
            //}
        }
}
