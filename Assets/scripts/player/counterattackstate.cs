using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterattackstate : playerstate
{
    public counterattackstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void enter()
    {
        base.enter();
        statetimer = player.counterattackduration;
        player.animator.SetBool("successfulcounterattack", false);
    }

    public override void exit()
    {
        player.animator.SetBool("successfulcounterattack", false);
        base.exit();
        
    }

    public override void update()
    {
        base.update();

        player.zerovelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackcheck.position, player.attackcheckradius);

        foreach (var hit in colliders)//检测敌人是否处于可击晕状态
        {
            if(hit.GetComponent<enemy>() != null)
            {

                if (hit.GetComponent<enemy>().Canbestunned() )
                {
                    statetimer = 10;
                    player.animator.SetBool("successfulcounterattack", true);
                }

            }
        }
        if (statetimer < 0 || triggercalled)
        {
            statemachine.changestate(player.playeridlestate);
        }

    }
}
