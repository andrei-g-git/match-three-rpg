using Common;
using Godot;
using Inventory;
using Skills;
using System;
using System.Collections.Generic;
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
				var firstLevel = _firstLevelScene.Instantiate();
				SceneManager.ChangeScene(firstLevel);
				break;
			default:
				var defaultLevel = _firstLevelScene.Instantiate();
				SceneManager.ChangeScene(defaultLevel);		
				break;	

		}
		
	}


	private void _CreateNewGame(){
		//I don't need to save an initial newGame for this prototype, the character isn't customizable so I can just start the same game every time
		var saveGame = new {
			LevelIndex = 0,
			LevelName = "Tutorial"
		};
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
