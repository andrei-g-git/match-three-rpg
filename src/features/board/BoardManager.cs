using Board;
using Godot;
using Godot.Collections;



public partial class BoardManager : PanelContainer
{
	[Export] private TileMapLayer _tileMap;
	[Export] private Node _model;

	public override void _Ready(){
		var env = Files.LoadCsv("D:\\projects\\match3\\mapping\\New folder\\9_07_0.csv");
		var environmentCellStructure = Hex.StringGridToEnums(env);
		var tileNames = Files.LoadCsv("D:\\projects\\match3\\mapping\\New folder\\9_07_1.csv");
		var tileTypes = Hex.StringGridToEnums(tileNames);		

		_PopulateMap(
			_tileMap,
			env,
			_MakeTileCoordDict()
		);		

		(_model as Organizable).Initialize(tileTypes);


		var bp = 123;
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

}
