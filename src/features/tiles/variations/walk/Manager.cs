using System.Collections.Generic;
using Board;
using Common;
using Godot;
using Skills;
using Tiles;
using static Skills.SkillNames;

namespace Walk{
	public partial class Manager : Control, Tile, AccessableBoard, Movable, Mapable, Collapsable, Swappable
	{
		[ExportGroup("behaviors")]
		[Export] private Node _swapping; 
        [Export] private Node _walkway;
		
		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
        [Export] private Node _popTweener;
		public TileTypes Type => TileTypes.Walk;
        public TileTypes AA => Type; //for debugging
        public SkillGroups SkillGroup{get; private set;} = SkillGroups.Defensive;
		public Node Board {
            set {
                (_swapping as AccessableBoard).Board = value;
                (_walkway as AccessableBoard).Board = value;
            }}
        public Tileable Map { set => (_moveTweener as Mapable).Map = value; }
        [Signal] public delegate void RemovedEventHandler();

        public override void _Ready(){
            (_popTweener as Creatable).Pop();
        }


        public void MoveTo(Vector2I target){
            (_moveTweener as Movable).MoveTo(target);
        }


		public void MoveOnPath(Stack<Vector2I> path){
			(_moveTweener as Movable).MoveOnPath(path);
		}


        public void SwapWith(Control tile)
        {
            throw new System.NotImplementedException();
        }

        public void OnRemoved(){ //NOT INTERFACE METHOD
            EmitSignal(SignalName.Removed);
        }
    }		
}






// using Godot;
// using Godot.Collections;
// using System;
// using System.Runtime.CompilerServices;
// using Tiles;

// namespace Walk{
// 	public partial class Manager : Control, Tile, Managerial, AccessableTileContainer, Transfering.IAnimator, Movable.IAnimator, Deletable, Swapable, Mapable, Collapsable
// 	{
// 		[Export] private Node transfer;
// 		[Export] private Control dragAndDrop;
// 		[Export] private Node fadeAnimator;
// 		[Export] private Node/* 2D */ moveAnimator;
//         private TileTypes type = TileTypes.Walk;
//         public TileTypes Type => type;
//         private Vector2I coordinates;
//         public Vector2I Coordinates { get => coordinates; set => coordinates = value; }
// 		private Node boardModel;
// 		public Node BoardModel {set => boardModel = value;}
// 		private Container tileContainer;
// 		public Container TileContainer {set => tileContainer = value;}
//         public Tileable Map { 
// 			set {
// 				(moveAnimator as Mapable).Map = value;
// 			} 
// 		}
//         public Array<Array<Control>> Tiles { set => throw new NotImplementedException(); } //I really need to get rid of IPathfinding, just make a Mappable or something...


//         public override void _Ready(){
// 			(transfer as Transfering.IBehavior).TransferTile = (boardModel as Organizable).TransferTileOverTo;

// 			(dragAndDrop as DraggableDroppable).ConnectEngagedBy((transfer as Transfering.IBehavior).TransferTileHere);

// 			(fadeAnimator as Transfering.IAnimator).ConnectTileRemoved(Remove);

// 			(fadeAnimator as Transfering.IAnimator).ConnectTileRemoved(() => (tileContainer as IBoard.View).UpdatePositions((boardModel as Board.Model).Tiles)); //Board.Model is not an interface, it's the implementation
// 		}

//         public void FadeOut(){
//             (fadeAnimator as Transfering.IAnimator).FadeOut();
//         }

// 		public void Remove(){ 
// 			QueueFree();
// 		}

//         public void ConnectTileRemoved(Action action)
//         {
//             //well this breaks interface segregation...
			
//         }

//         public void MoveTo(Vector2I target){
//             (moveAnimator as Movable.IAnimator).MoveTo(target);
//         }

//         public Array<Vector2I> FindPath(Vector2I target)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectFinishedMoving(Action action)
//         {
//             throw new NotImplementedException();
//         }

//         public void DashTo(Vector2I target)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectFinishedDashing(Action<Vector2I> action)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectRemoved(Action action)
//         {
//             throw new NotImplementedException();
//         }

//     }	
// }

