using Board;
using Common;
using Godot;
using System;
using System.Threading.Tasks;

public partial class ProjectileTween : Node2D, Mapable
{
	[Export] private Texture2D _spriteTexture;
	[Export] private float _durationPerTile;
	[Export] private PackedScene _projectileScene;

    public Tileable Map { private get; set; }

    [Signal] public delegate void FinishedFlyingEventHandler();

	public Task FlyTo(Vector2I targetCell, int tilesTraveled){
		var pixelTarget = (Vector2) Map.CellToPosition(targetCell);

		var source = Position; GD.Print($"thrower position:   {source}");  GD.Print($"target position:   {pixelTarget}"); GD.Print($"TARGET CELL:   {targetCell}");
		var projectile = _projectileScene.Instantiate();
		(projectile as Sprite2D).Texture = _spriteTexture;
		AddChild(projectile);

		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		tween.TweenProperty(projectile, "position", pixelTarget, (float)(_durationPerTile * tilesTraveled));

		var tcs = new TaskCompletionSource<bool>();

		tween.Finished += () => {
			EmitSignal(SignalName.FinishedFlying); 
			RemoveChild(projectile);
			tcs.SetResult(true);
		};
		return tcs.Task;
	}


	// private Sprite2D _MakeSprite(Texture2D texture){
	// 	var node = new Sprite2D();
	// 	node.Texture = texture;
	// 	AddChild(node);
	// 	return node;
	// }
}
