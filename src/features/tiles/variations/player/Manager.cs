using System.Collections.Generic;
using Board;
using Common;
using Godot;
using Tiles;

namespace Player{
	public partial class Manager : Control, Tile, AccessableBoard, Movable, Mapable, Swappable, Permeable
	{
		[ExportGroup("behaviors")]
		[Export] private Node _swapping;

		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
        [Export] private Node _popTweener;
		public TileTypes Type => TileTypes.Melee;
        public TileTypes AA => Type; //for debugging
		public Node Board {set {(_swapping as AccessableBoard).Board = value;}}
        public Tileable Map { set => (_moveTweener as Mapable).Map = value; }


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

    }	
}










// using Common;
// using Godot;
// using Godot.Collections;
// using Skill;
// using Skills;
// using Stats;
// using System;
// using Tiles;

// namespace Player{
// 	public partial class Manager : Control, Tile, Managerial, Actor, Playable, TurnBased, Movable.IAnimator, Dashing.IAnimator, Mapable, Disposition, Enduring.IModel, MatchableBounds.IBehavior, /* Offensive.IModel */Offensive.IBehavior, CombatItemry, TraversableMatching.IBehavior, AccessableBoard, Skillful.IBehavior, CollectableEnergy.IBehavior, RelayableStatus, Attributive
// 	{
// 		[Export] private Node model;
// 		//[Export] private Node view;
// 		[Export] private Node dragController;
// 		[Export] private Node turn;
// 		[Export] private Node/* 2D */ moveAnimator;
// 		[Export] private Node2D attackAnimator;
// 		[Export] private Node2D defendAnimator;
//         [Export] private AnimatedSprite2D sprite;
//         [Export] private Node2D radiusMargin;
//         [Export] private Control skillSlot;
//         [Export] private Node _attributes;
//         [Export] private Node _tileEventsBus;

//         [ExportGroup("Behaviors")]
//         [Export] private Node matchesTraversal;
//         [Export] private Node energyCollector;
// 		[Export] private Node offense;
// 		[Export] private Node endure;
// 		[Export] private Node hostility;
//         [Export] private Node matchingRange;        
//         //[Export] private Node haul;

//         [ExportGroup("Animation States")]
//         [Export] private Node stateMachine;
//         [Export] private Node idleState;
//         [Export] private Node moveState;
//         [Export] private Node attackState;
//         [Export] private Node hurtState;
//         [Export] private Node dashState;
// 		private TileTypes type = TileTypes.Player;
//         public TileTypes Type => type;
//         private Vector2I coordinates;
//         public Vector2I Coordinates { get => coordinates; set => coordinates = value; }
// 		private Node boardModel;
// 		public Node BoardModel {
//             set {
//                 boardModel = value;
//                 (matchesTraversal as AccessableBoard).Board = value; //don't need
//                 (skillSlot as AccessableBoard).Board = value;
//         }}		
//         public Node Board{
//             get=> boardModel; 
//             set  {
//                 boardModel = value;
//                 (matchesTraversal as AccessableBoard).Board = value; //don't need
//                 (skillSlot as AccessableBoard).Board = value;
//         }}
//         public Node Skill{ set {(skillSlot as Skillful.IBehavior).Skill = value;}}

// 		private Sequential turnQueue;
//         public Sequential TurnQueue { set => turnQueue = value; }	
// 		private FlickableInput inputBlocker;	
// 		public FlickableInput InputBlocker{set => inputBlocker = value;}
// 		public Tileable Map{set => (moveAnimator as Mapable).Map = value;}
//         public bool IsAggressive { get => (hostility as Disposition).IsAggressive; set => (hostility as Disposition).IsAggressive = value; }
//         public bool IsEnemy { get => (hostility as Disposition).IsAggressive; set => (hostility as Disposition).IsAggressive = value; }

//         public int Health => (endure as Enduring.IModel).Health;
//         public int MaxHealth => (endure as Enduring.IModel).MaxHealth;
//         public int Defense => (endure as Enduring.IModel).Defense;
//         public int MatchRange => (matchingRange as MatchableBounds.IBehavior).MatchRange;

//         public int Damage => (offense as Offensive.IModel).Damage;
//         public Control HealthDisplay{private get; set;}
//         public Control EnergyDisplay{private get; set;}
//         public override void _Ready(){

// 			//(moveAnimator as Movable.IAnimator).ConnectFinishedMoving((turn as TurnBased).EndTurn);	<---do this in the editor!



// 			(turn as TurnBased).ConnectRequestedTurnEnd(turnQueue.NextTurn);
// 			(turn as TurnBased).ConnectRequestedTurnStart(inputBlocker.AllowInput); 


//             (endure as Endure).ConnectTookDamage((int damage) => (hurtState as TileState).Enter());

//             //(offense as Offense).ConnectAttacked((Vector2 target) => (attackState as TileState).Enter());

// 			//Endure and Offense are not interfaces!
//             // (sprite as AnimatableActor).ConnectAnimationFinished((hurtState as TileState).Exit);
//             // (sprite as AnimatableActor).ConnectAnimationFinished((attackState as TileState).Exit);
//             // (sprite as AnimatableActor).ConnectAnimationFinished((moveState as TileState).Exit);
//             // (sprite as AnimatableActor).ConnectAnimationFinished((dashState as TileState).Exit);

//             (radiusMargin as Radius).Board = (boardModel as IBoard.Model).Tiles; //Radius is not an interface

// 			//(moveAnimator as Movable.IAnimator).ConnectFinishedDashing((haul as Haulable.IBehavior).AssessSurroundings); //this may clash with exiting state further up...
//             ////////////////////////////////////////(haul as Haulable.IBehavior).GetNeighbors = (boardModel as IBoard.Model).GetNeighboringTiles;

//             //(haul as Haulable.IBehavior).ConnectReachedEnemy((Control enemy) => (attackState as TileState).Enter());  don't need this, Attakced signal already connected to enter state
//             (energyCollector as EnergyCollector).ConnectEnergyChanged((EnergyDisplay as ProgressableBar).Update);   //EnergyCollector is not an interface! 

//             (_tileEventsBus as RemoteSignaling).Subscribe(AnimateAttacking, Events.Attacking);
            
// 		}

//         public int GetRightHandDamage(){
//             return 4; //will access equipment component and get the damage from it's equipped right hand item
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

//         public void MoveTo(Vector2I target){
//             (moveAnimator as Movable.IAnimator).MoveTo(target);
//             (moveState as TileState).Enter();
//         }
//         public void DashTo(Vector2I target){
//             (moveAnimator as /* Movable */Dashing.IAnimator).DashTo(target);
//             (dashState as TileState).Enter();
//         }

//         public void DashToNext(Array<Vector2I> path, int index, Action<Array<Vector2I>> ProcessPath){
//             (moveAnimator as Dashing.IAnimator).DashToNext(path, index, ProcessPath/* well this looks like utter crap... */);
//         }
// public void DashOnPath(Array<Vector2I> path){
//      throw new NotImplementedException();
// }
//         public void ConnectFinishedMoving(Action action)
//         {
//             throw new NotImplementedException();
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

//         public void FillEnergy(int magnitude, SkillNames.SkillGroups skillGroup){
//             (energyCollector as CollectableEnergy.IBehavior).FillEnergy(magnitude, skillGroup);
//         }
//         public void SpendEnergy(int amount){
//             (energyCollector as CollectableEnergy.IBehavior).SpendEnergy(amount);
//         }
        
//         public bool IsMatchGroupInRange(Array<Vector2I> matchGroup, Array<Array<Control>> board){
//             return (matchingRange as MatchableBounds.IBehavior).IsMatchGroupInRange(matchGroup, board);
//         }
//         public void BuffMeleeDamage(int amount){
//             (offense as Offensive.IModel).BuffMeleeDamage(amount);
//         }

//         public void BuffRangedDamage(int amount){
//             (offense as Offensive.IModel).BuffRangedDamage(amount);
//         }
//         public void Attack(/* Control */ Node target, int momentum){
//             //(offense as Offensive.IModel).Attack(target);
//             (offense as Offensive.IBehavior).Attack(target, momentum);
//         }
//         public void AnimateAttacking(){
//             (attackState as TileState).Enter();
//         }

//         public void ReceivePathAndTiles(Array<Vector2I> path, Control firstTile){
//             (matchesTraversal as TraversableMatching.IBehavior).ReceivePathAndTiles(path, firstTile);
//         }
//         public void ReceivePathAndSkill(Array<Vector2I> path, Skill.IBehavior skill){
//             (matchesTraversal as TraversableMatching.IBehavior).ReceivePathAndSkill(path, skill);
//         }

//         public void ConnectFinishedDashing(Action<Vector2I> action)
//         {
//             throw new NotImplementedException();
//         }

//         public int Strength {get => (_attributes as Attributive).Strength;}
//         public int Agility {get => (_attributes as Attributive).Agility;}
//         public int Constitution {get => (_attributes as Attributive).Constitution;}
//         public int Intelligence {get => (_attributes as Attributive).Intelligence;}

//         public void IncreaseStrength(int amount) => (_attributes as Attributive).IncreaseStrength(amount);
//         public void IncreaseAgility(int amount) => (_attributes as Attributive).IncreaseAgility(amount);
//         public void IncreaseConstitution(int amount) => (_attributes as Attributive).IncreaseConstitution(amount);
//         public void IncreaseIntelligence(int amount) => (_attributes as Attributive).IncreaseIntelligence(amount);

//         public void SubtractStrength(int amount) => (_attributes as Attributive).SubtractStrength(amount);
//         public void SubtractAgility(int amount) => (_attributes as Attributive).SubtractAgility(amount);
//         public void SubtractConstitution(int amount) => (_attributes as Attributive).SubtractConstitution(amount);
//         public void SubtractIntelligence(int amount) => (_attributes as Attributive).SubtractIntelligence(amount);	
//     }	
// }

