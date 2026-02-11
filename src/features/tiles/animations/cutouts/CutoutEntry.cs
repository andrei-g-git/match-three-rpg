using Godot;
using Inventory;

[GlobalClass]
public partial class CutoutEntry : Resource
{
    [Export] public Cutouts Cutout { get; set; }
    [Export] public Texture2D Texture { get; set; }
}
