using UnityEngine;
using UnityEngine.UI;

public class B : PlayerUnit
{
    public LilShock LS;
    public BigBolt BB;
    public BCheer cheer;

    protected override void Start()
    {
        base.Start();
        name = "B"; 
        LS = new LilShock(5, 4);
        BB = new BigBolt(3, 3);
        cheer = new BCheer(); 
    }

    public class LilShock : BattleAction
    {
        public LilShock(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
        }
    }

    public class BigBolt : BattleAction
    {
        public BigBolt(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
        }
    }

    public class BCheer : BattleAction
    {
        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            OnCast += Cheer; 

        }

        public void Cheer (BattleUnit target, Slider Lane)
        {
            FindObjectOfType<C>().SpeedMod += 1.5f;
            FindObjectOfType<A>().SpeedMod += 1.5f;
        }
    }

    public override void InstantiateAttackButtons()
    {
        base.InstantiateAttackButtons();

    }

    public override void Special_1()
    {
        if (Target != null && AttackQueued != null)
        {
            // BattleManager.Singleton.ActionQueue += () => Debug.Log($"{gameObject.name}'s turn");
            CommitBattleAction(LS);
            return;
        }

        base.Special_1();
    }

    //AOE
    public override void Special_2()
    {
        base.Special_2();
        // BattleManager.Singleton.ActionQueue += () => Debug.Log($"{gameObject.name}'s turn");
        CommitBattleAction(BB);
    }

    public override void Cheer()
    {
        base.Cheer();
        CommitBattleAction(cheer); 
    }
}
