using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemobject_trigger : MonoBehaviour
{
    private itemobject myitemobject => GetComponentInParent<itemobject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<player>() != null)
        {
            if(collision.GetComponent<characterstates>().isdead)
            { 
                return; 
            }
            myitemobject.pickitem();
        }
    }
}
