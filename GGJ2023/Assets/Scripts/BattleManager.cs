using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using System.Linq;
using UnityEngine.Pool;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Singleton;
    public GameObject BattleUI;
    public PlayerCanvas PlayerUI; 

    public BattleUnit target;
    public TextMeshProUGUI CurrentActorText; 
    public TextMeshProUGUI TargetText; 
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

    public GameObject AttackTray;

    private int UnitIndex; 
    private bool inBattle = false;

    public ObjectPool<TextMeshProUGUI> TextPool;

    private void Awake()
    {
        Singleton = this; 
    }

    public void OnBattleStarted()
    {
        PlayerUI.Units[0] = PlayerUnits[0];
        PlayerUI.Units[1] = PlayerUnits[1];
        PlayerUI.Units[2] = PlayerUnits[2];

        BattleUI.SetActive(true); 
        Actor = PlayerUnits[0];
        ResetUnitTray();
        inBattle = true;
        PlayerUI.gameObject.SetActive(true);
    }

    private void Update()
    {
        CurrentActorText.text = $"{Actor}";
    }

    public void OnBattleExit()
    {
        PlayerUnits.Clear();
        for (int i = 0; i < TrackPanel.transform.childCount; i++)
            Destroy(TrackPanel.transform.GetChild(i).gameObject);

        inBattle = false;
        BattleUI.SetActive(false);
        PlayerUI.gameObject.SetActive(false);
        GetComponent<LevelManager>().DefeatedEnemiesInLevel.Add(GameManager.Singleton.OWEnemyID);
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
        TargetText.text = ""; 
        //AttackQueued = null;
        ResetUnitTray();
    }

    public int EnemyReadyUp()
    {
        BattleUI.SetActive(false);

        for (int i = 0; i < EnemyUnits.Count; i++)
            EnemyUnits[i].AIAttack(PlayerUnits[Random.Range(0, 3)]);

        InvokeBattleQueue();
        
        BattleUI.SetActive(true);

        return 0; 
    }
    public void InvokeBattleQueue()
    {
        List<BattleUnit> AllLivingUnits = new List<BattleUnit>();
        AllLivingUnits.AddRange(EnemyUnits);
        AllLivingUnits.AddRange(PlayerUnits);
        AllLivingUnits.OrderBy(ctx => BattleTrack.Rank(ctx.Lane));

        foreach(BattleUnit unit in AllLivingUnits)
        {
         //   Debug.Log($"{unit} is attacking {unit.Target} with {unit.AttackQueued}");
            unit.Act(); 
        }
        
        //ActionQueue = null; 
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan; 
        for (int i = 0; i < 6; i++)
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.5f); 
    }
#endif
}