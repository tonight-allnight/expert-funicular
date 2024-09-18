using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerprimaryattackstate : playerstate
{
    public int combocounter { get; private set; }
    private float lasttimeattacked;
    private float combowindow = 0.4f;
    public playerprimaryattackstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
        if(combocounter >2 || Time.time >= lasttimeattacked + combowindow)
        {
            combocounter = 0;
        }
        player.animator.SetInteger("combocounter", combocounter);
        #region ¹¥»÷×ªÏò
        float attackdir = player.facingdir;
        if(xinput != 0)
        {
            attackdir = xinput;
        }
        #endregion
        player.setvelocity(player.attackmovement[combocounter].x * attackdir, player.attackmovement[combocounter].y);
        statetimer = .1f;
    }

    public override void exit()
    {
        base.exit();
        player.StartCoroutine("busyfor", .2f);
        combocounter++;
        lasttimeattacked = Time.time;
    }

    public override void update()
    {
        base.update();
        if(statetimer < 0)
        {
            player.zerovelocity();
        }
        if (triggercalled)
        {
            statemachine.changestate(player.playeridlestate);
        }
        
    }
}
