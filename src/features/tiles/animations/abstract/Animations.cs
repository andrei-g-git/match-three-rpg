using Godot;
using System;

namespace Animations
{
	public interface Animatable{
		public void Play(StringName animationName); 
		public void Stop(StringName animationName); 
		
	}	

	public interface CustomizableGear{
		public void ChangeGear(string type, string item);
		//public void ChangeGearTemporarily(string type, string item, float duration);
		public string GetEquipedGearOfType(string type);
	}
}

