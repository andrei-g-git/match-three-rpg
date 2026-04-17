using System;
using Skills;

namespace Stats{
    public class ActorStats//: StatBased
    {
        public /* Attributive */Attributes Attributes{get;set;}
        public int Health { get; set; }
        public int Energy { get; set; }
        public Energies Energies{get;set;}
        public int Speed { get; set; }
        public int Defense { get; set; }
    }

    // public class Attributes: Attributive   //actually this should be here
    // {
    //     public int Strength{get;set;}
    //     public int Agility{get;set;}
    //     public int Constitution{get;set;}
    //     public int Intelligence{get;set;}
    // }  

    public class Energies: WithEnergy{
        public int MaxFireEnergy{get;set;} 
        private int _FireEnergy;
        public int FireEnergy{
            get => _FireEnergy;
            set { _FireEnergy = Math.Clamp(value, 0, MaxFireEnergy); }
        }
        public int MaxWindEnergy{get;set;}
        private int _WindEnergy;
        public int WindEnergy{
            get => _WindEnergy;
            set { _WindEnergy = Math.Clamp(value, 0, MaxWindEnergy); }
        }
        public int MaxEarthEnergy{get;set;}
        private int _EarthEnergy;
        public int EarthEnergy{
            get => _EarthEnergy;
            set { _EarthEnergy = Math.Clamp(value, 0, MaxEarthEnergy); }
        }
        public int MaxWaterEnergy{get;set;}
        private int _WaterEnergy;
        public int WaterEnergy{
            get => _WaterEnergy;
            set { _WaterEnergy = Math.Clamp(value, 0, MaxWaterEnergy); }
        }     
    }
}