using System;

namespace Inventory
{
    public interface Gearable{
        public string Head {get;set;}
        public string Torso {get;set;}
        public string Weapon {get;set;}
        public string OffHand {get;set;}
    }   

    public interface StatBasedGear{
        public int GetTotalGearBaseDefense();
        public int GetTotalGearBaseDamage();
    } 
}

