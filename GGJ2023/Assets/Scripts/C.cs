using UnityEngine;
using UnityEngine.UI;

public class C : PlayerUnit
{
    public WispFire WF;
    public TheGrasp TG;

    protected override void Start()
    {
        base.Start();
        WF = new WispFire(5, 4);
        TG = new TheGrasp(3, 3);
    }

    public class WispFire : BattleAction
    {
        public WispFire(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void OnCast(BattleUnit target, Slider Lane)
        {
            base.OnCast(target, Lane);
        }
    }

    public class TheGrasp : BattleAction
    {
        public TheGrasp(int Speed, int Cooldown)
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
            BattleManager.Singleton.Attacks += () => WF.OnCast(Target, Lane);
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
        BattleManager.Singleton.Attacks += () => TG.OnCast(Target, Lane);
        BattleManager.Singleton.NextActor();
    }
}
