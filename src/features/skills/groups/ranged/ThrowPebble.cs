using Animations;
using Board;
using Common;
using Godot;
using Inventory;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiles;

public partial class ThrowPebble : Control, Skill, WithTileRoot, AccessableBoard, Traversing, WithAnimationTree, WithAnimatedActor
{
	[Export] private int _noiseRadius = 2;
	[Export] private /* Node2D */ Control _projectileTweener;
	[Export] private AnimatedSprite2D _soundRipple;
	
	private Control _tileRoot;
	public Control TileRoot{
		get => _tileRoot; 
		set{
			_tileRoot = value;
	}}
	private Node _board;
    public Node Board {get; set;}

	private AnimationTree _animationTree; //?
    public AnimationTree AnimationTree {get; set;}	
	public Node2D AnimatedActor{private get;set;}

	//[Signal] public delegate void AttackingEventHandler(Control enemy, int coveredTileDistance);
	

    public void ProcessPath(List<Vector2I> path)
    {
        throw new NotImplementedException();
    }


    public async Task ProcessPathAsync(List<Vector2I> path){
		//var nextCellAtEnd = Hex.FindNextInLine(path);
		var targetCell = path.Last();
		if(targetCell.X >= 0 && targetCell.Y >= 0){
			//var tileAhead = (Board as Queriable).GetItemAt(targetCell);
			//if (tileAhead is Disposition actor && actor.IsEnemy){
				var playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

				var currentWeapon = (TileRoot as Gearable).Weapon;

				(AnimatedActor as CustomizableGear).ChangeGear(EquipmentTypes.Weapon.ToString(), Weapons.Pebble.ToString());

				playback.Travel("Throw");

				await _WaitForStateToExitAsync("Throw", playback);	


				(_projectileTweener as Mapable).Map = (Board as BoardModel).GetMap(); //not interface method

				(AnimatedActor as CustomizableGear).ChangeGear(EquipmentTypes.Weapon.ToString(), currentWeapon);	
							
				await (_projectileTweener as ProjectileTween).FlyTo(targetCell, path.Count); //not innterface method

				//EmitSignal(SignalName.Attacking, tileAhead, path.Count);
				//check all cells within the radius of where it lands for enemies
				//call enemy's stunned behavior or something...
					//it should have an effects collection each with durations. at turn start these effects are iterated and applied
						//come to think of it perhaps delayed attacks can behave like this too? Probably not, the way I do it now is too convenient

				_soundRipple.AnimationFinished += () =>{
					GD.Print("Checking all cells in radius around landing spot")	;

					var cellsInRadius = (Board as Queriable).GetCellsInRadius(2, path.Last());
					
				};
				_soundRipple.Play();
			//}		
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
}
