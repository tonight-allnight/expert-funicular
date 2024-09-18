using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected float cooldowntimer;
    protected player player;
    protected virtual void Start()
    {
        player = playermanager.instance.player;
    }
    protected virtual void Update()
    {
        cooldowntimer -= Time.deltaTime;
    }

    public virtual bool canuseskill()
    {
        if(cooldowntimer < 0)
        {
            cooldowntimer = cooldown;
            Useskill();
            return true;
        }
        return false;

    }
    public virtual void Useskill()
    {
    
    }

    protected virtual Transform findclosestenemy(Transform _checktransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checktransform.position, 15);
        float closestdistance = Mathf.Infinity;
        Transform closetenemy = null;
        if (colliders[0] == null)
        {
            return null;
        }
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                float distanttoenemy = Vector2.Distance(_checktransform.position, hit.transform.position);
                if (distanttoenemy < closestdistance)
                {
                    closestdistance = distanttoenemy;
                    closetenemy = hit.transform;
                    //Debug.Log(closestdistance);
                }
            }
        }
        return closetenemy;
    }
}
