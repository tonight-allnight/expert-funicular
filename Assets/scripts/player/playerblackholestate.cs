using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerblackholestate : playerstate
{
    private float flytime = .4f;
    private bool skillused;
    private float defaultgravity;
    public bool isblackhole ;
    public playerblackholestate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void AnimationFinishtrigger()
    {
        base.AnimationFinishtrigger();
    }

    public override void enter()
    {
        base.enter();
        defaultgravity = rb.gravityScale;
        skillused = false;
        statetimer = flytime;
        rb.gravityScale = 0;
        isblackhole = true;
    }

    public override void exit()
    {
        base.exit();
        rb.gravityScale = defaultgravity;
        player.fx.maketransparent(false);
        isblackhole = false;
    }

    public override void update()
    {
        base.update();
        if (statetimer > 0)
        {
            rb.velocity = new Vector2(0, 15);
        }
        if(statetimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);
            if (!skillused)
            {
                if(player.skill.blackhole.canuseskill())
                    skillused = true;
            }
        }
        if (player.skill.blackhole.skillcompleted())
        {
            statemachine.changestate(player.playerairstate);
        }
    }
}
