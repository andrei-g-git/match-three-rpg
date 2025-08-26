namespace Stats;

public interface StatBased{
    public Attributive Attributes{get;set;}
    public int CurrentHealth{get;set;}
    public int CurrentEnergy{get;set;}
}

public interface Attributive{
    public int Strength{get;set;}
    public int Agility{get;set;}
    public int Constitution{get;set;}
    public int Intelligence{get;set;}
}
