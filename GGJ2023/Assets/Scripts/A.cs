using UnityEngine;
using UnityEngine.UI;

public class A : PlayerUnit
{
    public FingerBlast FB;
    public HandGambit HG;
    protected override void Start()
    {
        base.Start();
        FB = new FingerBlast(5, 4);
        HG = new HandGambit(3, 3); 
    }

    public class FingerBlast : BattleAction
    {
        public FingerBlast(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
        }
    }

    public class HandGambit : BattleAction
    {
        public HandGambit(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
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
            //BattleManager.Singleton.ActionQueue += () => Debug.Log($"{gameObject.name}'s turn");
            CommitBattleAction(FB);
        }

        base.Special_1();
    }

    public override void Special_2()
    {
        base.Special_2();
        //BattleManager.Singleton.ActionQueue += () => Debug.Log($"{gameObject.name}'s turn");
        CommitBattleAction(HG);
    }
}
