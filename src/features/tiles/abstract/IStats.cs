namespace Tiles;

public interface StatBased{
    public Attributive Attributes{get;set;}
}

public interface Attributive{
    public int Strength{get;set;}
    public int Agility{get;set;}
    public int Constitution{get;set;}
    public int Intelligence{get;set;}
}
