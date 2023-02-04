using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class BattleAction
{
    public int Speed;
    public int CoolDown;
    public int Damage;
    // public bool Available; 
    public delegate void CastDelegate(BattleUnit target, Slider Lane);
    public CastDelegate OnCast; 

    public BattleAction() { }
    public BattleAction(int Speed, int Cooldown, int Damage)
    {
        this.Speed = Speed; CoolDown = Cooldown; 
    }

    public virtual void CreateCast(BattleUnit target, Slider Lane)
    {
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
    public BattleAction AttackQueued = null;
    public Slider Lane;
    public Track PlayTrack;
    public int PrimaryAttackSpeed, PrimaryAttackDamage;
    protected PrimaryAttack Attack;
    public BattleUnit Target, TargetedBy; 
    public bool isAlive => UnitStats.Health > 0; 

    public void Die()
    {
        //Tempt
        if (tag == "Player")
        {
            BattleManager.Singleton.PlayerUnits.Remove(this as PlayerUnit);
        }
        if (tag == "Enemy")
        {
            BattleManager.Singleton.EnemyUnits.Remove(this);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (BattleManager.Singleton.PlayerUnits.Count == 0 ||
            BattleManager.Singleton.EnemyUnits.Count == 0)
        {
            GameManager.Singleton.OverWorld();
        }
    }

    public class PrimaryAttack : BattleAction
    {
        public PrimaryAttack(int Speed, int Cooldown, int Damage)
        {
            this.Speed = Speed; CoolDown = Cooldown; this.Damage = Damage; 
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
            OnCast += BasicStrike; 
        }

        protected void BasicStrike(BattleUnit target, Slider Lane)
        {
            Lane.value = Lane.value + Speed < Lane.maxValue ? Lane.value + Speed : Lane.value + Speed - Lane.maxValue;
            DealDamage(Damage, target);
          //  Debug.Log($"Basic Attack is at {Speed} and has a cool down of {CoolDown} it will be attacking {target}");
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
        Target = target; 
        Attack.CreateCast(target, Lane);
        AttackQueued = Attack; 
    }

    protected void CommitBattleAction(BattleAction action)
    {
        action.CreateCast(Target, Lane);
        AttackQueued = action;
        BattleManager.Singleton.NextActor();
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

    public void Act()
    {
        AttackQueued.OnCast.Invoke(Target, Lane); 
    }

    public void OnSelect()
    {
        if (BattleManager.Singleton.Actor != this)
        {
            BattleManager.Singleton.Target = this;
            TargetedBy = BattleManager.Singleton.Actor;
        }

        if (BattleManager.Singleton.Actor.AttackQueued != null)
        {
            // BattleManager.Singleton.ActionQueue += () => Debug.Log($"{BattleManager.Singleton.Actor.name}'s turn");
            BattleManager.Singleton.Actor.AttackQueued.CreateCast(this, TargetedBy.Lane);
            BattleManager.Singleton.NextActor();
            return;
        }

        if (tag == "Player")
        {
            Debug.Log("I'm a friend");
        }

        if (tag == "Enemy")
        {
            Debug.Log("I'm a foe");
        }
    }

    private void OnMouseDown()
    {
        OnSelect(); 
    }
}
