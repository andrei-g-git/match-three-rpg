using Board;
using Common;
using Godot;
using System;
using System.Threading.Tasks;

public partial class ProjectileTween : Control, Mapable
{
	[Export] private Texture2D _spriteTexture;
	[Export] private float _durationPerTile;
	[Export] private PackedScene _projectileScene;
	[Export] private AnimatedSprite2D _impactEffect;
	[Export] private Control _container; //prob not necessary

    public Tileable Map { private get; set; }

    [Signal] public delegate void FinishedFlyingEventHandler();


    public override void _Ready(){
        _impactEffect.Visible = false;
    }


	public Task FlyTo(Vector2I targetCell, int tilesTraveled){
		var pixelTarget = (Vector2) Map.CellToPosition(targetCell);

		var source = Position; GD.Print($"thrower position:   {source}");  GD.Print($"target position:   {pixelTarget}"); GD.Print($"TARGET CELL:   {targetCell}");
		var projectile = _projectileScene.Instantiate();
		(projectile as Sprite2D).Texture = _spriteTexture;
		AddChild(projectile);

		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		tween.TweenProperty(projectile/* _container */, "position", pixelTarget, (float)(_durationPerTile * tilesTraveled));

		var tcs = new TaskCompletionSource<bool>();

		//_impactEffect.Visible = false;

		_impactEffect.AnimationFinished += () => {
			_impactEffect.Visible = false;
			//_container.Position = source;
		};

		tween.Finished += () => {
			EmitSignal(SignalName.FinishedFlying); 

			_impactEffect.Visible = true;
			_impactEffect.Position = pixelTarget;
			_impactEffect.Play();

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
