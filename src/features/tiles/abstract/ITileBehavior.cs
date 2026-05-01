using System.Collections.Generic;
using System.Threading.Tasks;
using Board;
using Godot;
using Skills;
using static Skills.SkillNames;

namespace Tiles{
    public interface Swappable{
        public void SwapWith(Control tile);
        public void SwapInvoluntarilyTo(Vector2I toCell, float movementForce);
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
        public /* void */Task ReactToMatchesBySkillType(List<Vector2I> matches, SkillGroups skillGroup, SkillNames.All skillName, bool isAdjacent);
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
        public void /* Task */ ProcessEngagementBy(Control engagingActor);
        //public void EngageTarget(Control target); //don't need this yet
    }

    public interface Disposition{
        
        public bool IsAggressive{get;set;}
        public bool IsEnemy{get;set;}
    }  

    public interface CalculatableDamage {
        public int CalculateDamageFromMomentum(int tilesCovered);
        public int CalculateDamage();
    } 


    public interface TraversableMatching{
        public /* void */ Task ReceivePathAndSkill(List<Vector2I> path, Skill/* ful */ skill);
    }   

    public interface TurnBased{
        public void EndTurn();
        public void BeginTurn();
    }   

    public interface AI{
        public void Resume();
    }   

    public interface Vigilance{
        public void StandWatch();
    } 

    public interface Pursuing{
        public void ChaseActor(Vector2I cell);
    }     

    public interface Pathfindable{
        //public Tileable Map{set;}
        //public Grid<Control> Tiles{set;}
        public List<Vector2I> FindPath(Vector2I target);
    }  

    public interface Pushable{
        public /* void */ Task GetPushed(Vector2I toCell, int enemyStrength);
        public void InteractWithObstacle(Vector2I atCell, float movementForce); //probably shouldn't be here
    }   

    // public interface DelayableSkill{
    //     public void StoreSkill(SkillNames.All skill, int magnitude);
    //     public void ActivateSkill();
    // } 

    public interface Distractable{
        public void BecomeDistracted(int distractingActorIntelligence);
    }
}