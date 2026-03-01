using Animations;
using Board;
using Common;
using Godot;
using Inventory;
using Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiles;

public partial class ThrowPilum : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, WithAnimatedActor
{
	[Export] private Node _damageCalculator;
	
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
			(_damageCalculator as WithTileRoot).TileRoot = value;
	}}
	private Node _board;
    public Node Board {get; set;}

	private AnimationTree _animationTree; //?
    public AnimationTree AnimationTree {get; set;}	
	public Node2D AnimatedActor{private get;set;}
	[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);


	public async Task ProcessPathAsync(List<Vector2I> path){
		var nextCellAtEnd = Hex.FindNextInLine(path);
		if(nextCellAtEnd.X >= 0 && nextCellAtEnd.Y >= 0){
			var tileAhead = (Board as Queriable).GetItemAt(nextCellAtEnd);
			if (tileAhead is Disposition actor && actor.IsEnemy){
				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

				var currentWeapon = (TileRoot as Gearable).Weapon;

				(AnimatedActor as CustomizableGear).ChangeGear(EquipmentTypes.Weapon.ToString(), "some pilum thing, gotta make first");

				playback.Travel("Throw");

				await _WaitForStateToExitAsync("Throw");	

				(AnimatedActor as CustomizableGear).ChangeGear(EquipmentTypes.Weapon.ToString(), currentWeapon);			
			}		
		}
	}


    private async Task _WaitForStateToExitAsync(string stateName){
        // Polling loop - yields a frame each iteration to avoid blocking
        while (true){
            // The playback.CurrentNodeName getter is not exposed in some bindings,
            // so read the current state through the AnimationTree parameter path.
            // This reads the active state name from the state machine.
            var current = (string) AnimationTree.Get("parameters/playback/current_node");

            if (current != stateName)
                break;

            // yield for one frame
            await ToSignal(GetTree(), "idle_frame");
        }
    }


    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

}
