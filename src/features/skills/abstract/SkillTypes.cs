using System;

namespace Skills{
    public class SkillWithCount//: CountableSkill
    {
        private string _name;
        public /* SkillNames.All */string Name{
            get => _name;
            set{
                SkillNames.All valueToEnum;
                var isSkill = Enum.TryParse(value, out valueToEnum);
                _name =  isSkill? value : default;
            }
        }
        public int Uses{get;set;}
    }    
}