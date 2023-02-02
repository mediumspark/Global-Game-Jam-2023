using UnityEngine;
using UnityEngine.UI;

public class B : PlayerUnit
{
    public LilShock LS;
    public BigBolt BB;

    protected override void Start()
    {
        base.Start();
        LS = new LilShock(5, 4);
        BB = new BigBolt(3, 3);
    }

    public class LilShock : BattleAction
    {
        public LilShock(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void OnCast(BattleUnit target, Slider Lane)
        {
            base.OnCast(target, Lane);
        }
    }

    public class BigBolt : BattleAction
    {
        public BigBolt(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void OnCast(BattleUnit target, Slider Lane)
        {
            base.OnCast(target, Lane);
        }
    }

    public override void InstantiateAttackButtons()
    {
        base.InstantiateAttackButtons();

    }

    public override void Special_1()
    {
        if (Target != null)
        {
            BattleManager.Singleton.Attacks += () => Debug.Log($"{gameObject.name}'s turn");
            BattleManager.Singleton.Attacks += () => LS.OnCast(Target, Lane);
            BattleManager.Singleton.NextActor();
            return;
        }

        base.Special_1();
    }

    //AOE
    public override void Special_2()
    {
        base.Special_2();
        BattleManager.Singleton.Attacks += () => Debug.Log($"{gameObject.name}'s turn");
        BattleManager.Singleton.Attacks += () => BB.OnCast(Target, Lane);
        BattleManager.Singleton.NextActor();
    }
}
