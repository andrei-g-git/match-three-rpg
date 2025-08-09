using System.Linq;
using Godot.Collections;

namespace Tiles {
	public enum TileTypes
	{
		Archer,/////////////////////////////

		Barrel,/////////////////////////////////////////
		BgShit, //delete obviously
		Blank,
		Boulder, //env
		Brush, //env
		Chair, //now the initials matrix won't show unique tiles
		Charge,
		Chest,////////////////////////////
		Cart,/////////////////////////////////
		Defensive,//////////////////////////////////
		Energy,////////////////////////////////////
		Ensnare,
		Fighter,/////////////////////////////////////
		Grass, //Env
		Health,////////////////////////////////////
		JavelinThrow,
		LeapAttack,
		Melee,//////////////////////////////////////
		MeleeBuff,//////////////////////////////////
		Player,/////////////////////////////////////
		Ranged,//////////////////////////////
		RangedBuff,///////////////////////////
		ShieldBash,
		Shock,
		Stamina,///////////////////////////
		Table,
		Tech,
		TreeLower, //env
		TreeUpper, //env
		Unlock,/////////////////////////////////////////
		Walk,////////////////////////////////
		Wall,
		Water, //env
	}

	public class TileDict
	{
		private static readonly Dictionary<string, TileTypes> dict = new()
		{
			{"archer", TileTypes.Archer},
			{"barrel", TileTypes.Barrel},
			{"chair", TileTypes.Chair},//now the initials matrix won't show unique tile Chair, //now the initials matrix won't show unique tiles
			{"charge", TileTypes.Charge},
			{"chest", TileTypes.Chest},
			{"cart", TileTypes.Cart},
			{"defensive", TileTypes.Defensive},
			{"energy", TileTypes.Energy},
			{"ensnare", TileTypes.Ensnare},
			{"fighter", TileTypes.Fighter},
			{"health", TileTypes.Health},
			{"javelin_throw", TileTypes.JavelinThrow},
			{"leap_attack", TileTypes.LeapAttack},
			{"melee_buff", TileTypes.MeleeBuff},
			{"melee", TileTypes.Melee},
			{"player", TileTypes.Player},
			{"ranged", TileTypes.Ranged},
			{"ranged_buff", TileTypes.RangedBuff},
			{"shield_bash", TileTypes.ShieldBash},
			{"shock", TileTypes.Shock},
			{"stamina", TileTypes.Stamina},
			{"table", TileTypes.Table},
			{"tech", TileTypes.Tech},
			{"unlock", TileTypes.Unlock},
			{"walk", TileTypes.Walk},
			{"wall", TileTypes.Wall },
			{"-1", TileTypes.Blank},
			{"boulder", TileTypes.Boulder}, 
			{"brush", TileTypes.Brush}, 
			{"grass", TileTypes.Grass}, 
			{"tree_lower", TileTypes.TreeLower}, 
			{"tree_upper", TileTypes.TreeUpper}, 		
			{"water", TileTypes.Water}, 							
			{ "0", TileTypes.BgShit},{"1", TileTypes.BgShit},{"2", TileTypes.BgShit},{"3", TileTypes.BgShit},{"4", TileTypes.BgShit},{"5", TileTypes.BgShit},{"6", TileTypes.BgShit},{"7", TileTypes.BgShit},{"8", TileTypes.BgShit},{"9", TileTypes.BgShit},{"10", TileTypes.BgShit},{"11", TileTypes.BgShit},{"112", TileTypes.BgShit},{"13", TileTypes.BgShit},{"14", TileTypes.BgShit},{"15", TileTypes.BgShit},
		};

		public static TileTypes GetEnum(string key)
		{
			return dict[key];
		}

		public static string GetKey(TileTypes value)
		{
			return dict.FirstOrDefault(item => item.Value == value).Key;
		}
	}	
}