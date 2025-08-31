using Common;
using Content;
using Godot;
using Inventory;
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
                _ = _CreateNewGame();
				break;
			default:
				var defaultLevel = _firstLevelScene.Instantiate();
				SceneManager.ChangeScene(defaultLevel);		
				break;	

		}
		
	}


	private async Task _CreateNewGame(){
		var path = Files.SavesPath;
		var fileName = "new_game.json";
		var saveGame = new GameSave{
			LevelIndex = 0,
			LevelName = Levels.LevelNames.Tutorial.ToString(),
			Turn = 0, 
			Environment = Path.Join(Files.LevelEnvironmentsPath, "level_1_environment.csv"),
			Pieces = Path.Join(Files.LevelPiecesPath, "level_1_pieces.csv"),
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
				Health = PlayerDerivedStats.GetMaxHealth(10),
				Energy = PlayerDerivedStats.GetMaxEnergy(5) - 3 //test
			},
			SkillGroups = [//new Dictionary<string, SkillGroup>(){
				//{
					//SkillNames.SkillGroups.Melee.ToString(), 
					new SkillGroup{
						Group = SkillNames.SkillGroups.Melee.ToString(),
						Skills = [
							new SkillWithCount{
								Name=SkillNames.Melee.Charge.ToString(),
								Uses=99
							},
							new SkillWithCount{
								Name=SkillNames.Melee.LeapAttack.ToString(),
								Uses=99
							},
							new SkillWithCount{
								Name=SkillNames.Melee.Whirlwind.ToString(),
								Uses=99
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
								Uses=99
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
								Uses=99
							}						
						]
					},
				//},
				//{
					//SkillNames.SkillGroups.Tech.ToString(),
					new SkillGroup{
						Group = SkillNames.SkillGroups.Tech.ToString(),
						Skills = [
							new SkillWithCount{
								Name=SkillNames.Tech.Ensnare.ToString(),
								Uses=99
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


