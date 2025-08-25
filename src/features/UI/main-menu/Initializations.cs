using Godot;
using Levels;
using System;
using System.Threading.Tasks;

public partial class Initializations : Node
{

	public override void _Ready(){
		Files.CreateFolder(Files.LevelEnvironmentsPath);
		Files.CreateFolder(Files.LevelPiecesPath);
		Files.CreateFolder(Files.SavesPath);
		Files.CreateFolder(Files.ManualSavesPath);

		_ = _CreateLevelsData();
	}

	private async Task _CreateLevelsData(){
		await Files.SaveJsonIfNoneExists(AllLevels.Levels, Files.LevelsPath, "levels.json");
	}
}
