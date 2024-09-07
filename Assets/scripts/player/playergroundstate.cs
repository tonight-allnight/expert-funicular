using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playergroundstate : playerstate
{
    public playergroundstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            statemachine.changestate(player.playerblackhole);
        }

        if (Input.GetKeyDown(KeyCode.Q) && hasnosword())
        {
            statemachine.changestate(player.playeraims);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            statemachine.changestate(player.counterattack);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            statemachine.changestate(player.playerprimaryattackstate);
        }
        if (!player.isgrounddetected())
        {
            statemachine.changestate(player.playerairstate);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && player.isgrounddetected())
        {
            statemachine.changestate(player.playerjumpstate);
        }

    }
    private bool hasnosword()//检查有无多的jian
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<sword_skill_controller>().returnsword();
        return false;
    }

}
