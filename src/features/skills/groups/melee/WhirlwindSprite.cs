using Common;
using Godot;
using System;
using System.Threading.Tasks;

public partial class WhirlwindSprite : AnimatedSprite2D, AnimatableSprite
{
	//[Signal] public delegate void SkillAnimationFinishedEventHandler();

	public void PlayOnce(){
		Play();
		//AnimationFinished += () => EmitSignal(SignalName.SkillAnimationFinished);
	}

	public async Task WaitForAnimationFinished(){
		await ToSignal(this, AnimatedSprite2D.SignalName.AnimationFinished);
	}
}
