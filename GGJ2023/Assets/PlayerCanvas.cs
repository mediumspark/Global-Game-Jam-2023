using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class PlayerCanvas : MonoBehaviour
{
    public PlayerUnit[] Units = new PlayerUnit[3];
    [SerializeField]
    Image[] UnitPort = new Image[3];

    private class PlayerPortraitSelect : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
    {
        public PlayerUnit Unit; 
        public void OnPointerClick(PointerEventData eventData)
        {
            Unit.OnSelect(); 
        }

    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            UnitPort[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
            UnitPort[i].sprite = Units[i].Portrait;
            UnitPort[i].gameObject.AddComponent<PlayerPortraitSelect>().Unit = Units[i]; 
        }
    }
}
