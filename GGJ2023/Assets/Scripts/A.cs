using UnityEngine;
using UnityEngine.UI;

public class A : PlayerUnit
{
    public FingerBlast FB;
    public HandGambit HG;
    public ACheer ACheering; 
    protected override void Start()
    {
        base.Start();
        name = "A"; 
        FB = new FingerBlast(15 + Mathf.RoundToInt(15 * SpeedMod), 4);
        HG = new HandGambit(13 + Mathf.RoundToInt(15 * SpeedMod), 3);
        ACheering = new ACheer(); 
    }

    public class FingerBlast : BattleAction
    {
        public FingerBlast(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            OnCast += AttackDamage;
        }

        private void AttackDamage(BattleUnit target, Slider Lane)
        {
            var A = FindObjectOfType<A>();
            DealDamage(1, A);
            DealDamage(15, target);
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
            OnCast += AttackDamage; 
        }

        private void AttackDamage(BattleUnit target, Slider Lane)
        {
            var A = FindObjectOfType<A>();
            DealDamage(10, A);
            foreach(var Units in BattleManager.Singleton.EnemyUnits)
            {
                DealDamage(10, Units);
            }
        }
    }

    public class ACheer: BattleAction
    {
        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            OnCast += Cheer;
        }

        private void Cheer(BattleUnit target, Slider slider)
        {
            var B = FindObjectOfType<B>().AttackMod += 1.5f;
            var C = FindObjectOfType<C>().AttackMod += 1.5f;
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

    public override void Cheer()
    {
        base.Cheer();
        CommitBattleAction(ACheering);
    }
}
