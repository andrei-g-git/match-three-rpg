using Godot;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class Pop : Node, Creatable
{
	[Export] private Node _tileRoot;
	[Export] private float _duration;

	//[Signal] public delegate void CreatedEventHandler();
	private Tween _tween;

    void Creatable.Pop(){
		(_tileRoot as Control).Scale = Vector2.Zero;
		
		/* var  */_tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		_tween.TweenProperty(_tileRoot, "scale", Vector2.One, _duration);

		// tween.Finished += () => {
		// 	EmitSignal(SignalName.Created)
		// }
    }

	public async Task WaitUntilCreated(){
		await ToSignal(/* this */_tween, Tween.SignalName.Finished);
	}
}
