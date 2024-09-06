using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonattackstate : enemystate
{
    private enemy_skelton enemy;
    public skeltonattackstate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname, enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
        enemy.lasttimeattacked = Time.time;
    }

    public override void update()
    {
        base.update();
        enemy.zerovelocity();
        if (triggercalled)
        {
            statemachine.changestate(enemy.battlestate);
        }
    }
}
