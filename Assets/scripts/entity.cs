using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
    [Header("knockback info")]
    [SerializeField] protected Vector2 knockbackdir;
    [SerializeField] private float knockbackduration;
    protected bool isknocked;

    [Header("collision info")]
    public Transform attackcheck;
    public float attackcheckradius;
    [SerializeField] protected Transform groundcheck;
    [SerializeField] protected float groundcheckdistance;
    [SerializeField] protected Transform wallcheck;
    [SerializeField] protected float wallcheckdistance;
    [SerializeField] protected LayerMask whatisground;
    #region 组件
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public entityfx fx { get; private set; }

    public SpriteRenderer sr { get; private set; }
    #endregion
    public int facingdir { get; private set; } = 1;
    protected bool isright = true;

    
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();  
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<entityfx>();
    }
    protected virtual void Update()
    {

    }
    public virtual void damage()
    {
        fx.StartCoroutine("flashfx");
        StartCoroutine("hitknockback");
        //Debug.Log(gameObject.name);
    }
    protected virtual IEnumerator hitknockback()
    {
        isknocked = true;
        rb.velocity = new Vector2(knockbackdir.x * facingdir * -1, knockbackdir.y);
        yield return new WaitForSeconds(knockbackduration);
        isknocked = false;  
    }

    #region 速度
    public virtual void zerovelocity()
    {
        if (isknocked)
        {
            return;
        }
        rb.velocity = new Vector2(0, 0);
    }
    public virtual void setvelocity(float _xvelocity, float _yvelocity)
    {
        if (isknocked)
        {
            return;
        }
        rb.velocity = new Vector2(_xvelocity, _yvelocity);
        Flipcontroller(_xvelocity);
    }
    #endregion
    #region 碰撞
    public virtual bool isgrounddetected() => Physics2D.Raycast(groundcheck.position, Vector2.down, groundcheckdistance, whatisground);
    public virtual bool iswalletected() => Physics2D.Raycast(wallcheck.position, Vector2.right * facingdir, wallcheckdistance, whatisground);


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundcheck.position, new Vector3(groundcheck.position.x, groundcheck.position.y - groundcheckdistance));
        Gizmos.DrawLine(wallcheck.position, new Vector3(wallcheck.position.x + wallcheckdistance, wallcheck.position.y));
        Gizmos.DrawWireSphere(attackcheck.position , attackcheckradius);
    }
    #endregion

    #region 翻转
    public virtual void flip()
    {
        facingdir = facingdir * -1;
        isright = !isright;
        transform.Rotate(0, 180, 0);
    }

    public virtual void Flipcontroller(float _x)
    {
        if (_x > 0 && !isright)
        {
            flip();
        }
        else if (_x < 0 && isright)
        {
            flip();
        }
    }
    #endregion

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
}
