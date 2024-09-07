using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeraimswordstate : playerstate
{
    public playeraimswordstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }
    public bool isaiming;
    public override void enter()
    {
        base.enter();
        player.skill.sword.Dotsactive(true);
        //isaiming = true;试图取消第一下鼠标会出现瞄准线的bug
    }

    public override void exit()
    {
        //isaiming = false;
        base.exit();
        player.StartCoroutine("busyfor", .2f);
    }

    public override void update()
    {
        base.update();
        player.zerovelocity();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            statemachine.changestate(player.playeridlestate);
        }
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(player.transform.position.x > mouseposition.x && player.facingdir == 1)
        {
            player.flip();
        }
        else if(player.transform.position.x < mouseposition.x && player.facingdir == -1)
        {
            player.flip();
        }
    }
}
