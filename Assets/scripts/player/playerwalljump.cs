using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerwalljumpstate : playerstate
{
    public playerwalljumpstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
        statetimer = 1f;
        player.setvelocity(5 * -player.facingdir, player.jumpforce);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        if(statetimer < 0)
        {
            statemachine.changestate(player.playerairstate);
        }
        if (player.isgrounddetected())
        {
            statemachine.changestate(player.playeridlestate);
        }
        if (!player.iswalletected())
        {
            statemachine.changestate(player.playerjumpstate);
        }
        
    }

    
}
