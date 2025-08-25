namespace Inventory;

public interface Gearable{
    public Helmets Head {get;set;}
    public Armors Torso {get;set;}
    public Weapons Weapon {get;set;}
    public OffHands OffHand {get;set;}
}