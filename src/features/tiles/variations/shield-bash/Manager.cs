// using Godot;
// using Tiles;

// namespace ShieldBash{
// 	public partial class Manager : Control, Tile, Movable.IAnimator, Collapsable, Mapable
// 	{
// 		[Export] private Control dragController;
// 		[Export] private Node moveAnimator;
//         public TileTypes Type => TileTypes.ShieldBash;
//         private Vector2I coordinates;
//         public Vector2I Coordinates { get => coordinates; set => coordinates = value; }		
// 		private Node boardModel;
// 		public Node BoardModel {set => boardModel = value;}
//         public Sequential TurnQueue { private get; set; }
//         public Tileable Map{set => (moveAnimator as Mapable).Map = value;}
//         public override void _Ready(){

// 		}

//         public void MoveTo(Vector2I target){
//             (moveAnimator as Movable.IAnimator).MoveTo(target);
//         }
//     }	
// }