using Board;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class MatchesTraversal : Node, TraversableMatching, AccessableBoard
{
	[Export] private Node _tileRoot;

    public Node Board {get; set;}


    public /* void */ async Task ReceivePathAndSkill(List<Vector2I> path, Skill skill){
		(_tileRoot as Skillful).Skill = skill as Node;
		//(skill as Traversing).ProcessPath(path);
		await (skill as Traversing).ProcessPath(path, true);
    }
}
