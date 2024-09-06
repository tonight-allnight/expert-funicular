using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerwallslidestate : playerstate
{
    public playerwallslidestate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            statemachine.changestate(player.playerwalljumpstate);
            return;
        }

        if(xinput != 0 && xinput != player.facingdir)
        {
            statemachine.changestate(player.playeridlestate);
            Debug.Log("Œ“‘⁄idle");
        }

        if(xinput == player.facingdir)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);
        }
        if (player.isgrounddetected())
        {
            statemachine.changestate(player.playeridlestate);

        }
    }
}
