using System.Collections.Generic;
using UnityEngine;

public class sword_skill_controller : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cr;
    private player player;

    private bool canrotate = true;
    private bool isreturning;

    private float freezetimeduration;
    private float returnspeed = 12;

    [Header("pierce info")]
    private float pierceamount;

    [Header("bounce info")]
    private float bouncespeed;
    private bool isbouncing;
    private float bounceamount;
    private List<Transform> enemytarget;
    private int targetindex;

    [Header("spin info")]
    private float maxtraveldistance;
    private float spinduration;
    private float spintimer;
    private bool wasstopped;
    private bool isspinning;

    private float hittimer;
    private float hitcooldown;

    private float spindirection;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        cr = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Destoryme()
    {
        Destroy(gameObject);
    }

    public void setupsword(Vector2 _dir, float _gravityscale, player _player , float _freezetimeduration , float _returnspeed) 
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityscale;
        player = _player;
        freezetimeduration = _freezetimeduration;
        returnspeed = _returnspeed;
        if(pierceamount <= 0)
           anim.SetBool("rotate", true);

        spindirection = Mathf.Clamp(rb.velocity.x, -1, 1);

        Invoke("Destoryme", 2);
    }
    public void setuppierce(int _pierceamount)
    {
        pierceamount = _pierceamount;
    }

    public void setupbounce(bool _isbouncing , int _amountofbounce , float _bouncespeed)
    {
        isbouncing = _isbouncing;
        bounceamount = _amountofbounce;
        bouncespeed = _bouncespeed;

        enemytarget = new List<Transform>();
    }

    public void setupspin(bool _isspinning ,float _maxtraveldistance ,float _spinduratoin , float _hitcooldown )
    {
        isspinning = _isspinning;
        maxtraveldistance = _maxtraveldistance;
        spinduration = _spinduratoin;
        hitcooldown = _hitcooldown;
    }
    public void returnsword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isreturning = true;
    }
    private void Update()
    {
        if (canrotate)
            transform.right = rb.velocity;

        if (isreturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnspeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.clearthesword();
            }
        }

        bouncelogic();
        spinlogic();
    }

    private void spinlogic()
    {
        if (isspinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxtraveldistance && !wasstopped)//距离过远停止然后继续转动往前，速度下降的代码
            {
                stopwhenspinning();
            }
            if (wasstopped)
            {
                spintimer -= Time.deltaTime;
                //Debug.Log(spintimer);
                //Debug.Log(wasstopped);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spindirection, transform.position.y), 1.5f * Time.deltaTime);
                if (spintimer < 0)
                {
                    isreturning = true;
                    isspinning = false;
                }

                hittimer -= Time.deltaTime;
                if (hittimer < 0)
                {
                    hittimer = hitcooldown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .9f);
                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<enemy>() != null)
                        {
                            hit.GetComponent<enemy>().StartCoroutine("Freezetimefor" , freezetimeduration);
                            hit.GetComponent<enemy>().damage();
                        }
                    }
                }
            }
        }
    }

    private void stopwhenspinning()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        if(!wasstopped)
            spintimer = spinduration;
        wasstopped = true;
    }

    private void bouncelogic()
    {
        if (isbouncing && enemytarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemytarget[targetindex].position, bouncespeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemytarget[targetindex].position) < .1f)
            {
                enemytarget[targetindex].GetComponent<enemy>().damage();
                enemytarget[targetindex].GetComponent<enemy>().StartCoroutine("Freezetimefor" , freezetimeduration);
                targetindex++;
                bounceamount--;
                if (bounceamount < 0)
                {
                    isbouncing = false;
                    isreturning = true;
                }
                if (targetindex >= enemytarget.Count)
                {
                    targetindex = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isreturning)
        {
            return;
        }

        if(collision.GetComponent<enemy>() != null)
        {
            enemy enemy = collision.GetComponent<enemy>();
            enemy.damage();
            enemy.StartCoroutine("Freezetimefor" , freezetimeduration);
        }
        setuuptargetbounce(collision);
        stukinto(collision);
    }

    private void setuuptargetbounce(Collider2D collision)
    {
        if (collision.GetComponent<enemy>() != null)
        {
            if (isbouncing && enemytarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<enemy>() != null)
                    {
                        enemytarget.Add(hit.transform);
                    }
                }
            }
        }
    }

    private void stukinto(Collider2D collision)
    {

        if (pierceamount > 0 && collision.GetComponent<enemy>() != null)
        {
            pierceamount--;
            return;
        }
        if (isspinning)
        {
            stopwhenspinning();//撞到第一个会停下来
            return;
        }

        canrotate = false;
        cr.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isbouncing && enemytarget.Count > 0)
            return;

        anim.SetBool("rotate", false);
        transform.parent = collision.transform;
    }
}
