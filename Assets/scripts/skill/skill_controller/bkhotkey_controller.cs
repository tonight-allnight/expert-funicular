using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bkhotkey_controller : MonoBehaviour
{
    private KeyCode myhotkey;
    private SpriteRenderer sr;
    private TextMeshProUGUI mytext;

    private Transform myenemy;
    private blackhole_skill_controller blackhole;

    public void setuphotkey(KeyCode _mynewhotkey , Transform _myenemy , blackhole_skill_controller _myblackhole)
    {
        sr = GetComponent<SpriteRenderer>();
        mytext = GetComponentInChildren<TextMeshProUGUI>();
        myenemy = _myenemy;
        blackhole = _myblackhole;
        myhotkey = _mynewhotkey;
        mytext.text = _mynewhotkey.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(myhotkey))
        {
            blackhole.addenemytolist(myenemy);

            mytext.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
