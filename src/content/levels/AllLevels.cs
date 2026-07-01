using Godot;
using System;
using System.Collections.Generic;
using System.IO;

namespace Levels;

public class AllLevels{
	public List<LevelSchema> Levels{get;} = [
		new LevelSchema{
			Index = 0,
			Name = LevelNames.Tutorial.ToString(),
			Objective = Objectives.DefeatAllEnemies.ToString(), //it's already a string ... it's not an enum, it's a static class
			Environment	= Path.Join(Files.LevelEnvironmentsPath, "level_1_environment.csv"),
			Pieces = Path.Join(Files.LevelPiecesPath, "level_1_pieces.csv"),		
			Upcoming = Path.Join(Files.LevelUpcomingPath, "level_1_upcoming.csv"),
			UpcomingBg = Path.Join(Files.LevelUpcomingBgPath, "level_1_upcoming_bg.csv"),
			Modifiers = [
				LevelModifiers.vertical_match_multiplier.ToString()
			]
		},
		new LevelSchema{
			Index = 1,
			Name = LevelNames.InfiniteTutorial.ToString(),
			Objective = Objectives.Survive.ToString(),
			Environment	= Path.Join(Files.LevelEnvironmentsPath, "level_2_environment.csv"),
			Pieces = Path.Join(Files.LevelPiecesPath, "level_2_pieces.csv"),		
			Upcoming = Path.Join(Files.LevelUpcomingPath, "level_2_upcoming.csv"),
			UpcomingBg = Path.Join(Files.LevelUpcomingBgPath, "level_2_upcoming_bg.csv"),
			Modifiers = [
				LevelModifiers.random_upcoming_pieces.ToString(),
				LevelModifiers.infinite_upcoming_pieces.ToString()
			],
			RandomPieceDistribution = new List<PieceOdds> {
				new(){
					Piece = "melee",
					Odds = 5
				},
				new(){
					Piece = "ranged",
					Odds = 5
				},
				new(){
					Piece = "defensive",
					Odds = 5
				},
				new(){
					Piece = "tech",
					Odds = 5
				},
				new(){
					Piece = "fighter",
					Odds = 1
				}
			}
		}
	];
}
