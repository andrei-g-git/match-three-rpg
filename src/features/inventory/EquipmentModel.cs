using Godot;
using Inventory;
using System;

public partial class EquipmentModel : Node, Gearable
{
	public Gearable Gear{private get;set;} // this is stupid, this should be the model, add business logic here, it will not serialize the interface
    public string Head { get => Gear.Head; set => Gear.Head = value; }
    public string Torso { get => Gear.Torso; set => Gear.Torso = value; }
    public string Weapon { get => Gear.Weapon; set => Gear.Weapon = value; }
    public string OffHand { get => Gear.OffHand; set => Gear.OffHand = value; }

}
