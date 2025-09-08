using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Common;
using Godot;
using Stats;
using Tiles;
using static Skills.SkillNames;

namespace Archer{
	public partial class Manager : Control, Tile, /* AccessableBoard, */ Movable, Mapable, Permeable, RelayableUIEvents, Defensible, WithHealth, WithDefense, WithDamage, WithSpeed, Disposition
	{
		[ExportGroup("behaviors")]
        [Export] private Node _defender;
        [Export] private Node _hostility;

        [ExportGroup("stats")]
        [Export] private Node _stats;

		[ExportGroup("tweeners")]
		[Export] private Node _moveTweener;
        [Export] private Node _popTweener;
        [Export] private Node _recoil;
		public TileTypes Type => TileTypes.Archer;
        public TileTypes AA => Type; //for debugging
        public Tileable Map { set => (_moveTweener as Mapable).Map = value; }
        public int Health {get => (_stats as WithHealth).Health; set => (_stats as WithHealth).Health = value;}
        public int MaxHealth {get => (_stats as WithHealth).MaxHealth;}
        public int Defense {get => (_stats as WithDefense).Defense; set => (_stats as WithDefense).Defense = value;}
        public int Damage {get => (_stats as WithDamage).Damage; set => (_stats as WithDamage).Damage = value;}
        public int Speed {get => (_stats as WithSpeed).Speed; set => (_stats as WithSpeed).Speed = value;}
        public RemoteSignaling UIEventBus{private get; set;} //NO INTERFACE FOR THIS YET
        public bool IsAggressive { get => (_hostility as Disposition).IsAggressive; set => (_hostility as Disposition).IsAggressive = value; }
        public bool IsEnemy { get => (_hostility as Disposition).IsAggressive; set => (_hostility as Disposition).IsAggressive = value; }		

        public override void _Ready(){
            (_popTweener as Creatable).Pop();

            //_defender.Connect("TookDamage", _recoil, nameof(TestCurry));
            (_defender as Defender).ConnectTookDamage(TestCurry);
        }

        public void TestCurry(int unimportantValue){
            (_recoil as Recoiling).Recoil();
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

        public void SwapWith(Control tile)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage){
            (_defender as Defensible).TakeDamage(damage); 
        }   
    }	
}





