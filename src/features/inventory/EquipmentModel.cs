using Godot;
using Inventory;
using System;
using System.Linq;

public partial class EquipmentModel : Node, Gearable, StatBasedGear
{
    // this is stupid, this should be the model, add business logic here, it will not serialize the interface
	//public Gearable Gear{private get;set;} 
    public string _head;
    public string Head { 
        get => _head; 
        set{
            _head = value; 
            EmitSignal(SignalName.EquipmentChanged, EquipmentTypes.Head.ToString().ToLower(), value);
        }
    }
    private string _torso;
    public string Torso { 
        get => _torso; 
        set{
            _torso = value;
            EmitSignal(SignalName.EquipmentChanged, EquipmentTypes.Torso.ToString().ToLower(), value);
        } 
    }
    private string _weapon;
    public string Weapon { 
        get => _weapon; 
        set{
            _weapon = value;
            EmitSignal(SignalName.EquipmentChanged, EquipmentTypes.Weapon.ToString().ToLower(), value);
        } 
    }
    private string _offHand;
    public string OffHand { 
        get => _offHand; 
        set{
            _offHand = value;
            EmitSignal(SignalName.EquipmentChanged, EquipmentTypes.OffHand.ToString().ToLower(), value);
        } 
    }

    public AllGearData _possibleGear = new(); //obviously this should be loaded from the board manager/etc and passed here somehow

    [Signal] public delegate void EquipmentChangedEventHandler(string type, string item);




    public int GetTotalGearBaseDefense(){
        var defense = 0;
        if(_head != null){
            var headgearData = _possibleGear.Gear.Where(item => item.Name == _head).ElementAt(0);
            defense += headgearData.Defense;
        }
        if(_torso != null){
            var torsoData = _possibleGear.Gear.Where(item => item.Name == _torso).ElementAt(0);
            defense += torsoData.Defense;
        }
        if(_offHand != null){
            var offHandData = _possibleGear.Gear.Where(item => item.Name == _offHand).ElementAt(0);
            defense += offHandData.Defense;
        } 
        return defense;               
    }

    public int GetTotalGearBaseDamage(){
        var damage = 0;
        if(_weapon != null){
            var weaponData = _possibleGear.Gear.Where(item => item.Name == _weapon).ElementAt(0);
            damage += weaponData.Damage;
        }
        return damage;
    }
}
