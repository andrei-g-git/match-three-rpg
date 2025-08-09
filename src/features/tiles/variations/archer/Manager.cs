/* using Godot;
using System;
using Tiles;

namespace Archer{
	public partial class Manager : Control, Tile, Managerial, Actor, Movable.IAnimator, Mapable 
	{
		[Export] private Control dragController;
		[Export] private Node moveAnimator;
        private TileTypes type = TileTypes.Archer;
        public TileTypes Type => type;
        private Vector2I coordinates;
        public Vector2I Coordinates { get => coordinates; set => coordinates = value; }		
		private Node boardModel;
		public Node BoardModel {set => boardModel = value;}
		private Sequential turnQueue;
        public Sequential TurnQueue { set => turnQueue = value; }
        public Tileable Map{set => (moveAnimator as Mapable).Map = value;}
        
		public override void _Ready(){

		}

        public void MoveTo(Vector2I target){
            (moveAnimator as Movable.IAnimator).MoveTo(target);
        }
    }	
}

 */