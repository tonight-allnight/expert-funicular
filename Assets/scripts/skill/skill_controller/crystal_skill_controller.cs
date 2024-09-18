using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class crystal_skill_controller : MonoBehaviour
{
    [SerializeField] private float colorlosingspeed;
    private player player;
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

    [SerializeField] private LayerMask whatisenemy;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void setupcrystal(float _crystalduration , bool _canexplode, bool _canmove, float _movespeed , Transform _closettarget ,player _player)
    {
        player = _player;
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
            if(Vector2.Distance(transform.position , closettarget.position)< .5f)
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

    public void chooserandomenemy()
    {
        float radius = skillmanager.instance.blackhole.getblackholeradius();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius , whatisenemy);
        if(colliders.Length > 0)
            closettarget = colliders[Random.Range(0, colliders.Length)].transform;
    }

    private void animationexplodeevent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                player.states.DoMagicdamage(hit.GetComponent<characterstates>());
                itemdata_equipment equipment = storehouse.instance.getequipment(Equipmentype.amulet);
                if(equipment.itemtype != Itemtype.material)
                {
                    equipment.excuteitemeffect(hit.transform);
                }
                else
                {
                    return;
                }
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
