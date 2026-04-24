using Animations;
using Board;
using Common;
using Godot;
using Skills;
using System;
using System.Threading.Tasks;
using Tiles;

public partial class SkillSlot : Control, Skillful, AccessableBoard, DelayableSkill
{
	[Export] Control _tileRoot;
	[Export] Node _animatedActor;
	private Node _skill;
	public Node Skill{
		private get => _skill; 
		set{
			_skill = value;
			if(value is Node node){
				foreach(Node oldNode in GetChildren()){
					oldNode.QueueFree();
				}
				InitializeSkill(node);				
				AddChild(node);
			}
		}
	}
	public Node Board{get; set;}


	public void ActivateDelayedSkillSynchronously(){
		_ = ActivateDelayedSkill();
	}
	public async Task ActivateDelayedSkill(){
		if(_skill is DelayableSkill delayableSkill){
			await delayableSkill.ActivateDelayedSkill();

			Skill = null; /////////////////////
		}
	}


	private void InitializeSkill(Node skill){
		(skill as WithTileRoot).TileRoot = _tileRoot;
		(skill as AccessableBoard).Board = Board;
		(skill as WithAnimationTree).AnimationTree = (_animatedActor as AnimatedActor).AnimationTree; //animatedActor is not an interface
		if(skill is WithAnimatedActor){ //they should all just have this and use it's interfaces to leverage the animatiotree
			(skill as WithAnimatedActor).AnimatedActor = _animatedActor as Node2D;
		}
		
	}
}
