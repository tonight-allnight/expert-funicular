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
    public float defaultspeed;

    [Header("attack info")]
    public float attackdistance;
    public float attackcooldown;
    [HideInInspector] public float lasttimeattacked;
    public enemystatemachine statemachine { get; private set; }

    private float deadtimer = 3;

    public string lastanimboolname {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
        statemachine = new enemystatemachine();
        defaultspeed = movespeed;
    }
    protected override void Update()
    {
        base.Update();
        statemachine.currentstate.update();
    }
    public override void slowentityby(float _slowpercentage, float slowduration)
    {
        movespeed = movespeed * (1 - _slowpercentage);
        animator.speed = animator.speed * (1 - _slowpercentage);
        Invoke("returndefaultspeed",slowduration);
    }
    protected override void returndefaultspeed()
    {
        base.returndefaultspeed();
        movespeed = defaultspeed;
    }

    public virtual void Freezetimer( bool _timefreeze)
    {
        if (_timefreeze)
        {
            movespeed = 0;
            animator.speed = 0;
        }
        else
        {
            movespeed = defaultspeed;
            animator.speed = 1;
        }
    }

    public virtual void freezetimefor(float _duration) => StartCoroutine(Freezetimecorroutine(_duration));
    protected virtual IEnumerator Freezetimecorroutine(float _seconds)
    {
        Freezetimer(true);
        yield return new WaitForSeconds( _seconds );
        Freezetimer(false);
    }
    #region ��������
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
    #endregion
    public virtual bool Canbestunned()
    {
        if (canbestunned)
        {
            closecounterattackwindow();
            return true;
        }
        return false;
    }

    public virtual void assignlastanimname(string _animboolname)
    {
        lastanimboolname = _animboolname;
    }

    public virtual void animationfinishtrigger() => statemachine.currentstate.animationfinishtrigger();

    public virtual RaycastHit2D isplayerdetected() => Physics2D.Raycast(wallcheck.position, Vector2.right * facingdir, 50, whatisplayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(attackdistance * facingdir + transform.position.x, transform.position.y));
    }
    public override void Die()
    {
        base.Die();
    }
}
