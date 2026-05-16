using Board;
using Common;
using Godot;
using Skills;
using Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;
public partial class ShieldBash : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, FilterableSkill
{
    public Control TileRoot { get; set; }
    public Node Board { private get; set; }
    public AnimationTree AnimationTree { private get; set; }

	[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);



    public async Task ProcessPathAsync(List<Vector2I> path){
		//matches pieces at the direct back of the player, who then kicks forward if enemy RIGHT in front, on a line with player and matches eg. m,m,m,p,f
		//received path direction is away from player as always, need to reverse 

		path.Reverse(); //this is not mindful of the player position, it's just relative to the direction tiles are generally matched

        var nextCellAtEnd = Hex.FindNextInLine(path);
		var playerCell = (Board as Queriable).GetCellFor(TileRoot);
		if(nextCellAtEnd.Equals(playerCell)){
			path.Add(playerCell);
			nextCellAtEnd = Hex.FindNextInLine(path);
			var nextPiece = (Board as Queriable).GetItemAt(nextCellAtEnd);
			GD.Print($"NEXT CELL AT END:   {nextCellAtEnd}");
			if(nextPiece is Disposition actor && actor.IsEnemy){
				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

				playback.Travel("Shove");

			
				path.Add(nextCellAtEnd);
				nextCellAtEnd = Hex.FindNextInLine(path);

				var strength = (TileRoot as Attributive).Strength;	
				var constitution = (TileRoot as Attributive).Constitution;

				/* await  */(actor as Bashable).BeBashed(strength, constitution);	
			}
		}

    }

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

    public /* static */ bool CheckIfUsable(List<Vector2I> matchedGroup, SkillNames.SkillGroups skillGroup, Queriable boardQuery){
		//var path = matchedGroup;
		//var path = matchedGroup.AsEnumerable().Reverse().ToList();
		var path = new List<Vector2I>(matchedGroup);
		path.Reverse();
		var playerCell = boardQuery.GetPlayerPosition();		
        var nextCellAtEnd = Hex.FindNextInLine(path);
		if(nextCellAtEnd.Equals(playerCell)){
			path.Add(playerCell);
			nextCellAtEnd = Hex.FindNextInLine(path);
			var nextPiece = boardQuery.GetItemAt(nextCellAtEnd);
			if(nextPiece is Disposition actor && actor.IsEnemy){
				return true;
			}
		}
		return false; 
    }		
}
