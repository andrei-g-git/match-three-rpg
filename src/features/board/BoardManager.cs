using Board;
using Content;
using Godot;
using Godot.Collections;
//using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public partial class BoardManager : PanelContainer
{
	[Export] private TileMapLayer _tileMap;
	[Export] private Node _model;

	public override void _Ready(){
		var currentGame = Files.LoadJson<object>(Files.SavesPath, "current.json");// as CurrentlySavable;
		var currentGameName = "ererg";//currentGame.CurrentSave;
		var loadedGame = Files.LoadJson<object>(Files.SavesPath, currentGameName) as SavableGame;
		var environmentPath = loadedGame.Environment;
		var tilesPath = loadedGame.Pieces;

		var env = Files.LoadCsv(environmentPath);//"D:\\projects\\match3\\mapping\\New folder\\9_08_0.csv");
		var environmentCellStructure = Hex.StringGridToEnums(env);
		var tileNames = Files.LoadCsv(tilesPath);//"D:\\projects\\match3\\mapping\\New folder\\9_08_1.csv");
		var tileTypes = Hex.StringGridToEnums(tileNames);		

		_PopulateMap(
			_tileMap,
			env,
			_MakeTileCoordDict()
		);		

		(_model as Organizable).Initialize(tileTypes);


		var bp = 123;

		var zTestSave = new ZTest(tileNames.GetGridAs2DList());
		var json = JsonSerializer.Serialize(zTestSave);
		GD.Print(json);
		var path = ProjectSettings.GlobalizePath("user://");

		_TestSaveFile(path, "Save1.json", json);
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

}

public  class ZTest(System.Collections.Generic.List<System.Collections.Generic.List<string>> grid)
{
    public System.Collections.Generic.List<System.Collections.Generic.List<string>> Grid { get; set; } = grid;
    public string Name{get;set;} = "minimyny";
	public int Age{get;set;} = 69;
}
