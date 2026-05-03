using Board;
using Common;
using Godot;
using Skills;
using Stats;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;
using static Skills.SkillNames;

public partial class SearchOpening : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, WithAnimatedActor//, FilterableSkill
{
    public Control TileRoot { get; set; }
    public Node Board { private get; set; }
    public AnimationTree AnimationTree { private get; set; }
    public Node2D AnimatedActor { private get; set; }



    public async Task ProcessPathAsync(List<Vector2I> path){
		var boardQuery = Board as Queriable;
        var enemiesAdjacentToMatches = boardQuery.GetPiecesAroundLineOfType<Disposition>(path);
		enemiesAdjacentToMatches.Remove(/* (Disposition) */ TileRoot);
		if(enemiesAdjacentToMatches.Count > 0){
			var thisCell = boardQuery.GetCellFor(TileRoot);
			var targetPiece = boardQuery.GetClosestPieceToCellInList(thisCell, enemiesAdjacentToMatches);	

			var allActors = boardQuery.GetAllActors();
			allActors.Remove(TileRoot);
			foreach(var actor in allActors){
				if(actor is WithEffects effectable)
				effectable.RemoveAllOfType<FocusedDefenseDebuff>();
			}	
			(targetPiece as WithEffects).Add(new FocusedDefenseDebuff(999, 2));
		}


    }

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }


}
