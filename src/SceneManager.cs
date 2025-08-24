using Common;
using Godot;
using System;

public partial class SceneManager : Control, ChangeableScenes
{
	[Export] private PackedScene _gameStartScene;
	public override void _Ready(){
		var startingScene = _gameStartScene.Instantiate();
		(startingScene as WithSceneManager).SceneManager = this;
		ChangeScene(startingScene);
	}

	public void ChangeScene(Node instantiatedScene){
		var children = GetChildren();
		foreach(var child in children){
			child.QueueFree();
		}
		AddChild(instantiatedScene);
	}
}
