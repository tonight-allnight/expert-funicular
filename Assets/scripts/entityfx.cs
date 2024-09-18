using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityfx : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("flash fx")]
    [SerializeField] private Material hitmat;
    [SerializeField] private float flashduration;
    private Material originalmat;
    [Header("×´Ì¬ÑÕÉ«")]
    [SerializeField] private Color[] ignitedcolor;
    [SerializeField] private Color[] chilledcolor;
    [SerializeField] private Color[] shockedcolor;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalmat = sr.material;
    }
    public void maketransparent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }
    private IEnumerator flashfx()
    {
        sr.material = hitmat;
        Color currentcolor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashduration);
        sr.color = currentcolor;
        sr.material = originalmat;
    }
    private void redcolorblink()
    {
        if(sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void cannelcolorchange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
    #region ×´Ì¬±äÉ«
    public void chillededfxfor(float _seconds)
    {
        InvokeRepeating("chilledcolorfx", 0, .3f);
        Invoke("cannelcolorchange", _seconds);
    }
    public void ignitedfxfor(float _seconds)
    {
        InvokeRepeating("ignitedcolorfx",0,.3f);
        Invoke("cannelcolorchange" , _seconds);
    }
    public void shockedfxfor(float _seconds)
    {
        InvokeRepeating("shockedcolorfx", 0, .3f);
        Invoke("cannelcolorchange", _seconds);
    }
    private void ignitedcolorfx()
    {
        if (sr.color != ignitedcolor[0])
        {
            sr.color = ignitedcolor[0];
        }
        else
        {
            sr.color = ignitedcolor[1];
        }
    }
    private void shockedcolorfx()
    {
        if (sr.color != shockedcolor[0])
        {
            sr.color = shockedcolor[0];
        }
        else
        {
            sr.color = shockedcolor[1];
        }
    }
    private void chilledcolorfx()
    {
        if (sr.color != chilledcolor[0])
        {
            sr.color = chilledcolor[0];
        }
        else
        {
            sr.color = chilledcolor[1];
        }
    }
    #endregion
}
