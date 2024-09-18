using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystate
{
    protected enemystatemachine statemachine;
    protected enemy enemybase;
    protected bool triggercalled;
    protected float statetimer;
    private string animboolname;
    protected Rigidbody2D rb;

    public enemystate(enemystatemachine _statemachine, enemy _enemybase,string _animboolname)
    {
        this.statemachine = _statemachine;
        this.enemybase = _enemybase;
        this.animboolname = _animboolname;
    }

    public virtual void enter()
    {
        triggercalled = false;
        enemybase.animator.SetBool(animboolname, true);
        rb = enemybase.rb;
    }
    public virtual void exit()
    {
        enemybase.animator.SetBool(animboolname, false);
        enemybase.assignlastanimname(animboolname);
    }
    public virtual void update()
    {
        statetimer -= Time.deltaTime;
    }

    public virtual void animationfinishtrigger()
    {
        triggercalled = true;
        
    }
}
