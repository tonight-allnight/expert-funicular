using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_skill_controller : skill
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorlosingspeed;
    [SerializeField] private Transform attackcheck;
    [SerializeField] private float attackcheckradius = .8f;
    [SerializeField] private float checkdistance;
    private float clonetimer;
    private Transform closetenemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        clonetimer -= Time.deltaTime;
        if(clonetimer < 0)
        {
            sr.color = new Color(1,1,1,sr.color.a - (Time.deltaTime* colorlosingspeed));
            if(sr.color.a < 0) 
            {
                Destroy(gameObject);
            }
        }
    }
    public void setupclone(Transform _newtransform , float _cloneduration ,float _facingdir ,bool _canattack ,bool _canchangedir)
    {
        
        transform.position = _newtransform.position;

        clonetimer = _cloneduration;
        bool _can = !_canchangedir;
        Debug.Log(_can);
        if (_canattack)//残影攻击技能
            anim.SetInteger("attacknumber", Random.Range(1, 3));
        if (_can)//残影朝向敌人能力，攻击技能分支，未解决敌人过远方向未改变的问题
        {
            faceclosestenemy(ref _can);

        }
        else//未开启时朝向移动方向 未开启技能时状态
            changeface(_facingdir);
    }
    private void Animationtrigger()
    {
        clonetimer = -1f;
    }

    private void attacktrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackcheck.position, attackcheckradius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null)
            {
                hit.GetComponent<enemy>().damage();
            }
        }
    }
    private void faceclosestenemy(ref bool can)//朝向最近的敌人
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,25);
        float closestdistance = Mathf.Infinity;
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<enemy>() != null)
            {
                float distanttoenemy = Vector2.Distance(transform.position , hit.transform.position);
                if(distanttoenemy  < closestdistance)
                {
                    closestdistance = distanttoenemy;
                    closetenemy = hit.transform;
                    Debug.Log(closestdistance);
                }
            }
        }
        if(closetenemy != null)
        {
            if(transform.position.x > closetenemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            can = false;
        }

        
    }
    private void changeface( float face)
    {
        if(face != 1)
        {
            transform.Rotate(0, 180, 0);
        }
    }

}
