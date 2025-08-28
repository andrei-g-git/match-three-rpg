using System.Collections.Generic;
using static Skills.SkillNames;

namespace Skills;

// public interface Skillful{
//     public List<CountableSkill> Skills{get;set;}
// }

public interface CountableSkill{
    public /* SkillNames.All */ string Name{get;set;}
    public int Uses{get;set;}
}

public interface SkillBased{
    public SkillGroups SkillGroup{get;}
}