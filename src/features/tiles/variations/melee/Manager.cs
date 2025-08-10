using Board;
using Common;
using Godot;
using Tiles;

namespace Melee{
	public partial class Manager : Control, Tile, AccessableBoard, Movable, Mapable
	{
		[ExportGroup("behaviors")]
		[Export] private Node _swapping;

		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
		public TileTypes Type => TileTypes.Melee;
		public Node Board {set {(_swapping as AccessableBoard).Board = value;}}
        public Tileable Map { set => (_moveTweener as Mapable).Map = value; }


        public void MoveTo(Vector2I target){
            (_moveTweener as Movable).MoveTo(target);
        }
    }	
}

