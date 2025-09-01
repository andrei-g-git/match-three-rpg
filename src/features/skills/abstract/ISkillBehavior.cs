using System.Collections.Generic;
using Godot;

namespace Skills;

public interface Traversing{ 
    public void ProcessPath(List<Vector2I> path);
}