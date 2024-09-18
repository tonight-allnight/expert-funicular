using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class characterstates : MonoBehaviour
{
    #region ����
    private entityfx fx;
    //public int damage;
    [Header("��Ҫ״̬")]
    public stat strength;//1+1�˺�+1��������
    public stat agility;//1 + 1%���ܼ��޵л���1% ����޵�֡
    public stat intelligence;//1+ 1ħ���˺� 3ħ������
    public stat vitality;// ������

    [Header("��������")]
    public stat maxHp;//�������
    public stat armor;//����
    public stat evasion;//
    public stat magicresistance; //����

    [Header("��������")]
    public stat damage;
    public stat critchance;//��������
    public stat critpower; //�����˺� 130%

    [Header("ħ���˺�")]
    public stat firedamage;
    public stat icedamage;
    public stat lightingdamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

    [SerializeField] private float ailmentsduration = 4;

    [Header("״̬����ʱ��")]
    private float ignitedtimer;
    private float chilltimer;
    private float shocktimer;


    private float ignitedamagecooldown = .3f;
    private float ignitedamagetimer;
    private int ignitedamage;
    private int shockdamage;
    [SerializeField] private GameObject shockstrikeprefab;


    public int currentHp;

    public System.Action onHealthed;
    public bool isdead { get; private set; }

    #endregion
    protected virtual void Start()
    {
        critpower.setdefaultvalue(130);
        currentHp = getmaxHP();
        fx = GetComponent<entityfx>();
        //damage.addmodifier(4);
    }

    protected virtual void Update()
    {
        ignitedtimer -= Time.deltaTime;
        chilltimer -= Time.deltaTime;
        shocktimer -= Time.deltaTime;

        ignitedamagetimer -= Time.deltaTime;

        if (ignitedtimer < 0)
        {
            isIgnited = false;
        }
        if (chilltimer < 0)
        {
            isChilled = false;
        }
        if (shocktimer < 0)
        {
            isShocked = false;
        }
        if(isIgnited)
            applyigniteddamage();
    }

    public virtual void increasestatby(int _modifier , float _duration, stat _stattomodify)
    {
        StartCoroutine(statmodcorroutine(_modifier, _duration, _stattomodify));
    }
    private IEnumerator statmodcorroutine(int modifier, float _duration, stat _stattomodify)
    {
        _stattomodify.addmodifier(modifier);
        yield return new WaitForSeconds(_duration);
        _stattomodify.removemodifier(modifier);
    }

    public virtual void increasehealth(int _amount)
    {
        currentHp += _amount;
        if(currentHp > getmaxHP())
        {
            currentHp = getmaxHP();
        }
        if(onHealthed != null)
        {
            onHealthed();
        }

    }

    #region ������ħ������
    public virtual void DoDamage(characterstates _targetstats)
    {
        if (canmiss(_targetstats))
        {
            return;
        }

        int totaldamage = damage.getvalue() + strength.getvalue();

        if (cancrit())
        {
            totaldamage = calculatecriticaldamage(totaldamage);
        }
        totaldamage = checkarmor(_targetstats, totaldamage);
        _targetstats.takedamage(totaldamage);
        DoMagicdamage(_targetstats);
    }

    public virtual void DoMagicdamage(characterstates _taregtstats)
    {
        int fire = firedamage.getvalue();
        int ice = icedamage.getvalue();
        int light = lightingdamage.getvalue();

        int totalmagic;
        totalmagic = fire + ice + light + intelligence.getvalue();
        totalmagic = checkmagicresistance(_taregtstats, totalmagic);
        _taregtstats.takedamage(totalmagic);

        if (Mathf.Max(fire, ice, light) <= 0)
        {
            return;
        }
        toappltaliments(_taregtstats, fire, ice, light);
    }

    private void toappltaliments(characterstates _taregtstats, int fire, int ice, int light)
    {
        bool canfire = fire > ice && fire > light;
        bool canchill = ice > fire && ice > light;
        bool canshock = light > fire && light > fire;
        while (!canfire && !canchill && !canshock)
        {
            if (Random.value < .3333f && fire > 0)
            {
                canfire = true;
                _taregtstats.Applyaliments(canfire, canchill, canshock);
                return;
            }
            if (Random.value < .5f && ice > 0)
            {
                canchill = true;
                _taregtstats.Applyaliments(canfire, canchill, canshock);
                return;
            }
            if (Random.value < .88f && light > 0)
            {
                canshock = true;
                _taregtstats.Applyaliments(canfire, canchill, canshock);
                return;
            }
        }
        if (canfire)
        {
            _taregtstats.setupignitedamage(Mathf.RoundToInt(fire * .15f));
        }
        if (canshock)
        {
            _taregtstats.setupshockdamage(Mathf.RoundToInt(light * .15f));
        }

        _taregtstats.Applyaliments(canfire, canchill, canshock);
    }

    

    public void Applyaliments(bool _ignite , bool _chill , bool _shock)
    {
        bool canapplyignite = !isIgnited && !isChilled && !isShocked;
        bool canapplychill = !isIgnited && !isChilled && !isShocked;
        bool canapplyshock = !isIgnited && !isChilled;
        if (_ignite && canapplyignite)
        {
            isIgnited = _ignite;
            ignitedtimer = 3;

            fx.ignitedfxfor(3);
        }
        if (_chill && canapplychill)
        {
            isChilled = _chill;
            chilltimer = 2;
            float slowpercentage = .2f;
            GetComponent<entity>().slowentityby(slowpercentage,chilltimer);
            fx.chillededfxfor(2);
        }
        if (_shock && canapplyshock)
        {
            if (!isShocked)
            {
                applyshock(_shock);
            }
            else
            {
                if (GetComponent<player>() != null)//����
                    return;

                //�ҵ�������˲����м䴫��,��ʼ���׵����������
                hitnearstenemywiththunder();

            }

        }

    }

    public void applyshock(bool _shock)
    {
        if (isShocked)
            return;
        isShocked = _shock;
        shocktimer = 3;
        fx.shockedfxfor(3);
    }

    private void hitnearstenemywiththunder()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);
        float closestdistance = Mathf.Infinity;
        Transform closetenemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanttoenemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanttoenemy < closestdistance)
                {
                    closestdistance = distanttoenemy;
                    closetenemy = hit.transform;

                }
            }
            if (closetenemy == null)
            {
                closetenemy = transform;
            }
        }
        if (closetenemy != null)
        {
            GameObject newshockstrike = Instantiate(shockstrikeprefab, transform.position, Quaternion.identity);
            newshockstrike.GetComponent<thunderstrike_controller>().setup(shockdamage, closetenemy.GetComponent<characterstates>());

        }
    }
    private void applyigniteddamage()
    {
        if (ignitedamagetimer < 0)
        {
            //Debug.Log("�����˺�" + ignitedamage);
            decreasehp(ignitedamage);
            if (currentHp <= 0 && !isdead)
            {
               
                die();
            }
            ignitedamagetimer = ignitedamagecooldown;
        }
    }

    public void setupignitedamage(int _damage) => ignitedamage = _damage;
    public void setupshockdamage(int _damage) => shockdamage = _damage;
    #endregion

    public virtual void takedamage(int _damage)
    {
        decreasehp(_damage);
        GetComponent<entity>().damageEffect();
        fx.StartCoroutine("flashfx");
        if (currentHp < 0 && !isdead)
        {
            die();
        }
    }

    

    protected virtual void die()
    {
        isdead = true;
    }
    #region ״̬��ֵ����
    private bool canmiss(characterstates _targetstats)
    {
        int totalevasion = _targetstats.evasion.getvalue() + _targetstats.agility.getvalue();
        if (isShocked)
        {
            totalevasion += 20;
        }

        if (Random.Range(0, 100) < totalevasion)
        {
            Debug.Log("������ܳɹ�");
            return true;

        }
        return false;
    }
    protected virtual void decreasehp(int _damage)
    {
        currentHp -= _damage;
        if (onHealthed != null)
        {
            onHealthed();
        }
    }
    private int checkarmor(characterstates _targetstats, int totaldamage)
    {
        if (_targetstats.isChilled)
        {
            totaldamage -= Mathf.RoundToInt(_targetstats.armor.getvalue() * .8f);
        }
        else
        {
            totaldamage -= _targetstats.armor.getvalue();//��ȥ�����˺�
        }
        totaldamage = Mathf.Clamp(totaldamage, 0, int.MaxValue);
        return totaldamage;
    }
    private int checkmagicresistance(characterstates _taregtstats, int totalmagic)
    {
        totalmagic -= _taregtstats.magicresistance.getvalue() + (_taregtstats.intelligence.getvalue() * 3);
        totalmagic = Mathf.Clamp(totalmagic, 0, int.MaxValue);
        return totalmagic;
    }

    private bool cancrit()
    {
        int totalcritcalchance = critchance.getvalue() + agility.getvalue();
        if(Random.Range(0,100) <= totalcritcalchance)
        {
            return true;
        }
        return false;
    }

    private int calculatecriticaldamage(int _damage)
    {
        float totalcritpower = (critpower.getvalue() + strength.getvalue()) * 0.1f;

        float critdamage = _damage * totalcritpower;

        return Mathf.RoundToInt(critdamage);
    }

    public int getmaxHP()
    {
        return maxHp.getvalue() + vitality.getvalue() * 5;
    }
    #endregion
}
