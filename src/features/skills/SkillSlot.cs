using Board;
using Godot;
using Skills;
using System;
using Tiles;

public partial class SkillSlot : Control, Skillful, AccessableBoard
{
	[Export] Control _tileRoot;
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


	private void InitializeSkill(Node skill){
		(skill as WithTileRoot).TileRoot = _tileRoot;
		(skill as AccessableBoard).Board = Board;
	}
}
