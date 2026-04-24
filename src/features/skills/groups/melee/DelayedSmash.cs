using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class DelayedSmash : Control, Skill, DelayableSkill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree
{
    public Control TileRoot { get; set; }
    public Node Board { private get; set; }
    public AnimationTree AnimationTree { private get; set; }

	private int _builtUpMagnitude;

	[Export] Node _delete;

	[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);

	public async Task ProcessPathAsync(List<Vector2I> path){
		//run wind up animation
		//store covereddistance
	}

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

    public async Task ActivateDelayedSkill(){
        //skill conclusion will be handled here
		//await conclusion

		await ToSignal(_delete, "timeout"); //delete
    }


}
