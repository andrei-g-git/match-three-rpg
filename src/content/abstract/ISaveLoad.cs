using System.Collections.Generic;
using Inventory;
using Skills;
using Tiles;

namespace Content;
public interface CurrentlySavable{
    public string CurrentSave{get;}
}

public interface SavableGame{
    public int LevelIndex{get; set;}
    public string LevelName{get; set;}
    public int Turn{get; set;}
    public string Environment{get; set;}
    public string Pieces{get; set;}
    public SavablePlayer Player{get; set;}
    public List<object> OtherStatefulPieces{get; set;}
}

public interface SavablePlayer{
	public StatBased Stats{get;set;}
	public List<CountableSkill> Skills{get;set;}
	public Gearable Equipment{get;set;} 
}
