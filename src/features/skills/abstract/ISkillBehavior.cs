using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace Skills;

public interface Traversing{ 
    public void ProcessPath(List<Vector2I> path);
    public Task ProcessPathAsync(List<Vector2I> path);
}