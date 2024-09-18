using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemthundercontroller : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<enemy>()!= null)
        {
            playerstats Playerstats = playermanager.instance.player.GetComponent<playerstats>();
            enemystats enemystats = collision.GetComponent<enemystats>();
            Playerstats.DoMagicdamage(enemystats);
        }
    }
}
