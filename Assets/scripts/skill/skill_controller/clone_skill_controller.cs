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
    protected override void Update()
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
    public void setupclone(Transform _newtransform , float _cloneduration ,float _facingdir ,bool _canattack ,bool _canchangedir , Vector3 _offset ,Transform _clostenemy = null)
    {
        
        transform.position = _newtransform.position + _offset;
        Transform xx = _newtransform;
        clonetimer = _cloneduration;
        closetenemy = _clostenemy;
        //Debug.Log( "�������������"+ _clostenemy);
        bool _can = _canchangedir;
        //Debug.Log(_can);
        if (_canattack)//��Ӱ��������
            anim.SetInteger("attacknumber", Random.Range(1, 3));
        if (_can && findclosestenemy(xx) != null)//��Ӱ��������������������ܷ�֧��δ������˹�Զ����δ�ı������
        {
            faceclosestenemy(_can);

        }
        else//δ����ʱ�����ƶ����� δ��������ʱ״̬
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
    private void faceclosestenemy( bool can)//��������ĵ���
    {
       
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
    private void changeface(float face)
    {
        if(face != 1)
        {
            transform.Rotate(0, 180, 0);
        }
    }

}
