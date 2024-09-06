using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerjumpstate : playerstate
{
    public playerjumpstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpforce);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
         if (rb.velocity.y < 0)
        {
            statemachine.changestate(player.playerairstate);
        }
    }
}
