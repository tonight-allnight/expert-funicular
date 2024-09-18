using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonstunnedstate : enemystate
{
    private enemy_skelton enemy;
    public skeltonstunnedstate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname, enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();
        enemy.fx.InvokeRepeating("redcolorblink",0,.1f);
        statetimer = enemy.stunnedduration;
        rb.velocity  = new Vector2(-enemy.facingdir * enemy.stunneddir.x, enemy.stunneddir.y);
    }

    public override void exit()
    {
        base.exit();
        enemy.fx.Invoke("cannelcolorchange", 0);
    }

    public override void update()
    {
        base.update();
        if(statetimer < 0)
        {
            statemachine.changestate(enemy.idlestate);
        }
    }
}
