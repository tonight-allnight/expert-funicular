using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerairstate : playerstate
{
    public playerairstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
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

        if (player.iswalletected())
        {
            statemachine.changestate(player.playerwallslidestate);
        }
        if(player.isgrounddetected())
        {
            player.setvelocity(0, rb.velocity.y);
            statemachine.changestate(player.playeridlestate);
        }
        if(xinput != 0)
        {
            player.setvelocity(player.movespeed * .8f * xinput, rb.velocity.y);
        }
        
    }
}
