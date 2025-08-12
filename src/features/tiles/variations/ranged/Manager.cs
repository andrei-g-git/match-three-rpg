using Board;
using Common;
using Godot;
using Tiles;

namespace Ranged{
	public partial class Manager : Control, Tile, AccessableBoard, Movable, Mapable, Collapsable, Matchable
	{
		[ExportGroup("behaviors")]
		[Export] private Node _swapping;
		[Export] private Node _matching;
		
		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
		public TileTypes Type => TileTypes.Ranged;
        public TileTypes AA => Type; //for debugging
		public Node Board {set {(_swapping as AccessableBoard).Board = value;}}
        public Tileable Map { set => (_moveTweener as Mapable).Map = value; }

        public void MoveTo(Vector2I target){
            (_moveTweener as Movable).MoveTo(target);
        }


        public void BeginPostMatchProcessDependingOnPlayerPosition(Vector2I ownPosition, Node playerTile, bool playerAjacent){
            (_matching as Matchable).BeginPostMatchProcessDependingOnPlayerPosition(ownPosition, playerTile, playerAjacent);
        }

	}		
}
