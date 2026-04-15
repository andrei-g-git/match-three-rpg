using Content;
using Godot;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public partial class SkillModel : Node, WithFireEnergy, WithWindEnergy, WithEarthEnergy, WithWaterEnergy
{
	[Export] 
	private PackedScene _elementSkillsDisplay;
	[Export]
	private Control _skillGroupsDisplay;

	public SkillGroup[] SkillGroups = [];

	public int MaxFireEnergy{get;set;}
	private int _FireEnergy;
	public int FireEnergy{
		get => _FireEnergy;
		set { _FireEnergy = Math.Clamp(value, 0, MaxFireEnergy); }
	}
	public int MaxWindEnergy{get;set;}
	private int _WindEnergy;
	public int WindEnergy{
		get => _WindEnergy;
		set { _WindEnergy = Math.Clamp(value, 0, MaxWindEnergy); }
	}
	public int MaxEarthEnergy{get;set;}
	private int _EarthEnergy;
	public int EarthEnergy{
		get => _EarthEnergy;
		set { _EarthEnergy = Math.Clamp(value, 0, MaxEarthEnergy); }
	}
	public int MaxWaterEnergy{get;set;}
	private int _WaterEnergy;
	public int WaterEnergy{
		get => _WaterEnergy;
		set { _WaterEnergy = Math.Clamp(value, 0, MaxWaterEnergy); }
	}

	private List<ElementSkillsDisplay> _ElementSkillsViews = []; //not interface but it just needs this to update the views

	private GameSave _loadedGame;
	
	public override void _Ready(){
		_InitializeSkills();
	}

	// public void RegisterElementSkills(ElementSkillsDisplay view){
	// 	_ElementSkillsViews.Add(view);

	// 	view.Update()
	// }

	private void _InitializeSkills(){
		Files.LoadJson<CurrentSaveGame>(Files.SavesPath, "current.json")
			.ContinueWith(task => {
				var currentGame = task.Result;
				var currentGameName = currentGame.CurrentSave;
				Files.LoadJson<GameSave>(Files.SavesPath, currentGameName)
					.ContinueWith(t => {

						_loadedGame = t.Result; 

						//skills also need base energy consumption

						SkillGroups = _loadedGame.Player.SkillGroups; //converting the Uses skill property to Level, will change properly later


				});
			});	


		//MMMMHHHHHHHHHHHHHHHHH
		//await Task.Delay(1000);	
		Thread.Sleep(1000);

		//Uses actually = Level now...

		foreach(var element in SkillGroups){
			if(element.Skills.Length > 0){
				var elementSkillsNode = _elementSkillsDisplay.Instantiate();
				(elementSkillsNode as SelectableSkills).UpdateSkills(element.Skills);

				//assume it's empty ... although it does have, uh, this model node so if I clear it later on there's gonna be issues...

				(elementSkillsNode as ElementSkillsDisplay).TestParent = _skillGroupsDisplay;

				_skillGroupsDisplay.AddChild(elementSkillsNode);	
				GD.Print("awefawef", element.Skills.Length);
				//mmmmhhhhhh....
				//_skillGroupsDisplay.CallDeferred("add_child", elementSkillsNode);							
			}
		}
	}
}
