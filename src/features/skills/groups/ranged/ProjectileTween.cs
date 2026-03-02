using Godot;
using System;

public partial class ProjectileTween : Node2D
{
	[Export] private Texture2D _spriteTexture;
	[Export] private float _durationPerTile;
	[Signal] public delegate void FinishedFlyingEventHandler();

	public void FlyTo(Vector2 target, int tilesTraveled){


		var source = Position;

		var sprite = _MakeSprite(_spriteTexture);

		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		tween.TweenProperty(sprite, "position", source, (float)(_durationPerTile * tilesTraveled));

		
		tween.Finished += () => {
			EmitSignal(SignalName.FinishedFlying); 
			RemoveChild(sprite);
		};
	}


	private Sprite2D _MakeSprite(Texture2D texture){
		var node = new Sprite2D();
		node.Texture = texture;
		AddChild(node);
		return node;
	}
}
