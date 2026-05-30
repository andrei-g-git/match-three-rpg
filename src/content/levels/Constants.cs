using Godot;
using System;

namespace Levels{
	public static class LevelNames{ //should turn back into enums, because I can't define values as this type if they're just constant props
	//public enum LevelNames{
		public const string Tutorial = "tutorial";
	}

	public static class Objectives{
	//public enum Objectives{
		public const string DefeatAllEnemies = "defeat_all_enemies";
		public const string EscapeOnExit = "escape_on_exit";
	}

	// public static class RoomModifiers{
	// 	public const string VerticalMatchMultiplier = "vertical_match_multiplier";
	// }

	public enum LevelModifiers{
		vertical_match_multiplier
	}	
}

