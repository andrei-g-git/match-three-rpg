using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Common;
using Godot;
using Skills;
using Tiles;
using static Skills.SkillNames;

namespace Ranged{
	public partial class Manager : Control, Tile, AccessableBoard, Movable, Mapable, Collapsable, Matchable, Swappable, SkillBased, Removable, Creatable
	{
		[ExportGroup("behaviors")]
		[Export] private Node _swapping;
		[Export] private Node _matching;
        [Export] private Node _removing;
		
		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
        [Export] private Node _popTweener;
		public TileTypes Type => TileTypes.Ranged;
        public TileTypes AA => Type; //for debugging
        public SkillGroups SkillGroup{get; private set;} = SkillGroups.Ranged;
		public Node Board {set {(_swapping as AccessableBoard).Board = value;}}
        public Tileable Map { set => (_moveTweener as Mapable).Map = value; }

        [Signal] public delegate void RemovedEventHandler();


        public override void _Ready(){
            (_popTweener as Creatable).Pop();
        }


        public void MoveTo(Vector2I target){
            (_moveTweener as Movable).MoveTo(target);
        }

        public async Task WaitUntilMoved(){
            await (_moveTweener as Movable).WaitUntilMoved();
        }


		public void MoveOnPath(Stack<Vector2I> path){
			(_moveTweener as Movable).MoveOnPath(path);
		}


        public void BeginPostMatchProcessDependingOnPlayerPosition(Vector2I ownPosition, Node playerTile, bool playerAjacent){
            (_matching as Matchable).BeginPostMatchProcessDependingOnPlayerPosition(ownPosition, playerTile, playerAjacent);
        }

        public /* void */async Task SwapWith(Control tile)
        {
            throw new System.NotImplementedException();
        }

        public void OnRemoved(){
            EmitSignal(SignalName.Removed); //should queueFree here, not in the remove behavior
        }

        public async Task WaitForRemoved(){ //not in
            //await ToSignal(this, SignalName.Removed);
            await (_removing as Removable).WaitForRemoved();
            var pb = 123;
        }

        public void PrepDestroy() //great...
        {
            throw new System.NotImplementedException();
        }

        public void Remove()
        {
            throw new System.NotImplementedException();
        }  

        public void Pop() {
            (_popTweener as Creatable).Pop();
        }

        public async Task WaitUntilCreated(){
            await (_popTweener as Creatable).WaitUntilCreated();
        }
    }		
}
