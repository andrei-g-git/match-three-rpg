using Godot;
using Inventory;
using System;

public partial class EquipmentModel : Node, Gearable
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

    [Signal] public delegate void EquipmentChangedEventHandler(string type, string item);

}
