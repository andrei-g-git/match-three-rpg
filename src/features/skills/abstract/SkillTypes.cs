using System;
using System.Collections.Generic;

//skill types as in the data type of the skill, not what kind of skill it is. I can't use interfaces in the serialization process so I have to make classes as TS-style types
namespace Skills{
    public class SkillWithCount: CountableSkill
    {
        private string _name;
        public /* SkillNames.All */string Name{
            get => _name;
            set{
                var isSkill = Enum.TryParse(value, out SkillNames.All valueToEnum);
                _name =  isSkill? value : default;
            }
        }
        public int Uses{get;set;}
        public SkillNames.All GetSkillEnum(){
            var isSkill = Enum.TryParse(_name, out SkillNames.All valueToEnum);
            return isSkill ? valueToEnum : default;  //maybe default is not such a hot idea...      
        }
    }  

    public class SkillGroup: GroupableSkills{
        private string _group;
        public string Group{
            get => _group;
            set{
                var isGroup = Enum.TryParse(value, out SkillNames.SkillGroups valueToEnum);
                _group =  isGroup? value : default;
            }
        }  

        public /* List< */SkillWithCount[]/* > */ Skills{get;set;}
        public string _selected;
        public string Selected {
            get => _selected;
            set{
                var isSkill = Enum.TryParse(value, out SkillNames.All valueToEnum);
                _selected =  isSkill? value : default;                
            } 
        }

        public SkillNames.SkillGroups GetGroupEnum(){
            var isSkill = Enum.TryParse(_group, out SkillNames.SkillGroups valueToEnum);
            return isSkill ? valueToEnum : default;  
        }   

        public SkillNames.All GetSelected(){
            var isSkill = Enum.TryParse(_group, out SkillNames.All valueToEnum);
            return isSkill ? valueToEnum : default; 
        }       
    }  
}