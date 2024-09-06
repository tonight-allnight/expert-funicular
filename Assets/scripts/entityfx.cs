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

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalmat = sr.material;
    }
    private IEnumerator flashfx()
    {
        sr.material = hitmat;
        yield return new WaitForSeconds(flashduration);
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

    private void cannelcolorblink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
