using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; 

public class PlayerUnit : BattleUnit
{
    public Button ButtonPrefab;
    public Sprite Portrait;

    protected override void Start()
    {
        base.Start();
        Lane.handleRect.GetComponent<Image>().sprite = Portrait; 
    }
    public virtual void BasicAttack()
    {
        if (Target != null && AttackQueued != null)
        {
            CommitBattleAction(Attack);
            return;
        }

        AttackQueued = Attack; 
    }

    private void AddButtonListeners(Button Button, string Text, UnityEngine.Events.UnityAction call)
    {
        Button.onClick.AddListener(call);

        if (Target == null)
        {
            Button.onClick.AddListener(() => { 
                BattleManager.Singleton.TargetText.text = "Choose a target";
            });
        }

        Button.GetComponentInChildren<TextMeshProUGUI>().text = Text;

    }
    public virtual void InstantiateAttackButtons()
    {
        var Attack = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        AddButtonListeners(Attack, "Basic Attack", BasicAttack); 

        var SpecialAttack_1 = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        AddButtonListeners(SpecialAttack_1, "Special Attack 1", Special_1);

        var SpecialAttack_2 = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        AddButtonListeners(SpecialAttack_2, "Special Attack 2", Special_2);

        var OnCheer = Instantiate(ButtonPrefab, BattleManager.Singleton.AttackTray.transform);
        OnCheer.onClick.AddListener(Cheer);
        OnCheer.GetComponentInChildren<TextMeshProUGUI>().text = "Cheer";
    }
}
