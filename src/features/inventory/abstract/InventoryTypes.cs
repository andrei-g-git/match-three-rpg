using System;
using Godot;

namespace Inventory{
    public partial class Gear: Gearable 
    {
        private string _head;
        public string Head {
            get => _head;
            set{
                Helmets valueToEnum;
                var isSkill = Enum.TryParse(value, out valueToEnum);
                _head =  isSkill? value : default;                
            }
        }
        private string _torso;
        public string Torso {
            get => _torso;
            set{
                Armors valueToEnum;
                var isSkill = Enum.TryParse(value, out valueToEnum);
                _torso =  isSkill? value : default; 
            }
        }
        private string _weapon;
        public string Weapon {
            get => _weapon;
            set{
                Weapons valueToEnum;
                var isSkill = Enum.TryParse(value, out valueToEnum);
                _weapon =  isSkill? value : default; 
            }
        }
        private string _offHand;
        public string OffHand {
            get => _offHand;
            set{
                OffHands valueToEnum;
                var isSkill = Enum.TryParse(value, out valueToEnum);
                _offHand =  isSkill? value : default; 
            }
        }
    }  


    public record GearData{
        public string Name{get; init;} //should check if it parses to the item enum
        public string Purpose{get; init;} //same, should check enum GearPurpose{Armor, Weapon} or something
        public string Slot{get; init;}
        public int Damage{get; init;} //these are bad, there's no interface segregation, I should compose. If I need to later add stuff like special effects and more stats it will be unwieldy
        public int Defense{get; init;}
    }  
}

