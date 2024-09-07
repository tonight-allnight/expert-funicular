using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class playerdashstate : playerstate
{
    public playerdashstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
        //Debug.Log(player.transform);
        player.skill.clone.createclone(player.transform , player.facingdir , Vector2.zero);
        statetimer = player.dashduration;
    }

    public override void exit()
    {
        base.exit();
        player.setvelocity(0, rb.velocity.y);
    }

    public override void update()
    {
        //Debug.Log("»¦Ò¯³å»÷");
        base.update();
        if(!player.isgrounddetected() && player.iswalletected())
        {
            statemachine.changestate(player.playerwallslidestate);
        }
        

        player.setvelocity(player.dashspeed * player.dashdir, 0);

        if(statetimer < 0)
        {
            statemachine.changestate(player.playeridlestate);
        }
    }
}
