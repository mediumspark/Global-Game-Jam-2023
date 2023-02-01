using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DELETEBarGoUpTest : MonoBehaviour
{
    public Slider Slider;
    float lerpDuration = 3;
    float startValue = 0;
    float endValue = 100;
    float valueToLerp;
    void Start()
    {
        StartCoroutine(Lerp());
    }
    IEnumerator Lerp()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            Slider.value = valueToLerp;
            yield return null;
        }
        valueToLerp = endValue;
        Slider.value = valueToLerp;

    }
}
