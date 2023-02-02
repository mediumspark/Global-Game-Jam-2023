using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers; 

public delegate void BattleQueue(); 
public class BattleManager : MonoBehaviour
{
    public static BattleManager Singleton; 
    public BattleQueue Attacks = null;

    public BattleUnit target; 

    public BattleUnit Target {
        get 
        { 
            return target;
        } 
        set 
        {
            Actor.Target = value;
            target = value; 
        }
    }
    public PlayerUnit Actor;

    public List<PlayerUnit> PlayerUnits;
    public List<BattleUnit> EnemyUnits;
    public Track BattleTrack;
    public GameObject TrackPanel;
    public GameObject BattleUI;

    public GameObject AttackTray;

    public BattleAction AttackQueued;
    private int UnitIndex; 
    private bool inBattle = false;

    private void Awake()
    {
        if (!GetComponent<GameManager>())
            OnBattleStarted(); 
    }

    private void OnBattleStarted()
    {
        Singleton = this;
        Actor = PlayerUnits[0];
        ResetUnitTray();
        inBattle = true;
    }

    private void OnBattleExit()
    {
        inBattle = false; 
    }

    private void ClearUnitTray()
    {
        for (int i = 0; i < AttackTray.transform.childCount; i++)
            Destroy(AttackTray.transform.GetChild(i).gameObject);
    }

    private void LoadUnitTray()
    {
        Actor.InstantiateAttackButtons(); 
    }

    public void ResetUnitTray()
    {
        ClearUnitTray();
        LoadUnitTray(); 
    }

    public void NextActor()
    {
        UnitIndex = UnitIndex < PlayerUnits.Count - 1 ? UnitIndex + 1 : EnemyReadyUp();
        Actor = PlayerUnits[UnitIndex]; 
        AttackQueued = null;
        ResetUnitTray(); 
    }

    public int EnemyReadyUp()
    {
        BattleUI.SetActive(false);

        for (int i = 0; i < EnemyUnits.Count; i++)
            EnemyUnits[i].AIAttack(PlayerUnits[0]);

        Attacks += () => BattleUI.SetActive(true); 

        InvokeBattleQueue(); 

        return 0; 
    }

    public void InvokeBattleQueue()
    {
        Attacks.Invoke();
        Attacks = null; 
    }
}