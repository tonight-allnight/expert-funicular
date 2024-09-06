using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonidlestate : skeltonongroundedstate
{
    public skeltonidlestate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname, enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname, _enemy)
    {
    }

    public override void enter()
    {
        base.enter();
        statetimer = enemy.idletime;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        if(statetimer < 0 )
        {
            statemachine.changestate(enemy.movestate);
        }
    }
}
