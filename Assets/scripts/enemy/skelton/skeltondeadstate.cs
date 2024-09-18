using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class skeltondeadstate : enemystate
{
    private enemy_skelton enemy;
    public skeltondeadstate(enemystatemachine _statemachine, enemy _enemybase, string _animboolname , enemy_skelton _enemy) : base(_statemachine, _enemybase, _animboolname)
    {
        this.enemy = _enemy;
    }

    public override void animationfinishtrigger()
    {
        base.animationfinishtrigger();
    }

    public override void enter()
    {
        base.enter();
        statetimer = 2;

        //enemy.animator.speed = 0;
        /*enemy.animator.SetBool(enemy.lastanimboolname,true);
        enemy.cd.enabled = false;

        statetimer = .1f; 针对没有死亡动画的敌人*/
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        if(statetimer<0)
            enemy.gameObject.SetActive(false);
       
    }
}
