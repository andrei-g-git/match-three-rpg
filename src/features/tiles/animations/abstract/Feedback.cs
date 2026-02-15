using Godot;
using System;

namespace Animations
{
	public interface DisplayableNumber{
		public void DisplayNumberAt(Vector2I position, int value);
		public void DisplayNumber(int value);
	}
}
