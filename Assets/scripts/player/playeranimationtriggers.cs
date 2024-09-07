 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimationtriggers : MonoBehaviour
{
    private player player => GetComponentInParent<player>();

    private void Animationtrigger()
    {
        player.Animationtrigger();
    }

    private void attacktrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackcheck.position , player.attackcheckradius);

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<enemy>() != null)
            {
                hit.GetComponent<enemy>().damage();
            }
        }
    }
    private void swordthrowtrigger()
    {
        skillmanager.instance.sword.createsword();
    }
}
