using Godot;
using Stats;
using System;

public abstract class ActiveEffect/*  : Effectful */
{
    public int MaxDuration { get; set; }
    public int TurnsLeft { get; set; }
    public Effects Type { get; set; }

    public abstract void ApplyToStats(Node stats);
}
