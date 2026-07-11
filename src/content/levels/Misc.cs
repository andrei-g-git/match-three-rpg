using System.Collections.Generic;
using Godot;

namespace Levels;

[GlobalClass]
public partial class GridResource: Resource 
{
    public List<List<string>> Grid{get;set;}
}


public class EachLevel
{
    public static List<List<string>> level_2_environment => [
        ["boulder", "boulder", "grass", "grass", "grass", "grass", "grass"],
        ["boulder", "boulder", "grass", "grass", "grass", "grass", "grass"],
        ["boulder", "grass", "grass", "grass", "grass", "grass", "grass"],
        ["grass", "grass", "grass", "water_2", "grass", "grass", "grass"],
        ["grass", "grass", "grass", "grass", "grass", "grass", "grass"],
        ["grass", "grass", "grass", "grass", "grass", "grass", "grass"],
        ["grass", "grass", "grass", "grass", "grass", "tree_upper", "brush"],
        ["grass", "grass", "grass", "grass", "grass", "tree_lower", "tree_upper"],
        ["grass", "grass", "grass", "grass", "grass", "brush", "tree_lower"],         
    ];


    public static List<List<string>> level_2_pieces => [
        ["solid_prop", "solid_prop", "defensive", "melee", "tech", "fighter", "ranged"],
        ["solid_prop", "solid_prop", "tech", "ranged", "defensive", "ranged", "defensive"],
        ["solid_prop", "defensive", "fighter", "tech", "melee", "defensive", "tech"],
        ["ranged", "melee", "ranged", "hole", "tech", "ranged", "melee"],
        ["tech", "ranged", "defensive", "defensive", "defensive", "melee", "ranged"],
        ["melee", "tech", "melee", "tech", "ranged", "defensive", "melee"],
        ["melee", "ranged", "defensive", "tech", "melee", "solid_prop", "solid_prop"],
        ["tech", "melee", "defensive", "ranged", "ranged", "-1", "solid_prop"],
        ["melee", "ranged", "player", "ranged", "melee", "solid_prop", "solid_prop"]          
    ];


    public static List<List<string>> level_2_upcoming_bg => [
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
        ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"]        
    ];

    public static List<List<string>> level_2_upcoming => [
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
        ["-1", "-1", "-1", "-1", "-1", "-1", "-1"]        
    ];

}