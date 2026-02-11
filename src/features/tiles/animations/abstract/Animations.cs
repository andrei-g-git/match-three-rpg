using Godot;
using System;

namespace Animations
{
	public interface Animatable{
		public void Play(StringName animationName); 
		public void Stop(StringName animationName); 
	}	
}

