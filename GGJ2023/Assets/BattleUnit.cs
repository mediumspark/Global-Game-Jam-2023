using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleAction
{
    public int Speed;
    public int CoolDown;
    public int Damage;
   // public bool Available; 

    public BattleAction() { }
    public BattleAction(int Speed, int Cooldown, int Damage)
    {
        this.Speed = Speed; CoolDown = Cooldown; 
    }

    public virtual void OnCast(BattleUnit target, Slider Lane)
    {
        BattleUnit ba = target; 
    }

    public void DealDamage(int Attack, BattleUnit target)
    {
        target.UnitStats.Health -= Attack;
        if (!target.isAlive)
        {
            target.Die();
        }
    }
}

public class BattleUnit : MonoBehaviour
{
    public Stats UnitStats;
    [SerializeField]
    public Slider SliderPrefab;
    public Seasons CurrentSeason; 
    public Slider Lane;
    public Track PlayTrack;
    public int PrimaryAttackSpeed, PrimaryAttackDamage;
    protected PrimaryAttack Attack;
    public BattleUnit Target, TargetedBy; 
    public bool isAlive => UnitStats.Health > 0; 

    public void Die()
    {
        Destroy(gameObject); 
    }

    public class PrimaryAttack : BattleAction
    {
        public PrimaryAttack(int Speed, int Cooldown, int Damage)
        {
            this.Speed = Speed; CoolDown = Cooldown; this.Damage = Damage; 
        }

        public override void OnCast(BattleUnit target, Slider Lane)
        {
            base.OnCast(target, Lane);
            Lane.value = Lane.value + Speed < Lane.maxValue ? Lane.value + Speed : Lane.value + Speed - Lane.maxValue;
            DealDamage(Damage, target);
            Debug.Log($"Basic Attack is at {Speed} and has a cool down of {CoolDown} it will be attacking {target}"); 
        }

    }

    protected virtual void Start()
    {
        Attack = new PrimaryAttack(PrimaryAttackSpeed, 0, PrimaryAttackDamage);
        PlayTrack = BattleManager.Singleton.BattleTrack;
        Lane = Instantiate(SliderPrefab, BattleManager.Singleton.TrackPanel.transform);
    }


    private void Update()
    {
        if (PlayTrack.isInSeasonTerritory(Lane))
        {
            CurrentSeason = PlayTrack.CurrentSeason; 
        }
    }

    public virtual void AIAttack(BattleUnit target)
    {
        BattleManager.Singleton.Attacks += () => Attack.OnCast(target, Lane); 
    }


    public virtual void Special_1()
    {
        Debug.Log("Primary Special");
    }
    public virtual void Special_2()
    {
        Debug.Log("Secondary Special");
    }

    public virtual void Cheer()
    {
        Debug.Log("Cheer");
    }

    private void OnMouseDown()
    {
        if(BattleManager.Singleton.Actor != this)
        {
            BattleManager.Singleton.Target = this;
            TargetedBy = BattleManager.Singleton.Actor; 
        }

        if(BattleManager.Singleton.AttackQueued != null)
        {
            BattleManager.Singleton.Attacks += () => Debug.Log($"{BattleManager.Singleton.Actor.name}'s turn");
            BattleManager.Singleton.Attacks += () => BattleManager.Singleton.AttackQueued.OnCast(Target, TargetedBy.Lane);
            BattleManager.Singleton.NextActor();
            return;
        }

        if(tag == "Player")
        {
            Debug.Log("I'm a friend");
        }

        if(tag == "Enemy")
        {
            Debug.Log("I'm a foe");
        }
    }
}
