using Board;
using Common;
using Content;
using Godot;
using Godot.Collections;
using Skills;
using Stats;
using System;


//using System.Collections.Generic;


using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tiles;


public partial class BoardManager : PanelContainer
{
	[Export] private TileMapLayer _tileMap;
	[Export] private Node _model;
	[Export] private Node _tileFactory;
	[Export] private Node _playerSkillsModel;

	private Grid<TileTypes> _tileTypes;
	private GameSave _loadedGame;

	private static readonly string _userPath = ProjectSettings.GlobalizePath("user://"); //delete


	public override void _Ready(){
		InitializeLevel();

	}


	public void InitializeLevel(){

						// //delete
						// var delete = System.IO.Path.Join(_userPath, Files.SavesPath, "new_game.json");
						// CreateLogMessagePopup(delete);		

						// var delete2 = System.IO.Path.Join(_userPath, Files.SavesPath, "current.json");
						// CreateLogMessagePopup(delete2);	
		Files.LoadJson<CurrentSaveGame>(Files.SavesPath, "current.json")
			.ContinueWith(task => {
				var currentGame = task.Result;
				var currentGameName = currentGame.CurrentSave;
				Files.LoadJson<GameSave>(Files.SavesPath, currentGameName)
					.ContinueWith(t => {

						_loadedGame = t.Result; 

						var environmentPath = _loadedGame.Environment;
						var tilesPath = _loadedGame.Pieces;

						var env = Files.LoadCsv(environmentPath);
						var environmentCellStructure = Hex.StringGridToEnums(env);
						var tileNames = Files.LoadCsv(tilesPath);
						_tileTypes = Hex.StringGridToEnums(tileNames);		

						_PopulateMap(
							_tileMap,
							env,
							_MakeTileCoordDict()
						);		

						//(_model as Organizable).Initialize(tileTypes);
						var bp = 123;							
					});
			
			});

		//the above is async, while adding children to dones inside the SceneTree is main-thread-only.
		//I need to fix this mess.
		GetTree().CreateTimer(1).Timeout += () => {
			(_model as Organizable).Initialize(_tileTypes);

			var player = (_model as WithTiles).Tiles.FindItemByType(typeof(Playable));
			var loadedAttributes = _loadedGame.Player.Stats.Attributes; //well this sucks on multiple levels
			var attributes = new Attributes{
				Strength = loadedAttributes.Strength,
				Agility = loadedAttributes.Agility,
				Constitution = loadedAttributes.Constitution,
				Intelligence = loadedAttributes.Intelligence
			};
			var stats = _loadedGame.Player.Stats; //this really, REALLY sucks

			var derivableStats = player as DerivableStats;
			derivableStats.Attributes = attributes;	
			derivableStats.Health = stats.Health;
			derivableStats.Energy = stats.Energy;

			Classes playerClass;
			Enum.TryParse(_loadedGame.Player.Class, out playerClass);
			(player as Classy).Class = playerClass;

			(player as Player.Manager).InitializeHud();


			var skillGroups = _loadedGame.Player.SkillGroups; 
			var skillsModel =  _playerSkillsModel as ManageableSkills;
			(player as Player.Manager).SkillsModel = skillsModel;

			skillsModel.Melee = skillGroups.Where(group => (group as GroupableSkills).Group == SkillNames.SkillGroups.Melee.ToString()).ElementAt(0);
			skillsModel.Ranged = skillGroups.Where(group => (group as GroupableSkills).Group == SkillNames.SkillGroups.Ranged.ToString()).ElementAt(0);
			skillsModel.Defensive = skillGroups.Where(group => (group as GroupableSkills).Group == SkillNames.SkillGroups.Defensive.ToString()).ElementAt(0);
			skillsModel.Tech = skillGroups.Where(group => (group as GroupableSkills).Group == SkillNames.SkillGroups.Tech.ToString()).ElementAt(0);

			(skillsModel as Modelable).Notify();

			// var griddd = (_model as WithTiles).Tiles.GetGridAs2DList();
			// CreateLogMessagePopup(skillsModel.Melee.Group);	
		};
	}

	
	private async Task<string> _LoadCurrentGameName(){
		var currentGame = await Files.LoadJson<CurrentSaveGame>(Files.SavesPath, "current.json") as CurrentlySavable;
		return currentGame.CurrentSave;		
	}


	//this is a bit too manager-y for a simple manager, I should have a separate model for the environment tile map, etc and do this stuff there...
	private Dictionary<string, Vector2I> _MakeTileCoordDict(){
		var dict = new Dictionary<string, Vector2I>();
		var nameLayerId = _tileMap.TileSet.GetCustomDataLayerByName("name");

		var sourceId = _tileMap.TileSet.GetSourceId(0);
		var source = _tileMap.TileSet.GetSource(sourceId) as TileSetAtlasSource;

		for (int i = 0; i < source.GetTilesCount(); i++){
			var tileId = source.GetTileId(i); //tileid is actually an atlas coord ...
			var altId = 0;
			var tileData = source.GetTileData(tileId, altId);
			var name = (string)tileData.GetCustomDataByLayerId(nameLayerId);
			dict[name] = tileId;
		}
		return dict;
	}


	private void _PopulateMap(
		TileMapLayer tileMap, 
		Grid<string> cellTable, 
		Dictionary<string, Vector2I> tileAtlasCoordinateDict
	){
		for (int x = 0; x < cellTable.Width; x++){
			for (int y = 0; y < cellTable.Height; y++){
				var cellType = cellTable.GetItem(y, x); //only the csv table needs reversing
				if (cellType != "-1"){
					var atlasCell = tileAtlasCoordinateDict[cellType];
					//GD.Print(atlasCell);
					tileMap.SetCell(
						new Vector2I(x, y),
						tileMap.TileSet.GetSourceId(0),
						atlasCell
					);
				}
			}
		}
	}

	private void _TestSaveFile(string path, string fileName, string data){
		if(!Directory.Exists(path)){
			Directory.CreateDirectory(path);
		}

		var fullPath = Path.Join(path, fileName);
		GD.Print(fullPath);

		try{
			File.WriteAllText(fullPath, data);
		}catch(System.Exception exception){
			GD.Print(exception);
		}
	}

public async void CreateLogMessagePopup(string text){
    var dlg = new AcceptDialog();
    dlg.DialogText = text;
    AddChild(dlg);
    dlg.PopupCentered();

    await ToSignal(dlg, "popup_hide");
    dlg.QueueFree();
}


}
