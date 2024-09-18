using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class player : entity
{
    // Start is called before the first frame update

    public bool isbusy { get; private set; }//启用之后不可攻击时移动
    

    [Header("attack details")]
    public Vector2[] attackmovement;
    public float counterattackduration = .2f;//防守反击窗口时间；

    [Header("move info")]
    public float movespeed;
    public float jumpforce;
    public float swordreturnimpact;
    private float defaultmovespeed;
    private float defaultjumpforce;

    [Header("dash info")]
    public float dashspeed;
    public float dashduration;
    public float dashdir { get;private set; }
    private float defaultdashspeed;
    public skillmanager skill {  get; private set; }
    public GameObject sword { get; private set; }

    #region 状态
    public playerstatemachine statemachine { get; private set; }
    public playeridlestate playeridlestate { get; private set; }
    public playerjumpstate playerjumpstate { get; private set; }
    public playerairstate playerairstate { get; private set; }
    public playermovestate playermovestate { get; private set; }

    public playerdashstate playerdashstate { get; private set; }

    public playerwallslidestate playerwallslidestate { get; private set; }

    public playerwalljumpstate playerwalljumpstate { get; private set; }

    public playerprimaryattackstate playerprimaryattackstate { get; private set; }
    public counterattackstate counterattack { get; private set; }

    public playeraimswordstate playeraims {  get; private set; }
    public playercatchswordstate Playercatchsword { get; private set; }

    public playerblackholestate playerblackhole { get; private set; }

    public playerdeadstate playerdeadstate { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        statemachine = new playerstatemachine();

        playeridlestate = new playeridlestate(statemachine,this, "idle");
        playermovestate = new playermovestate(statemachine, this, "move");
        playerjumpstate = new playerjumpstate(statemachine, this, "jump");
        playerairstate  = new playerairstate(statemachine, this, "jump");
        playerdashstate = new playerdashstate(statemachine, this, "dash");
        playerwallslidestate = new playerwallslidestate(statemachine, this, "wallslide");
        playerwalljumpstate = new playerwalljumpstate(statemachine, this, "jump");
        playerprimaryattackstate = new playerprimaryattackstate(statemachine, this, "attack");
        counterattack = new counterattackstate(statemachine, this, "defendattack");
        playeraims = new playeraimswordstate(statemachine, this, "aimsword");
        Playercatchsword = new playercatchswordstate(statemachine, this, "catchsword");
        playerblackhole = new playerblackholestate(statemachine, this, "jump");
        playerdeadstate = new playerdeadstate(statemachine, this, "dead");
    }
    protected override void Start()
    {
        base.Start();
        skill = skillmanager.instance;
        statemachine.initialize(playeridlestate);
        defaultmovespeed = movespeed;
        defaultjumpforce = jumpforce;
        defaultdashspeed = dashspeed;
    }
     
    protected override void Update()
    {
        base .Update();
        statemachine.currentstate.update();  

        checkfordashinput();

        if (Input.GetKeyDown(KeyCode.F))
        {
            skill.crystal.canuseskill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            storehouse.instance.useflask();
        }
    }

    public override void slowentityby(float _slowpercentage, float slowduration)
    {
        movespeed = movespeed * (1 - _slowpercentage);
        jumpforce = jumpforce * (1 - _slowpercentage);
        dashspeed = dashspeed * (1 - _slowpercentage);
        animator.speed = animator.speed * (1 - _slowpercentage);
        Invoke("returndefaultspeed" , slowduration);
    }
    protected override void returndefaultspeed()
    {
        base.returndefaultspeed();
        movespeed = defaultmovespeed;
        jumpforce = defaultjumpforce;
        dashspeed = defaultdashspeed;
        //animator.speed = 1;
    }
    public void assignnewsword(GameObject _newsword)
    {
        sword = _newsword;
    }
    public void clearthesword()
    {
        statemachine.changestate(Playercatchsword);
        Destroy(sword);
    }

 

    private void checkfordashinput()
    {

        if (iswalletected() && !isgrounddetected())
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && skillmanager.instance.dash.canuseskill() && !playerblackhole.isblackhole)
        {

            dashdir = Input.GetAxisRaw("Horizontal");
            if (dashdir == 0)
            {
                dashdir = facingdir;
            }
            statemachine.changestate(playerdashstate);
        }
    }
    
    #region 空闲判
    public IEnumerator  busyfor(float _seconds)
    {
        isbusy = true;
        yield return new WaitForSeconds(_seconds);
        isbusy = false;
    }
    #endregion

    public void Animationtrigger() => statemachine.currentstate.AnimationFinishtrigger();


    public override void Die()
    {
        base.Die();
        statemachine.changestate(playerdeadstate);
    }

}
