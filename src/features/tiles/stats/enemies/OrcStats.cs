using Godot;
using Stats;

public partial class OrcStats: Node, WithHealth, WithDefense, WithDamage, WithSpeed, WithStrength, WithIntelligence, WithAgility 
{
    [Export] int _maxHealth;
    [Export] int _defense;
    [Export] int _damage;
    [Export] int _speed;
    [Export] int _strength;
    [Export] int _intelligence;
    [Export] int _agility;
    public int Health{get;set;} //needs to pick off from save 
    public int MaxHealth => _maxHealth; //might need setter if it can suffer health debuffs, but that's neither here or there
    public int Defense     { //I probably won't have to set these programatically ... maybe they can be changed with buffs and dbuffs
        get => _defense;
        set{
            _defense = value;
    }}
    public int Damage     {
        get => _damage;
        set{
            _damage = value;
    }}
    public int Speed     {
        get => _speed;
        set{
            _speed = value;
    }}

    public int Strength { 
        get => _strength;
        set{
            _strength = value;    
    }}

    public int Intelligence { 
        get => _intelligence;
        set{
            _intelligence = value;    
    }}

    public int Agility { 
        get => _agility;
        set{
            _agility = value;    
    }}

    public override void _Ready()
    {
        Health = _maxHealth; //obviously only for testing
        var bp = 123;
    }

}