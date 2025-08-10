// using System;
// using System.Threading.Tasks;
using Godot;
using Tiles;
// using Skills;
// using Tiles;
namespace Ranged{
	public partial class Manager : Control, Tile//, Managerial, Movable.IAnimator, Mapable, Collapsable, Transfering.IAnimator, SkillTile
{
// 		[Export] private Control dragAndDrop;	
//         [Export] private SkillNames.SkillGroups skillGroup;

//         [ExportGroup("Tweeners")]
//         [Export] private Node2D fadeTweener;        	
// 		[Export] private Node/* 2D */ moveTweener;	

// 		[ExportGroup("Behaviors")]
// 		[Export] private Node swap;			
// 		[Export] private Node collapse;	
// 		[Export] private Node transfer;
// 		[Export] private Node damageBuffer;
// 		[Export] private Node selfRemover;

// 		private TileTypes type = TileTypes.Ranged;
        public TileTypes Type => TileTypes.Ranged;
//         private Vector2I coordinates;
//         public Vector2I Coordinates { get => coordinates; set => coordinates = value; }		
// 		private Node boardModel;
// 		public Node BoardModel {set => boardModel = value;}
// 		//private Tileable map;
//         public Tileable Map { 
// 			set {
// 				(moveTweener as Mapable).Map = value;
// 				(damageBuffer as Mapable).Map = value;				
// 			} 
// 		}		

//         public SkillNames.SkillGroups SkillGroup => skillGroup;

//         public override void _Ready(){
// 			(swap as Swapping).SwapTiles = (boardModel as Organizable).Swap2;
// 			(swap as Swapping).TryMatching = (boardModel as Matching).TryMatching;			
// 			//(dragAndDrop as DraggableDroppable).ConnectDroppedTile((swap as Swapping).SwapTo);
// 			(dragAndDrop as DraggableDroppable).ConnectEngagedBy((swap as Swapping).SwapTo);



// 			// (collapse as Collapsable.IBehavior).ConnectCollapsedByActor((Control actor, Vector2I cell) => (damageBuffer as BuffableDamage.IBehavior).IncreaseDamage(actor));
// 			// (collapse as Collapsable.IBehavior).ConnectCollapsedByActor((transfer as Transfering.IBehavior).TransferAttacker);		
// 			// (collapse as Collapsable.IBehavior).ConnectMerging((damageBuffer as BuffableDamage.IBehavior).BecomeConsumableBuff);
// 			// (collapse as Collapsable.IBehavior).ConnectDestroyed((selfRemover as Deletable).Remove);



// 			(damageBuffer as BuffableDamage.IBehavior).CreateConsumableBuff = (boardModel as Board.Model).CreateBuff;	

//             (transfer as Transfering.IBehavior).TransferTile = (boardModel as Organizable).TransferTileOverTo;            					
// 		}

//         public void MoveTo(Vector2I target){
//             (moveTweener as Movable.IAnimator).MoveTo(target);
//         }

//         public void ConnectFinishedMoving(Action action)
//         {
//             throw new NotImplementedException();
//         }
//         // public void CollapseByActor(Control actor, Vector2I cell){
//         //     (collapse as Collapsable.IBehavior).CollapseByActor(actor, cell);
//         // }

//         // public void CollapseAndMerge(int matchingTiles, bool lastInSequence){
//         //     (collapse as Collapsable.IBehavior).CollapseAndMerge(matchingTiles, lastInSequence);
//         // }
//         // public void CollapseSelf(){
//         //     (collapse as Collapsable.IBehavior).CollapseSelf();
//         // }
//         // public void ConnectCollapsedByActor(Action<Control, Vector2I> action)
//         // {
//         //     throw new NotImplementedException();
//         // }

//         public void ConnectMerging(Action<int> action)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectDestroyed(Action action)
//         {
//             throw new NotImplementedException();
//         }

//         public void FinishUp()
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

//         public void FadeOut(){
//             (fadeTweener as Transfering.IAnimator).FadeOut();
//         }

//         public void ConnectTileRemoved(Action action)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectFinished(Action action)
//         {
//             throw new NotImplementedException();
//         }
        
//         public async Task WaitUntilFinished(){
//             await (collapse as Collapse).WaitUntilFinished(); //Collapse is not an interface
//         }
    }	
}
