using Board;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;

public partial class MatchesTraversal : Node, TraversableMatching, AccessableBoard
{
	[Export] private Node _tileRoot;

    public Node Board {get; set;}


    public /* void */ async Task ReceivePathAndSkill(List<Vector2I> path, Skill skill){
		(_tileRoot as Skillful).Skill = skill as Node;
		//(skill as Traversing).ProcessPath(path);
		await (skill as Traversing).ProcessPath(path, true); //this awaits the whole path traveling since the method is recursive, which is good I suppose
		(Board as Organizable).RelocateTile(_tileRoot as Control, path.Last()); //tileOrganizer.TransferTileTo places the player all over the path, I only need him at the end
    }
}
