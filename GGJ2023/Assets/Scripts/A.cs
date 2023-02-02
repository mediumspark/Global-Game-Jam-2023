﻿using UnityEngine;
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

        public override void OnCast(BattleUnit target, Slider Lane)
        {
            base.OnCast(target, Lane);
        }
    }

    public class HandGambit : BattleAction
    {
        public HandGambit(int Speed, int Cooldown)
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
            BattleManager.Singleton.Attacks += () => FB.OnCast(Target, Lane);
            BattleManager.Singleton.NextActor();
            return;
        }

        base.Special_1();
    }

    public override void Special_2()
    {
        base.Special_2();
        BattleManager.Singleton.Attacks += () => Debug.Log($"{gameObject.name}'s turn");
        BattleManager.Singleton.Attacks += () => FB.OnCast(Target, Lane);
        BattleManager.Singleton.NextActor();
    }
}
