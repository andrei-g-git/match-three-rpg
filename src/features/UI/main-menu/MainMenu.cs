using Common;
using Godot;
using Inventory;
using Skills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
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
		var saveGame = new {
			LevelIndex = 0,
			LevelName = Levels.LevelNames.Tutorial.ToString(),
			Turn = 0, 
			Environment = Path.Join(Files.LevelEnvironmentsPath, "level_1_pieces.csv"),
			Pieces = Path.Join(Files.LevelPiecesPath, "level_1_pieces.csv"),
			Player = new NewMainCharacter(),
			OtherStatefulPieces = new List<object>(),


		};

		//await Files.SaveJson(saveGame, path, fileName);
		await Files.SaveJsonIfNoneExists(saveGame, path, fileName);

		await Files.SaveJson(
			new{CurrentSave = "new_game.json"}, 
			Files.SavesPath,
			"current.json"
		);

		var firstLevel = _firstLevelScene.Instantiate();
		SceneManager.ChangeScene(firstLevel);		
	}
}

class NewMainCharacter{
	public object Stats{get;set;} = new{
		Attributes = new{
			Strength = 10,
			Agility = 7,
			Constitution = 10, 
			Intelligence = 5
		},
		DerivedStats = new{ //since they're derived, I'm goint to have to ... derive them ... so doing that here just so I can save it is awkward, I should just calculate them after loading
			CurrentHealth = 10 * 2,
		}
	};

	public List<object> Skills{get;set;} = [
		new{
			Name=SkillNames.Melee.Charge.ToString(),
			Uses=99
		},
		new{
			Name=SkillNames.Melee.LeapAttack.ToString(),
			Uses=99
		},
	];	

	public object Equipment{get;set;} = new{
		Head = Helmets.RustyHelmet.ToString(),
		Torso = Armors.QuiltedArmor.ToString(),
		Weapon = Weapons.WoodenClub.ToString(),
		OffHand = OffHands.WoodenShield.ToString()
	};
}
