using Godot;
using System;
using System.Collections.Generic;
using System.IO;

namespace Levels;

public static class AllLevels{
	public static List<LevelSchema> Levels{get;} = [
		new LevelSchema{
			Index = 0,
			Name = LevelNames.Tutorial.ToString(),
			Objective = Objectives.DefeatAllEnemies.ToString(),
			Environment	= Path.Join(Files.LevelEnvironmentsPath, "level_1_environment.csv"),
			Pieces = Path.Join(Files.LevelPiecesPath, "level_1_pieces.csv"),		
			Upcoming = Path.Join(Files.LevelUpcomingPath, "level_1_upcoming.csv"),
			UpcomingBg = Path.Join(Files.LevelUpcomingBgPath, "level_1_upcoming_bg.csv"),
		}
	];
}