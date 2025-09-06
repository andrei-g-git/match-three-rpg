using Godot;
using System;
using Tiles;

public partial class FlashWhite : Node, WhiteFlashable
{
	[Export] private Control _tileRoot;
	[Export] private float _duration;
    [Export] private Sprite2D _sprite;


	public void FlashOnce(){
		var originalColor = _tileRoot.Modulate; //this is for the root, not the sprite!!!
        //var originalColor = _sprite.Modulate;
        //_sprite.Modulate = Colors.Red;//Color.FromHsv(originalColor.H, 0, originalColor.V);
		var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Sine);
        var toWhite = tween.TweenProperty(
            _tileRoot,
            "modulate",
            Colors.Red,
            _duration
        );

        var toOriginal = tween.TweenProperty(
            _tileRoot,
            "modulate",
            originalColor,
            _duration
        )
			.SetDelay(_duration);		

		// tween.TweenProperty(_tileRoot, "modulate", Colors.White, _duration)
		// 	.SetTrans(Tween.TransitionType.Sine);
		// tween.TweenProperty(_tileRoot, "modulate", originalColor, _duration)
		// 	.SetDelay(_duration)
		// 	.SetTrans(Tween.TransitionType.Sine);
	}
}

/* 

Likely causes and quick checks (in order):

    Alpha mismatch

    If originalColor.a == 0 (fully transparent) you won’t see the white flash. Ensure alpha preserved: var white = new Color(1f,1f,1f, originalColor.a);

    Something else immediately overwrites Modulate

    Another tween, AnimationPlayer, or code may set Modulate after your tween runs. Temporarily stop other tweens/animations or print when Modulate changes: GD.Print($"before: { _tileRoot.Modulate }"); // start flash // in _Process: GD.Print(_tileRoot.Modulate);

    Tween being killed or garbage-collected

    If the SceneTreeTween variable is local and you're not keeping a reference, Godot still runs it, but if you used a Tween node and freed it, it may stop. Keep a SceneTreeTween field and Kill() it when needed. Example: _currentFlashTween?.Kill(); _currentFlashTween = CreateTween(); _currentFlashTween.TweenProperty(...);
    Also ensure you aren't calling RemoveTweens or clearing the node that owns the tween.

    Overlapping tweens on same property

    Two tweens (from your other animation and this flash) fighting the same property will cancel/overwrite each other. If the other tween runs first and keeps changing modulate continuously, your flash will be invisible. Temporarily disable the other tween and test.

    Target property path correctness

    "modulate" is correct for CanvasItem/Control. If _tileRoot is not a CanvasItem (or it's a container with children showing visuals), you're changing the wrong node. Confirm _tileRoot is the visual node (Sprite/TextureRect/Control) that actually displays color.

    Tween configured but transition/ease hides it

    Sine is fine; still try Linear and longer duration to test: .TweenProperty(..., 1.5f).SetTrans(Tween.TransitionType.Linear)

    Ensure code actually runs

    Add a print right before creating the tweens to confirm FlashOnce() executes.

Minimal debug version to try — keep a reference and preserve alpha, and stop other tweens:

csharp

SceneTreeTween _currentFlashTween;

public void FlashOnce(){
    GD.Print("FlashOnce called");
    var originalColor = _tileRoot.Modulate;
    var white = new Color(1f,1f,1f, originalColor.a);

    _currentFlashTween?.Kill();
    _currentFlashTween = CreateTween();

    _currentFlashTween.TweenProperty(_tileRoot, "modulate", white, 0.9f)
        .SetTrans(Tween.TransitionType.Linear);
    _currentFlashTween.TweenProperty(_tileRoot, "modulate", originalColor, 0.9f)
        .SetDelay(0.9f)
        .SetTrans(Tween.TransitionType.Linear);
}

Run with the other tween disabled. If this works, the problem is overlapping tweens or something overwriting modulate. If it still doesn’t, paste what type _tileRoot is and the other tween code so I can point to the exact conflict.

 */