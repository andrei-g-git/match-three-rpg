using System.Collections.Generic;
using Godot;

namespace Levels;

[GlobalClass]
public partial class GridResource: Resource 
{
    public List<List<string>> Grid{get;set;}
}


public class LevelContent
{
    public List<List<string>> Environment {get;set;}
    public List<List<string>> Pieces {get;set;}
    public List<List<string>> UpcomingBg {get;set;}
    public List<List<string>> Upcoming {get;set;}
}

public class EachLevel
{
    public static List<LevelContent> Levels = new(){
        new LevelContent(){
            
        },

        new LevelContent(){
            Environment = [
                ["wall_down", "wall_up", "wall_down", "wall_up_last", "grass"],
                ["grass", "grass", "water_2", "water_3", "grass"],
                ["grass", "grass", "water_6", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass"],
            ],
            Pieces = [
                ["solid_prop", "solid_prop", "solid_prop", "solid_prop", "solid_prop"],
                ["fighter", "defensive", "hole", "hole", "fighter"],
                ["melee", "ranged", "hole", "defensive", "tech"],
                ["melee", "defensive", "ranged", "tech", "melee"],
                ["ranged", "defensive", "ranged", "melee", "melee"],
                ["melee", "tech", "tech", "defensive", "defensive"],
                ["defensive", "player", "melee", "ranged", "ranged"],
                ["tech", "melee", "melee", "ranged", "melee"],
            ],            
            UpcomingBg = [
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],       
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"]                   
            ],
            Upcoming = [
                ["-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1"],
                ["-1", "tech", "-1", "-1", "-1"],
                ["-1", "melee", "-1", "-1", "-1"],
                ["-1", "ranged", "-1", "-1", "-1"],
                ["-1", "tech", "melee", "-1", "-1"],
                ["-1", "defensive", "tech", "-1", "-1"],
                ["-1", "ranged", "melee", "-1", "tech"],
                ["-1", "defensive", "tech", "melee", "defensive"],
            ]              
        },

        new LevelContent(){
            Environment = [
                ["boulder", "boulder", "grass", "grass", "grass", "grass", "grass"],
                ["boulder", "boulder", "grass", "grass", "grass", "grass", "grass"],
                ["boulder", "grass", "grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "water_2", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass", "grass", "grass"],
                ["grass", "grass", "grass", "grass", "grass", "tree_upper", "brush"],
                ["grass", "grass", "grass", "grass", "grass", "tree_lower", "tree_upper"],
                ["grass", "grass", "grass", "grass", "grass", "brush", "tree_lower"],         
            ],
            Pieces = [
                ["solid_prop", "solid_prop", "defensive", "melee", "tech", "fighter", "ranged"],
                ["solid_prop", "solid_prop", "tech", "ranged", "defensive", "ranged", "defensive"],
                ["solid_prop", "defensive", "fighter", "tech", "melee", "defensive", "tech"],
                ["ranged", "melee", "ranged", "hole", "tech", "ranged", "melee"],
                ["tech", "ranged", "defensive", "defensive", "defensive", "melee", "ranged"],
                ["melee", "tech", "melee", "tech", "ranged", "defensive", "melee"],
                ["melee", "ranged", "defensive", "tech", "melee", "solid_prop", "solid_prop"],
                ["tech", "melee", "defensive", "ranged", "ranged", "-1", "solid_prop"],
                ["melee", "ranged", "player", "ranged", "melee", "solid_prop", "solid_prop"]          
            ],            
            UpcomingBg = [
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"],
                ["transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1", "transparent_1"]        
            ],
            Upcoming = [
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"],
                ["-1", "-1", "-1", "-1", "-1", "-1", "-1"]        
            ]           
        }
    };




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