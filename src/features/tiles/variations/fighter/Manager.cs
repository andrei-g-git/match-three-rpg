using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Common;
using Godot;
using Stats;
using Tiles;
using static Skills.SkillNames;

namespace Fighter{
	public partial class Manager : Control, Tile, AccessableBoard, /* WithTiles, */  Movable, Mapable, Permeable, RelayableUIEvents, Defensible, WithHealth, WithDefense, WithDamage, WithSpeed, Disposition, Creatable, Agentive, TurnBased
	{
		[ExportGroup("behaviors")]
        [Export] private Node _defender;
        [Export] private Node _hostility;
        [Export] private Node _turn;
        [Export] private Node _sentinel;
        [Export] private Node _chase;
        [Export] private Node _pathfinding;
        [Export] private Node _swapping;
        [Export] private Node _engagement;

        [ExportGroup("stats")]
        [Export] private Node _stats;

		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
        [Export] private Node _popTweener;
        [Export] private Node _recoil;
        [Export] private Node _flashWhite;
		public TileTypes Type => TileTypes.Fighter;
        public TileTypes AA => Type; //for debugging
        public Tileable Map { set {
            (_moveTweener as Mapable).Map = value;
            (_sentinel as Mapable).Map = value;
            (_chase as Mapable).Map = value;
            (_pathfinding as Mapable).Map = value;
        }}
        public int Health {get => (_stats as WithHealth).Health; set => (_stats as WithHealth).Health = value;}
        public int MaxHealth {get => (_stats as WithHealth).MaxHealth;}
        public int Defense {get => (_stats as WithDefense).Defense; set => (_stats as WithDefense).Defense = value;}
        public int Damage {get => (_stats as WithDamage).Damage; set => (_stats as WithDamage).Damage = value;}
        public int Speed {get => (_stats as WithSpeed).Speed; set => (_stats as WithSpeed).Speed = value;}
        public RemoteSignaling UIEventBus{private get; set;} //NO INTERFACE FOR THIS YET
        public bool IsAggressive { get => (_hostility as Disposition).IsAggressive; set => (_hostility as Disposition).IsAggressive = value; }
        public bool IsEnemy { get => (_hostility as Disposition).IsAggressive; set => (_hostility as Disposition).IsAggressive = value; }		
        public Sequential TurnQueue{private get; set;}
        private Grid<Control> _tiles;
        // public Grid<Control> Tiles { //not using...
        //     get => _tiles; 
        //     set{
        //         (_sentinel as WithTiles).Tiles = value;
        // }}
		public Node Board {
            set {
                //(_sentinel as WithTiles).Tiles = (value as WithTiles).Tiles;
                (_sentinel as AccessableBoard).Board = value;
                (_chase as AccessableBoard).Board = value;
                (_pathfinding as AccessableBoard).Board = value;
                (_swapping as AccessableBoard).Board = value;
                (_engagement as AccessableBoard).Board = value;
        }}


        public override void _Ready(){
            (_popTweener as Creatable).Pop();

            //_defender.Connect("TookDamage", _recoil, nameof(TestCurry));
            (_defender as Defender).ConnectTookDamage(TestCurry);

            (_turn as Turn).ConnectRequestedTurnEnd(TurnQueue.AdvanceTurn);
        }

        public void TestCurry(int unimportantValue){
            (_recoil as Recoiling).Recoil();
            (_flashWhite as WhiteFlashable).FlashOnce();
        }



        public void MoveTo(Vector2I target){
            (_moveTweener as Movable).MoveTo(target);
        }


		public void MoveOnPath(Stack<Vector2I> path){
			(_moveTweener as Movable).MoveOnPath(path);
		}

        public async Task WaitUntilMoved(){
            await (_moveTweener as Movable).WaitUntilMoved();
        }

        public void/* async Task */ SwapWith(Control tile)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage){
            (_defender as Defensible).TakeDamage(damage); 
        }   

        public void Pop() {
            (_popTweener as Creatable).Pop();
        }

        public async Task WaitUntilCreated(){
            await (_popTweener as Creatable).WaitUntilCreated();
        }  

        public void AdvanceTurn(){ //not Sequential interface, from Agentive
            TurnQueue.AdvanceTurn();
        }

        public void EndTurn(){
            (_turn as TurnBased).EndTurn();
        }

        public void BeginTurn(){
            (_turn as TurnBased).BeginTurn();
        }
    }	
}










// using Godot;
// using Godot.Collections;
// using System;
// using Tiles;

// namespace Fighter{
// 	public partial class Manager : Control, Tile, Mapable, Managerial, Movable.IAnimator, Actor, NPC, TurnBased, Offensive.IModel, Enduring.IModel, Disposition/* those signal connection methods are killing me */ 
// 	{
// 		[Export] private Node model;
// 		[Export] private Node view;
// 		[Export] private Node dragAndDrop;
// 		[Export] private Control pathfinding;
// 		[Export] private Node/* 2D */ moveAnimator;
// 		[Export] private Node2D attackAnimator;
// 		[Export] private Node2D defendAnimator;		
// 		[Export] private AnimatedSprite2D sprite;        
//         [ExportGroup("Behaviors")]
// 		[Export] private Node offense;
// 		[Export] private Node endure;		
// 		[Export] private Node sentinel;
// 		[Export] private Node chase;
// 		[Export] private Node swap;
// 		[Export] private Node turn;
// 		[Export] private Node ai;
// 		[Export] private Node engagement;
// 		[Export] private Node hostility;
//         [Export] private Node haul;

//         [ExportGroup("Animation States")]
//         [Export] private Node idleState;
//         [Export] private Node moveState;
//         [Export] private Node attackState;
//         [Export] private Node hurtState;		
// 		//private /* TileMapLayer */ Tileable map;
//         private TileTypes type = TileTypes.Fighter;
//         public TileTypes Type => type;
//         private Vector2I coordinates;
//         public Vector2I Coordinates { get => coordinates; set => coordinates = value; }		
// 		public /* TileMapLayer */ Tileable Map{
// 			set {
// 				(pathfinding as IPathfinding).Map = value;
// 				(moveAnimator as Mapable).Map = value;
// 				(sentinel as Vigilance).Map = value;
// 				(chase as Mapable).Map = value;
// 			}
// 		}
//         public bool IsAggressive { get => (hostility as Disposition).IsAggressive; set => (hostility as Disposition).IsAggressive = value; }
//         public bool IsEnemy { get => (hostility as Disposition).IsAggressive; set => (hostility as Disposition).IsAggressive = value; }		
// 		public int Health => (endure as Enduring.IModel).Health;
//         public int MaxHealth => (endure as Enduring.IModel).MaxHealth;
//         public int Defense => (endure as Enduring.IModel).Defense;
// 		private Node boardModel;
// 		public Node BoardModel {
//             set {
//                 boardModel = value;
//                 //(chase as Chase).Tiles = (value as IBoard.Model).Tiles; //the boards does not have tiles at this point since it's setting from within the factory as it produces tiles...
//                 (swap as Swapping).TryMatching = (value as Matching).TryMatching; 
//             }
//         }
// 		private Sequential turnQueue;
//         public Sequential TurnQueue { set => turnQueue = value; }

//         public int Damage => (offense as Offensive.IModel).Damage; //I'll probably never need this

//         public override void _ExitTree()
//         {
//             var bp = 123;
//         }


//         public override void _Ready(){
// 			//(pathfinding as IPathfinding).Map = map;

// 			//(view as Draggable.IView).OnGetDragData = (dragController as Draggable.IController).OnGetDragData;

// 			(sentinel as Vigilance).Tiles = (boardModel as IBoard.Model).Tiles;  //here it's too late, can't test the behavior on it's ready method
// 			(engagement as Engagement).Tiles = (boardModel as IBoard.Model).Tiles; //Engagement is not an interface!
// 			(pathfinding as IPathfinding).Tiles = (boardModel as IBoard.Model).Tiles;
// 			//(sentinel as Vigilance).ConnectFoundPlayer((chase as Pursuing).ChaseActor);
//             (chase as Chase).Tiles = (boardModel as IBoard.Model).Tiles; //<----???
// 			//(chase as Pursuing).ConnectTrySwapping((swap as Swapping).SwapTo); //<--------------- this could match but algo won't run
// 			(swap as Swapping).SwapTiles = (boardModel as Organizable).Swap2;
// 			//(moveAnimator as Movable.IAnimator).ConnectFinishedMoving((turn as TurnBased).EndTurn);
// 			(turn as TurnBased).ConnectRequestedTurnEnd(turnQueue.NextTurn);
// 			//(turn as TurnBased).ConnectRequestedTurnStart((ai as AI).Resume);
// 			//(ai as AI).ConnectStartedSearching((sentinel as Vigilance).StandWatch);

// 			(dragAndDrop as DraggableDroppable).ConnectEngagedBy((engagement as Engagement).SustainAttack); //Engagement is not an interface!


// 			(chase as Chase).ConnectCaughtTarget((engagement as Engagement).AttackCell);	

// 			//Endure and Offense are not interfaces!
//             //(endure as Endure).ConnectTookDamage((int damage) => (hurtState as TileState).Enter());
//             //(offense as Offense).ConnectAttacked((Vector2 target) => (attackState as TileState).Enter());

//             // (sprite as AnimatableActor).ConnectAnimationFinished((hurtState as TileState).Exit);
//             // (sprite as AnimatableActor).ConnectAnimationFinished((attackState as TileState).Exit);
//             // (sprite as AnimatableActor).ConnectAnimationFinished((moveState as TileState).Exit);		

// 			var bp = 1123;
// 		}

//         public void MoveTo(Vector2I target){
//             (moveAnimator as Movable.IAnimator).MoveTo(target);
// 			(moveState as TileState).Enter();
//         }


//         public void ConnectFinishedMoving(Action action) //rats...
//         {
//             throw new NotImplementedException();
//         }

//         public void EndTurn(){
//             (turn as TurnBased).EndTurn();
//         }

//         public void BeginTurn(){
//             (turn as TurnBased).BeginTurn();
//         }

//         public void ConnectRequestedTurnEnd(Action action)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectRequestedTurnStart(Action action)
//         {
//             throw new NotImplementedException();
//         }

//         public void Attack(Control target){
//             (offense as Offensive.IModel).Attack(target);
//         }

//         public void TakeDamage(int damage){
//             (endure as Enduring.IModel).TakeDamage(damage);
//         }

//         public void ReceiveHealing(int amount){
//             (endure as Enduring.IModel).ReceiveHealing(amount);
//         }

//         public void ReceiveHealingFrom(Node tile){
//             (endure as Enduring.IModel).ReceiveHealingFrom(tile);
//         }

//         public void BuffMeleeDamage(int amount){
//             (offense as Offensive.IModel).BuffMeleeDamage(amount);
//         }

//         public void BuffRangedDamage(int amount){
//             (offense as Offensive.IModel).BuffRangedDamage(amount);
//         }

//         public void DashTo(Vector2I target)
//         {
//             throw new NotImplementedException();
//         }

//         public void ConnectFinishedDashing(Action<Vector2I> action)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }

