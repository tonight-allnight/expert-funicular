using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstate
{
    protected playerstatemachine statemachine;
    protected player player;

    protected Rigidbody2D rb;
    protected float xinput;
    protected float yinput;
    protected string animboolname;

    protected float statetimer; //×´Ì¬ÀäÈ´¼ÆÊ±Æ÷
    protected bool triggercalled;

    public playerstate(playerstatemachine _statemachine, player _player, string _animboolname)
    {
        this.statemachine = _statemachine;
        this.player = _player;
        this.animboolname = _animboolname;
    }

    public virtual void enter()
    {
        player.animator.SetBool(animboolname, true);
        rb = player.rb;
        triggercalled = false;
    }

    public virtual void update()
    {
        statetimer -= Time.deltaTime;
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yvelocity", rb.velocity.y);
    }
    public virtual void exit()
    {
        player.animator.SetBool(animboolname, false);
    }
    public virtual void AnimationFinishtrigger()
    {
        triggercalled = true;
    }

}
 