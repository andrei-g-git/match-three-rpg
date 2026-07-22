using Common;
using Content;
using Godot;
using Inventory;
using Levels;
using Skills;
using Stats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Tiles;
using UI;

public partial class MainMenu : Control, WithSceneManager
{
	[Export] private PackedScene _firstLevelScene;
	public ChangeableScenes SceneManager{private get; set;}

	public override void _Ready()
	{
	}

	public void HandleMainItemPressed(string buttonName){
		MainMenuItems buttonEnum;
		Enum.TryParse(buttonName, out buttonEnum);
		switch(buttonEnum){
			case MainMenuItems.NewGame:
                // var firstLevel = _firstLevelScene.Instantiate();
                // SceneManager.ChangeScene(firstLevel);
				GD.Print("clicked new game");
                _ = _CreateNewGame(1);
				break;
			case MainMenuItems.Level1:
                _ = _CreateNewGame(0);
				break;	
			case MainMenuItems.Level2:
                _ = _CreateNewGame(1);
				break;							
			default:
				var defaultLevel = _firstLevelScene.Instantiate();
				SceneManager.ChangeScene(defaultLevel);		
				break;	

		}
		
	}


	private async Task _CreateNewGame(int levelIndex){
		GD.PrintRich("[color=green] creating new game[/color]");
		var path = Files.SavesPath;
		var fileName = "new_game.json";
		var saveGame = new GameSave{
			LevelIndex = levelIndex,
			LevelName = Levels.LevelNames.PuzzleTutorial.ToString(),
			Turn = 0, 
			Environment = Path.Join(Files.LevelEnvironmentsPath, "level_2_environment.csv"), //THESE ARE ALL IN AllLevels class, I'm repeating this shit for no reason
			Pieces = Path.Join(Files.LevelPiecesPath, "level_2_pieces.csv"),
			Upcoming = Path.Join(Files.LevelUpcomingPath, "level_2_upcoming.csv"),
			UpcomingBg = Path.Join(Files.LevelUpcomingBgPath, "level_2_upcoming_bg.csv"),
			Player = _CreateNewPlayerCharacter(),
			OtherStatefulPieces = new List<object>(),


		};

		await Files.SaveJson(saveGame, path, fileName);
		//await Files.SaveJsonIfNoneExists(saveGame, path, fileName);

		await Files.SaveJson(
			new CurrentSaveGame{CurrentSave = "new_game.json"}, 
			Files.SavesPath,
			"current.json"
		);

		await Files.SaveJson(new AllLevels().Levels, Files.LevelsPath, "levels.json");

		var bp = 123;
		var bar = 3245;
		// await Files.CopyFileAsync("res://assets/content/levels/level_2_environment.csv", Path.Join(Files.LevelEnvironmentsPath, "level_2_environment.csv"));
		// await Files.CopyFileAsync("res://assets/content/levels/level_2_pieces.csv", Path.Join(Files.LevelPiecesPath, "level_2_pieces.csv"));
		// await Files.CopyFileAsync("res://assets/content/levels/level_2_upcoming_bg.csv", Path.Join(Files.LevelUpcomingBgPath, "level_2_upcoming_bg.csv"));
		// await Files.CopyFileAsync("res://assets/content/levels/level_2_upcoming.csv", Path.Join(Files.LevelUpcomingPath, "level_2_upcoming.csv"));
		//GD.Print("before copyFIle");
		//Files.CopyFileAsync("res://assets/content/levels/level_2_environment.csv", "user://content/levels/environments/level_2_environment.csv");
		//Files.CopyFileAsync("res://assets/content/levels/level_2_pieces.csv", "user://content/levels/pieces/level_2_pieces.csv");
		//Files.CopyFileAsync("res://assets/content/levels/level_2_upcoming_bg.csv", "user://content/levels/upcomingBg/level_2_upcoming_bg.csv");
		//Files.CopyFileAsync("res://assets/content/levels/level_2_upcoming.csv", "user://content/levels/upcoming/level_2_upcoming.csv");
		//GD.Print("AFTER copyFIle");

		// Files.CopyFile("res://level_2_environment.csv"/* "res://assets/content/levels/level_2_environment.csv" */, "user://content/levels/environment/level_2_environment.csv");
		// Files.CopyFile("res://level_2_pieces.csv"/* "res://assets/content/levels/level_2_pieces.csv" */, "user://content/levels/pieces/level_2_pieces.csv");
		// Files.CopyFile("res://level_2_upcoming_bg.csv"/* "res://assets/content/levels/level_2_upcoming_bg.csv" */, "user://content/levels/upcomingBg/level_2_upcoming_bg.csv");
		// Files.CopyFile("res://level_2_upcoming.csv"/* "res://assets/content/levels/level_2_upcoming.csv" */, "user://content/levels/upcoming/level_2_upcoming.csv");


		var firstLevel = _firstLevelScene.Instantiate();
		SceneManager.ChangeScene(firstLevel);		
	}


	private PlayerSave _CreateNewPlayerCharacter(){
		return new PlayerSave{
			Stats = new ActorStats{
				Attributes = new Attributes{
					Strength = 10,
					Agility = 7,
					Constitution = 10, 
					Intelligence = 5
				},
				Health = PlayerDerivedStats.GetMaxHealth(10) + 999,
				Energy = PlayerDerivedStats.GetMaxEnergy(5) - 3, //test
				Energies = new Energies{
					MaxFireEnergy = PlayerEnergy.CalculateInitialMaxEnergy(10),	
					FireEnergy = 8,
					MaxWindEnergy = PlayerEnergy.CalculateInitialMaxEnergy(7),	
					WindEnergy = 5,
					MaxEarthEnergy = PlayerEnergy.CalculateInitialMaxEnergy(10),	
					EarthEnergy = 9,
					MaxWaterEnergy = PlayerEnergy.CalculateInitialMaxEnergy(5),	
					WaterEnergy = 4,
				},
				Speed = PlayerDerivedStats.GetSpeed(7),
				Defense = 2
			},
			SkillGroups = [//new Dictionary<string, SkillGroup>(){
				//{
					//SkillNames.SkillGroups.Melee.ToString(), 
					new SkillGroup{
						Group = SkillNames.SkillGroups.Melee.ToString(),
						Skills = [
							new SkillWithCount{
								Name=SkillNames.Melee.Bullrush.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{ //skill properties should not be defined here, this should just be where I keep track of what skills I have
									Fire=5,
									Wind=1,
									Earth=1,
									Water=0
								}
							},
							new SkillWithCount{
								Name=SkillNames.Melee.LeapAttack.ToString(),
								Uses=99,
								Level=2,
								EnergyRequirement = new EnergyRequirement{
									Fire=6,
									Wind=3,
									Earth=0,
									Water=0
								}
							},
							new SkillWithCount{
								Name=SkillNames.Melee.Whirlwind.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=5,
									Wind=4,
									Earth=0,
									Water=0
								}
							},
							new SkillWithCount{
								Name=SkillNames.Melee.Kick.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=3,
									Wind=0,
									Earth=3,
									Water=0
								}
							},
							new SkillWithCount{
								Name=SkillNames.Melee.DelayedSmash.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=8,
									Wind=0,
									Earth=5,
									Water=0
								}
							}					
						]
					},					
				//},
				//{
					//SkillNames.SkillGroups.Ranged.ToString(), 
					new SkillGroup{
						Group = SkillNames.SkillGroups.Ranged.ToString(),
						Skills = [
							new SkillWithCount{
								Name=SkillNames.Ranged.ThrowWeapon.ToString(),
								Uses=99,
								Level=3,
								EnergyRequirement = new EnergyRequirement{
									Fire=2,
									Wind=4,
									Earth=0,
									Water=0
								}
							},
							new SkillWithCount{
								Name=SkillNames.Ranged.ThrowPebble.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=0,
									Wind=4,
									Earth=0,
									Water=4
								}
							},
							new SkillWithCount{
								Name=SkillNames.Ranged.Sprint.ToString(),
								Uses=99,
								Level=3,
								EnergyRequirement = new EnergyRequirement{
									Fire=0,
									Wind=5,
									Earth=2,
									Water=0
								}
							}						
						]
					},					
				//},
				//{
					//SkillNames.SkillGroups.Defensive.ToString(),
					new SkillGroup{
						Group = SkillNames.SkillGroups.Defensive.ToString(),
						Skills = [
							new SkillWithCount{
								Name=SkillNames.Defensive.ShieldBash.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=3,
									Wind=0,
									Earth=5,
									Water=0
								}
							},
							new SkillWithCount{
								Name=SkillNames.Defensive.Walk.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=0,
									Wind=2,
									Earth=3,
									Water=0
								}
							}						
						]
					},
				//},
				//{
					//SkillNames.SkillGroups.Tech.ToString(),
					new SkillGroup{
						Group = SkillNames.SkillGroups.Tech.ToString(),
						Skills = [
							// new SkillWithCount{
							// 	Name=SkillNames.Tech.Ensnare.ToString(),
							// 	Uses=99,
							// 	Level=2,
							// 	EnergyRequirement = new EnergyRequirement{
							// 		Fire=1,
							// 		Wind=2,
							// 		Earth=0,
							// 		Water=4
							// 	}
							// },
							new SkillWithCount{
								Name=SkillNames.Tech.SearchOpening.ToString(),
								Uses=99,
								Level=1,
								EnergyRequirement = new EnergyRequirement{
									Fire=1,
									Wind=2,
									Earth=0,
									Water=4
								}
							}						
						]
					}
				//}	
			],			
			//},
	
			Class = Classes.Fighter.ToString(),
			Equipment = new Gear{
				Head = Helmets.RustyHelmet.ToString(),
				Torso = Armors.QuiltedArmor.ToString(),
				Weapon = Weapons.WoodenClub.ToString(),
				OffHand = OffHands.WoodenShield.ToString()
			}
		};
	}
}


