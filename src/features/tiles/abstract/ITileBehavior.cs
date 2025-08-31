using System.Collections.Generic;
using Godot;
using static Skills.SkillNames;

namespace Tiles{
    public interface Swappable{
        public void SwapWith(Control tile);
    }    

    public interface Removable{
        public void PrepDestroy();
        public void Remove();
    }

    public interface Matchable{
        public void BeginPostMatchProcessDependingOnPlayerPosition(Vector2I ownPosition, Node playerTile, bool playerAjacent);
    }

    public interface MatchableBounds{
        public int MatchRange{get;}
        public bool IsMatchGroupInRange(Queue<List<Vector2I>> matchGroupQueue, Grid<Control> board); 
    }

    public interface CollectableEnergy{
        public void FillEnergy(int magnitude, SkillGroups skillGroup);
        public void SpendEnergy(int amount);
    }

    public interface ReactiveToMatches{
        public void ReactToMatchesBySkillType(List<Vector2I> matches, SkillGroups skillGroup);
    }

    public interface Walkable{
        public void LeadPlayer(Control tile);
    }

    public interface Defensible{
        public void TakeDamage(int damage);
    }

    public interface Offensive{
        public void Attack(Control target/* , int momentum */); //will have separate interfaces for skill offensive behaviors
    }

    public interface Engageable{
        public void ProcessEngagementBy(Control engagingActor);
    }

    public interface Disposition{
        
        public bool IsAggressive{get;set;}
        public bool IsEnemy{get;set;}
    }    
}