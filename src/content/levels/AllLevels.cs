using Godot;
using System;
using System.Collections.Generic;

namespace Levels;
public partial class AllLevels{
	public List<LevelSchema> Levels = [
		new LevelSchema{
			Index = 0,
			Name = LevelNames.Tutorial.ToString(),
			Objective = Objectives.DefeatAllEnemies.ToString(),
			Environment	= "content/levels/environment/level_1_environment.csv",
			Pieces = "content/levels/pieces/level_1_pieces.csv"		
		}
	];
}