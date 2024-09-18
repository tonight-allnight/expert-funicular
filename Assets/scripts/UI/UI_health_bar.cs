using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_health_bar : MonoBehaviour
{
    private entity entity1;
    private RectTransform rectTransform;
    private characterstates mycharacterstate;
    private Slider slider;
    private void Start()
    {
        entity1 = GetComponentInParent<entity>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        mycharacterstate = GetComponentInParent<characterstates>();
        entity1.onFlipped += FlipUI;
        mycharacterstate.onHealthed += UpdatehealthUI;

        UpdatehealthUI();
    }

    private void UpdatehealthUI()
    {
        slider.maxValue = mycharacterstate.getmaxHP();
        slider.value = mycharacterstate.currentHp;
    }
    private void FlipUI() => rectTransform.Rotate(0, 180, 0);



    private void OnDisable()
    {
        entity1.onFlipped -= FlipUI;
        mycharacterstate.onHealthed -= UpdatehealthUI;
    }
}
