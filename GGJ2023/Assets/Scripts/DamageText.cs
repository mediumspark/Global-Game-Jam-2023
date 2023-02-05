using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DamageText : MonoBehaviour
{
    RectTransform t;
    Vector2 MoveVector = Vector2.zero;

    private void Awake()
    {
        t = GetComponent<RectTransform>();
    }


    public IEnumerator AssignAndMove(string text)
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<TextMeshProUGUI>().text = text;
        MoveVector = Vector2.Lerp(t.anchoredPosition, t.anchoredPosition + Vector2.up * 15, 3.5f);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void Update()
    {
        t.anchoredPosition = MoveVector;
    }

    private void OnDisable()
    {
        Destroy(gameObject); 
    }

    /*  public IEnumerator Velvet()
      {

      }*/

    public void TextFinished()
    {
        Destroy(gameObject); 
    }
}
