using UnityEngine;
using UnityEngine.UI;

public class C : PlayerUnit
{
    public WispFire WF;
    public TheGrasp TG;
    public CCheer cheer; 

    protected override void Start()
    {
        base.Start();
        WF = new WispFire(5, 4);
        TG = new TheGrasp(3, 3);
        cheer = new CCheer();
        name = "C"; 
    }

    public class WispFire : BattleAction
    {
        public WispFire(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
        }
    }

    public class TheGrasp : BattleAction
    {
        public TheGrasp(int Speed, int Cooldown)
        {
            this.Speed = Speed; CoolDown = Cooldown;
        }

        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            base.CreateCast(target, Lane);
        }
    }

    public class CCheer : BattleAction
    {
        public override void CreateCast(BattleUnit target, Slider Lane)
        {
            OnCast += Cheer; 
        }

        private void Cheer(BattleUnit target, Slider Lane)
        {
            var a = FindObjectOfType<A>();
            var b = FindObjectOfType<B>();

            switch (BattleManager.Singleton.BattleTrack.CurrentSeason)
            {
                case Seasons.Spring:
                case Seasons.SpringOfDeception:
                case Seasons.FoolsSpring: 
                    Heal(a, 3); 
                    Heal(b, 3); 
                    break;
                case Seasons.Fall:
                    Heal(a, 50); 
                    Heal(b, 50); 
                    break;
                case Seasons.Summer:
                case Seasons.Hell: 
                    Heal(a, 5); 
                    Heal(b, 5); 
                    break;
                case Seasons.Winter: 
                case Seasons.ThirdWinter:
                    Heal(a, 30);
                    Heal(b, 30);
                    break;
                default:
                    Heal(a, 10); 
                    Heal(b, 10); 
                    break; 
            }
        }

        private void Heal(BattleUnit Ally, int Amount)
        {
            Ally.UnitStats.Health = Ally.UnitStats.Health + Amount < Ally.UnitStats.MaxHealth ?
                Ally.UnitStats.Health + Amount : Ally.UnitStats.MaxHealth;
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
            CommitBattleAction(WF);
            return;
        }

        base.Special_1();
    }

    //AOE
    public override void Special_2()
    {
        base.Special_2();
        //BattleManager.Singleton.ActionQueue += () => Debug.Log($"{gameObject.name}'s turn");
        CommitBattleAction(TG);
    }

    public override void Cheer()
    {
        base.Cheer();
        CommitBattleAction(cheer); 
    }
}
