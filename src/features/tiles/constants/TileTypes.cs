using System.Linq;
using Godot.Collections;

namespace Tiles {
	public enum TileTypes
	{
		Archer,

		Barrel,
		Blank,
		Boulder, //env
		Brush, //env
		Chair, //now the initials matrix won't show unique tiles
		Charge,
		Chest,
		Cart,
		Defensive,
		Energy,
		EmptySolid,
		Ensnare,
		Fighter,
		Grass, //Env
		Health,
		JavelinThrow,
		LeapAttack,
		Melee,
		MeleeBuff,
		Player,
		Ranged,//
		RangedBuff,
		ShieldBash,
		Shock,
		Stamina,
		Table,
		Tech,
		TreeLower, //env
		TreeUpper, //env
		Unlock,
		Walk,
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
			//{"empty_solid", TileTypes.EmptySolid},
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
			{ "0", TileTypes.EmptySolid},{"1", TileTypes.EmptySolid},{"2", TileTypes.EmptySolid},{"3", TileTypes.EmptySolid},{"4", TileTypes.EmptySolid},{"5", TileTypes.EmptySolid},{"6", TileTypes.EmptySolid},{"7", TileTypes.EmptySolid},{"8", TileTypes.EmptySolid},{"9", TileTypes.EmptySolid},{"10", TileTypes.EmptySolid},{"11", TileTypes.EmptySolid},{"12", TileTypes.EmptySolid},{"13", TileTypes.EmptySolid},{"14", TileTypes.EmptySolid},{"15", TileTypes.EmptySolid},
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