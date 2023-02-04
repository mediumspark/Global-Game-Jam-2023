using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; 

public class PlayerUnit : BattleUnit
{
    public Button ButtonPrefab;
    public Sprite Portrait;
    public virtual void BasicAttack()
    {
        if (Target != null && AttackQueued != null)
        {
            //BattleManager.Singleton.ActionQueue += () => Debug.Log($"{name}'s turn");
            CommitBattleAction(Attack);
            return;
        }

        AttackQueued = Attack; 

        Debug.Log("Attack Queued");
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
}
