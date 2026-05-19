using Godot;
using Stats;
using System;

public abstract class ActiveEffect/*  : Effectful */
{
    public int MaxDuration { get; set; }
    public int TurnsLeft { get; set; }
    public Effects Type { get; set; }

    public abstract void ApplyToStats(Node stats);

    public abstract void RemoveFromStats(Node stats); //this isn't a great solution, but the cleaner, or less problematic alternative would be to ditch the applyToStats interface too and factor in all effects each time I do something involving stat changes, derived or otherwise, which is too bothersome
}
