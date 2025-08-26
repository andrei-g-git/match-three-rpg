using System.Collections.Generic;

namespace Skills;

// public interface Skillful{
//     public List<CountableSkill> Skills{get;set;}
// }

public interface CountableSkill{
    public /* SkillNames.All */ string Name{get;set;}
    public int Uses{get;set;}
}