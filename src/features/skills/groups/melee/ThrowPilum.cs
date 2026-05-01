using Animations;
using Board;
using Common;
using Godot;
using Inventory;
using Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Tiles;

public partial class ThrowPilum : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, WithAnimatedActor//, Mapable
{
	[Export] private Node _damageCalculator;
	[Export] private /* Node2D */ Control _projectileTweener;
	
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
	//public Tileable Map{private get; set;}
	[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);


	public async Task ProcessPathAsync(List<Vector2I> path){
		var nextCellAtEnd = Hex.FindNextInLine(path);
		if(nextCellAtEnd.X >= 0 && nextCellAtEnd.Y >= 0){
			var tileAhead = (Board as Queriable).GetItemAt(nextCellAtEnd);
			if (tileAhead is Disposition actor && actor.IsEnemy){
				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

				var currentWeapon = (TileRoot as Gearable).Weapon;

				(AnimatedActor as CustomizableGear).ChangeGear(EquipmentTypes.Weapon.ToString(), Weapons.Pilum.ToString());

				playback.Travel("Throw");

				// var direction = (tileAhead.Position - TileRoot.Position).Normalized();
				// var angle = direction.Angle(); //don't need this, I can just motion tween

// var sw = new Stopwatch();
// sw.Start();
				await _WaitForStateToExitAsync("Throw", playback);	
// sw.Stop();
// GD.Print($"Throw animation took     {sw.Elapsed} seconds");

				(_projectileTweener as Mapable).Map = (Board as BoardModel).GetMap(); //not interface method

				(AnimatedActor as CustomizableGear).ChangeGear(EquipmentTypes.Weapon.ToString(), currentWeapon);	
							
				await (_projectileTweener as ProjectileTween).FlyTo(nextCellAtEnd, path.Count); //not innterface method

				EmitSignal(SignalName.Attacking, tileAhead, path.Count);


			}		
		}
	}


    private async Task _WaitForStateToExitAsync(string stateName, AnimationNodeStateMachinePlayback playback){
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame); //2x smells like bad practice...
        // Polling loop - yields a frame each iteration to avoid blocking
        while (true){
            // The playback.CurrentNodeName getter is not exposed in some bindings,
            // so read the current state through the AnimationTree parameter path.
            // This reads the active state name from the state machine.

			var current = playback.GetCurrentNode();

            if (current != stateName)
                break;

            // yield for one frame
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);		
        }
    }


    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }

}
