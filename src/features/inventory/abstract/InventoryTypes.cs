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
}

