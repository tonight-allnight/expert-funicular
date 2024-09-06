using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : entity
{
    [Header("stun info")]
    public float stunnedduration;
    public Vector2 stunneddir;
    public bool canbestunned;
    [SerializeField] protected GameObject counterImage;

    [SerializeField] protected LayerMask whatisplayer;
    [Header("move info")]
    public float movespeed;
    public float idletime;
    public float battletime;

    [Header("attack info")]
    public float attackdistance;
    public float attackcooldown;
    [HideInInspector] public float lasttimeattacked;
    public enemystatemachine statemachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        statemachine = new enemystatemachine();
    }
    protected override void Update()
    {
        base.Update();
        statemachine.currentstate.update();
    }
    public virtual void opencounterattackwindow()
    {
        canbestunned = true;
        counterImage.SetActive(true);
    }
    public virtual void closecounterattackwindow()
    {
        canbestunned = false;
        counterImage.SetActive(false);
    }
    public  virtual bool Canbestunned()
    {
        if (canbestunned)
        {
            closecounterattackwindow();
            return true;
        }
        return false;
    }

    public virtual void animationfinishtrigger() => statemachine.currentstate.animationfinishtrigger();

    public virtual RaycastHit2D isplayerdetected() => Physics2D.Raycast(wallcheck.position, Vector2.right * facingdir, 50, whatisplayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(attackdistance * facingdir + transform.position.x, transform.position.y));
    }
}
