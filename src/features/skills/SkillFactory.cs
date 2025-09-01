using Godot;
using Godot.Collections;
using Skills;
using System;

public partial class SkillFactory : Node, SkillMaking//, Initializable
{
    [Export] private Array<PackedScene> _skillPackedScenes;
    //[Export] private TileMapLayer _environment;
    private Dictionary<SkillNames.All, PackedScene> _skillsWithScenes = [];

    // public void Initialize(){
    //     _skillsWithScenes = _AssociateSkillScenesWithTheirNames(_skillPackedScenes);
    // }    

    public Node Create(SkillNames.All skillName){
		_AssociateSkillScenesWithTheirNames(_skillPackedScenes);
        var packedScene = _skillsWithScenes[skillName];
        var skill = packedScene.Instantiate() as Node; //Control;
        InitializeSkill(skillName, skill);
        return skill;
    }

    private void InitializeSkill(SkillNames.All type, /* Control */Node skill){
        switch(type){
            case SkillNames.All.LeapAttack:
            case SkillNames.All.Charge:
                //(skill as Mapable).Map = _environment as Tileable;
                break;
        }
    }

    private Dictionary<SkillNames.All, PackedScene> _AssociateSkillScenesWithTheirNames(Array<PackedScene> packedScenes){
        var dict = new Dictionary<SkillNames.All, PackedScene>();
        foreach (PackedScene scene in packedScenes){
            var instance = scene.Instantiate();
            SkillNames.All skillName;
            Enum.TryParse((string) instance.Name, out skillName);
            dict.Add(skillName, scene);
        }
        return dict;
    }	    
}