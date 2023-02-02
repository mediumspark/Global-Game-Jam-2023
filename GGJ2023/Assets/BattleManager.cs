using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BattleQueue(); 
public class BattleManager : MonoBehaviour
{
    public static BattleManager Singleton; 
    public BattleQueue Attacks = null;

    public BattleUnit Target, Actor;

    public List<BattleUnit> PlayerUnits;
    public List<BattleUnit> EnemyUnits;
    public Track BattleTrack;
    public GameObject TrackPanel;
    public GameObject BattleUI;

    public GameObject AttackTray; 

    int UnitIndex = 0; 

    private void Awake()
    {
        Singleton = this;
        Actor = PlayerUnits[UnitIndex];
        ResetUnitTray(); 
    }

    private void Update()
    {
        Actor = PlayerUnits[UnitIndex];
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
        UnitIndex = UnitIndex + 1 < PlayerUnits.Count ? UnitIndex + 1 : EnemyReadyUp();
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
public enum Seasons { }
