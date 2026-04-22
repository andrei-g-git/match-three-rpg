using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;

public partial class Sprint : Control, Skill, WithTileRoot, AccessableBoard, WithAnimationTree, Traversing
{
    public Node Board { private get; set; }
    public Control TileRoot { get; set; }
    public AnimationTree AnimationTree { private get; set; }
	[Signal] public delegate void FinishedPathEventHandler();

    public async Task ProcessPathAsync(List<Vector2I> path){
		var playerCell = (Board as Queriable).GetCellFor(TileRoot);
		var isAdjacent = (Board as Queriable).IsCellAdjacentToLine(playerCell, path);
		if (isAdjacent){
			var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

			playback.Travel("Dash");

			await (Board as BoardModel).TransferTileToAsync(TileRoot, path.Last());

			EmitSignal(SignalName.FinishedPath);	

			(Board as Organizable).RelocateTile(TileRoot, path.Last());		
		}else{
			//gain energy
		}
	}

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

}
