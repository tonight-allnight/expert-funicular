using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovestate : playergroundstate
{
    public playermovestate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        player.setvelocity(xinput * player.movespeed, rb.velocity.y);
        if (xinput == 0)
        {
            statemachine.changestate(player.playeridlestate);
        }
    }
}
