using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldowntimer;

    protected virtual void Update()
    {
        cooldowntimer -= Time.deltaTime;
    }

    public virtual bool canuseskill()
    {
        if(cooldowntimer < 0)
        {
            Useskill();
            cooldowntimer = cooldown;
            return true;
        }
        return false;

    }
    public virtual void Useskill()
    {
    
    }
}
