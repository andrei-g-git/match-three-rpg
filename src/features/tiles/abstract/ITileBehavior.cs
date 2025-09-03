using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Skills;
using static Skills.SkillNames;

namespace Tiles{
    public interface Swappable{
        public void SwapWith(Control tile);
    }    

    public interface Removable{
        public void PrepDestroy();
        public void Remove();
        public Task WaitForRemoved();
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
        public /* void */Task ReactToMatchesBySkillType(List<Vector2I> matches, SkillGroups skillGroup, /* SkillNames.All skillType, */ bool isAdjacent);
    }

    public interface Walkable{
        public void LeadPlayer(Control tile);
    }

    public interface Defensible{
        public void TakeDamage(int damage);
    }

    public interface Offensive{
        public void Attack(Control target/* , int momentum */); 
        public void AttackWithMomentum(Control target, int momentum); 
    }

    public interface Engageable{
        public void ProcessEngagementBy(Control engagingActor);
    }

    public interface Disposition{
        
        public bool IsAggressive{get;set;}
        public bool IsEnemy{get;set;}
    }  

    public interface CalculatableDamage {
        public int CalculateDamageFromMomentum(int tilesCovered);
    } 


    public interface TraversableMatching{
        public /* void */ Task ReceivePathAndSkill(List<Vector2I> path, Skill/* ful */ skill);
    }       
}