using System;
using Godot;
using Godot.Collections;
using Tiles;

namespace Board{
	public partial class TileFactory: Node, TileMaking
	{
		[Export] private Array<PackedScene> packedTileScenes;

		private Dictionary<TileTypes, PackedScene> _tilesWithScenes;

        public override void _Ready(){
			_Initialize(); //don't like this...
        }

		private void _Initialize(){
			_tilesWithScenes = _AssociateTileScenesWithTheirNames(packedTileScenes);
		}

		private Dictionary<TileTypes, PackedScene> _AssociateTileScenesWithTheirNames(Array<PackedScene> packedScenes){
			var dict = new Dictionary<TileTypes, PackedScene>();
			foreach (PackedScene scene in packedScenes){
				var instance = scene.Instantiate();
				TileTypes tileType;
				Enum.TryParse((string) instance.Name, out tileType);
				//dict.Add(((string) instance.Name).ToLower(), scene);
				dict.Add(tileType, scene);
			}
			return dict;
		}	

		public Node Create(TileTypes type){
			var packedScene = _tilesWithScenes[type];
			var tile = packedScene.Instantiate();
			//_InitializeTile(tile, type);
			return tile;
		}	




		// private void _InitializeTile(Node tile, TileTypes type){
		// 	switch(type){
		// 		case TileTypes.Fighter:
		// 			(tile as Mapable).Map = environment as Tileable;
		// 			(tile as Actor).TurnQueue = (boardModel as IBoard.Model).TurnQueue;
		// 			break;
		// 		case TileTypes.Walk:
		// 			(tile as AccessableTileContainer).TileContainer = tileContainer;
		// 			(tile as Mapable).Map = environment as Tileable;
		// 			break;
		// 		case TileTypes.Player:
		// 			(tile as Actor).TurnQueue = (boardModel as IBoard.Model).TurnQueue;
		// 			(tile as Playable).InputBlocker = inputBlocker as FlickableInput;
		// 			(tile as Mapable).Map = environment as Tileable;
		// 			(tile as RelayableStatus).EnergyDisplay = energyBar;
		// 			break;
		// 		case TileTypes.Melee:
		// 			(tile as Mapable).Map = environment as Tileable;
		// 			break;
		// 		case TileTypes.Ranged:
		// 			(tile as Mapable).Map = environment as Tileable;
		// 			break;					
		// 		default:
		// 			//dunno
		// 			var b = 2 + 2;
		// 			break;
		// 	}
		// }
	}

}