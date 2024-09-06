using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonbattlestate : enemystate
{
    private enemy_skelton enemy;
    private Transform player1;
    private int movedir;

    public skeltonbattlestate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname, enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();
        player1 = playermanager.instance.player.transform;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (enemy.isplayerdetected())
        {
            statetimer = enemy.battletime;
            if (enemy.isplayerdetected().distance < enemy.attackdistance)
            {
                if (canattack())
                    statemachine.changestate(enemy.attackstate);
            }
        }
        else
        {
            if (statetimer < 0 || Vector2.Distance(player1.transform.position , enemy.transform.position)>10)
            {
                statemachine.changestate(enemy.idlestate);
            }
        }

        if(player1.position.x > enemy.transform.position.x)
        {
            movedir = 1;
        }
        else if(player1.position.x < enemy.transform.position.x)
        {
            movedir =  -1;
        }
        enemy.setvelocity(movedir * enemy.movespeed, rb.velocity.y);    
    }
    private bool canattack()
    {
        if(Time.time >= enemy.lasttimeattacked + enemy.attackcooldown)
        {
            enemy.lasttimeattacked = Time.time;
            return true;
        }
        return false;
    }
}
