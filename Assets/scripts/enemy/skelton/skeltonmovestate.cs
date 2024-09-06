using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonmovestate : skeltonongroundedstate
{
    public skeltonmovestate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname, enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname, _enemy)
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
        enemy.setvelocity( enemy.movespeed * enemy.facingdir , rb.velocity.y);
        if(enemy.iswalletected() || !enemy.isgrounddetected())
        {
            enemy.flip();
            statemachine.changestate(enemy.idlestate);
        }
        
    }
}
