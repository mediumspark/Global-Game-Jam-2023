using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class BattleAction
{
    public int Speed;
    public int CoolDown;
    public int Damage;
    public bool Available; 

    public BattleAction() { }
    public BattleAction(int Speed, int Cooldown, int Damage)
    {
        this.Speed = Speed; CoolDown = Cooldown; 
    }

    public virtual void OnCast(BattleUnit target, Slider Lane)
    {
        target.health -= Damage; 
    }
}

public class BattleUnit : MonoBehaviour
{
    public int health;
    public enum State { idle, acting, targeting }
    public Seasons CurrentSeason;
    [SerializeField]
    public Slider SliderPrefab; 
    public Slider Lane;
    public Button ButtonPrefab;
    public Track PlayTrack;
    public int PrimaryAttackSpeed, PrimaryAttackDamage;
    PrimaryAttack Attack;

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
            target.health -= Damage; 
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

    public virtual void InstantiateAttackButtons()
    {
        var Attack = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        Attack.onClick.AddListener(BasicAttack);
        Attack.GetComponentInChildren<TextMeshProUGUI>().text = "Basic Attack";

        var SpecialAttack_1 = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        SpecialAttack_1.onClick.AddListener(Special_1);
        SpecialAttack_1.GetComponentInChildren<TextMeshProUGUI>().text = "Special Attack 1";


        var SpecialAttack_2 = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        SpecialAttack_2.onClick.AddListener(Special_2);
        SpecialAttack_2.GetComponentInChildren<TextMeshProUGUI>().text = "Special Attack 2";

        var Cheer = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        Cheer.onClick.AddListener(this.Cheer);
        Cheer.GetComponentInChildren<TextMeshProUGUI>().text = "Cheer";
    }

    public virtual void BasicAttack()
    {
        if (BattleManager.Singleton.Target != null)
        {
            BattleManager.Singleton.Attacks += () => Debug.Log($"{name}'s turn");
            BattleManager.Singleton.Attacks += () => Attack.OnCast(BattleManager.Singleton.Target, Lane);
            BattleManager.Singleton.NextActor();
            BattleManager.Singleton.Target = null;
            return; 
        }

        Debug.Log("Target Required");
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
