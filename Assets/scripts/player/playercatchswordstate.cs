using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class playercatchswordstate : playerstate
{
    private Transform sword;
    public playercatchswordstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
        sword = player.sword.transform;
        if (player.transform.position.x > sword.position.x && player.facingdir == 1)
        {
            player.flip();
        }
        else if (player.transform.position.x < sword.position.x && player.facingdir == -1)
        {
            player.flip();
        }

        rb.velocity = new Vector2(player.swordreturnimpact * -player.facingdir, rb.velocity.y);
    }

    public override void exit()
    {
        base.exit();
        player.StartCoroutine("busyfor", .1f);
    }

    public override void update()
    {
        base.update();
        if (triggercalled)
        {
            statemachine.changestate(player.playeridlestate);
        }
    }

    
}
