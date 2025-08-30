using System.Collections.Generic;
using Inventory;
using Skills;
using Stats;

namespace Content{
    public class CurrentSaveGame{
        public string CurrentSave{get;set;}
    }

    public class GameSave{
        public int LevelIndex{get; set;}
        public string LevelName{get; set;}
        public int Turn{get; set;}
        public string Environment{get; set;}
        public string Pieces{get; set;}
        public /* SavablePlayer */PlayerSave Player{get; set;}
        public List<object> OtherStatefulPieces{get; set;}
    }

    public class PlayerSave/* : SavablePlayer */{ ///can't deserialize abstarctions
        public /* StatBased */ ActorStats Stats{get;set;}
        //public List</* CountableSkill */SkillWithCount> Skills{get;set;}
        public /* SkillGroup[] */ Dictionary<string, SkillGroup> SkillGroups { get; set; }
        public /* Gearable */Gear Equipment{get;set;} 
        public string Class{get;set;}
    }    
}
