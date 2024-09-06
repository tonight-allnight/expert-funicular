using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeridlestate : playergroundstate
{
    public playeridlestate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
        player.zerovelocity();//Ó°Ïì»¬ÐÐµÄÅÐ¶Ï
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        if (xinput != 0 && !player.isbusy)
        {
            statemachine.changestate(player.playermovestate);
        }
        //player.zerovelocity();
    }
}
