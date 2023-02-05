using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerCanvas : MonoBehaviour
{
    public PlayerUnit[] Units = new PlayerUnit[3];
    [SerializeField]
    Image[] UnitPort = new Image[3];
    Slider[] HealthBars = new Slider[3]; 

    private class PlayerPortraitSelect : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
    {
        public PlayerUnit Unit; 
        public void OnPointerClick(PointerEventData eventData)
        {
            Unit.OnSelect(); 
        }
    }

    public Transform PlayerPortraitPanel(int Index) => UnitPort[Index].transform;
    public Transform PlayerPortraitPanel(BattleUnit unit)
    {
        if(unit is PlayerUnit)
        {
            return UnitPort[BattleManager.Singleton.PlayerUnits.IndexOf(unit as PlayerUnit)].transform; 
        }
        Debug.LogError("This is only meant to get portraits for the player units");
        return null;
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            UnitPort[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
            UnitPort[i].sprite = Units[i].Portrait;
            UnitPort[i].gameObject.AddComponent<PlayerPortraitSelect>().Unit = Units[i];
            HealthBars[i] = UnitPort[i].GetComponentInChildren<Slider>(); 
            HealthBars[i].maxValue = Units[i].UnitStats.MaxHealth;
        }
    }

    private void Update()
    {
        for(int i = 0; i < HealthBars.Length; i++)
        {
            HealthBars[i].value = Units[i].UnitStats.Health;
        }
    }
}
