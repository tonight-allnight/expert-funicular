using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdeadstate : playerstate
{
    private player player;
    public playerdeadstate(playerstatemachine _statemachine, player _player, string _animboolname) : base(_statemachine, _player, _animboolname)
    {
    }

    public override void AnimationFinishtrigger()
    {
        base.AnimationFinishtrigger();
    }

    public override void enter()
    {
        base.enter();
        //player.zerovelocity();

    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        
    }

    
}
