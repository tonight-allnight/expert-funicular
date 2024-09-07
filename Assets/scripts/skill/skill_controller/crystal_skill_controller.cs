using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class crystal_skill_controller : MonoBehaviour
{
    [SerializeField] private float colorlosingspeed;
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float crystalexisttime;

    private SpriteRenderer sr;

    private bool canexplode;
    private bool cangrow;
    private float growspeed = 5;

    private bool canmove;
    private float movespeed;

    private Transform closettarget;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void setupcrystal(float _crystalduration , bool _canexplode, bool _canmove, float _movespeed , Transform _closettarget)
    {
        crystalexisttime = _crystalduration;
        canexplode = _canexplode;
        canmove = _canmove;
        movespeed = _movespeed;
        closettarget = _closettarget;
    }
    private void Update()
    {
        crystalexisttime -= Time.deltaTime;
        if (crystalexisttime < 0)
        {
            fininshexplode();
        }
        if (canmove && closettarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closettarget.position, movespeed * Time.deltaTime);
            if(Vector2.Distance(transform.position , closettarget.position)< 2)
            {
                fininshexplode() ;
                canmove = false ;
            }
        }
        if (cangrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growspeed * Time.deltaTime);
        }
    }

    private void animationexplodeevent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                hit.GetComponent<enemy>().damage();
            }
        }
    }

    public void fininshexplode()
    {
        if (canexplode)
        {
            cangrow = true;
            anim.SetTrigger("explode");
        }
        else
        {

            disappearwithtime();
        }
    }

    private void disappearwithtime()
    {
        sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorlosingspeed));
        if (sr.color.a < 0)
        {
            selfdestroy();
        }
    }

    public void selfdestroy() => Destroy(gameObject);
}
