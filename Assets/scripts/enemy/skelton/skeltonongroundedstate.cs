using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonongroundedstate : enemystate
{
    protected enemy_skelton enemy;

    protected Transform player;
    public skeltonongroundedstate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname, enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname)
    {
        this.enemy = _enemy;

    }

    public override void enter()
    {
        base.enter();
        player = playermanager.instance.player.transform;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        if (enemy.isplayerdetected() || Vector2.Distance(enemy.transform.position , player.transform.position) < 2)
        {
            statemachine.changestate(enemy.battlestate);
        }
    }

    
}
