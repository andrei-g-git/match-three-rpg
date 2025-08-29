using Godot;
using Stats;

public partial class OrcStats: Node, WithHealth, WithDefense, WithDamage, WithSpeed
{
    [Export] int _maxHealth;
    [Export] int _defense;
    [Export] int _damage;
    [Export] int _speed;
    public int Health{get;set;} //needs to pick off from save 
    public int MaxHealth => _maxHealth; //might need setter if it can suffer health debuffs, but that's neither here or there
    public int Defense     {
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

}