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
        public SavablePlayer Player{get; set;}
        public List<object> OtherStatefulPieces{get; set;}
    }

        public class PlayerSave: SavablePlayer{
            public StatBased Stats{get;set;}
            public List<CountableSkill> Skills{get;set;}
            public Gearable Equipment{get;set;} 
        }    
}
