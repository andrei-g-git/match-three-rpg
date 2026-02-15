using Godot;
using System;

public static class MathUtils{
	public static Vector2 InvertVector(Vector2 old){
        float x = old.Y;
        float y = old.X;
        return new Vector2(x, y);
    }
}
