using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_skelton : enemy
{
    #region states
    public skeltonidlestate idlestate {  get; private set; }
    public skeltonmovestate movestate { get; private set; }

    public skeltonbattlestate battlestate { get; private set; }

    public skeltonattackstate attackstate { get; private set; }

    public skeltonstunnedstate stunnedstate { get; private set; }
    public skeltondeadstate deadstate { get; private set; }
    #endregion 

    private float deadtimer;
    protected override void Awake()
    {
        base.Awake();
        idlestate = new skeltonidlestate(statemachine, this, "idle", this);
        movestate = new skeltonmovestate(statemachine, this, "move", this);
        battlestate = new skeltonbattlestate(statemachine , this , "move" , this);
        attackstate = new skeltonattackstate(statemachine ,this, "attack" , this);
        stunnedstate = new skeltonstunnedstate(statemachine, this, "stunned", this);
        deadstate = new skeltondeadstate(statemachine, this, "dead", this);
    }

    protected override void Start()
    {
        base.Start();
        statemachine.initialize(idlestate);
    }

    protected override void Update()
    {
        base.Update();
        deadtimer -= Time.deltaTime;
    }
    public override bool Canbestunned()
    {
        if (base.Canbestunned())
        {
            statemachine.changestate(stunnedstate);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        statemachine.changestate(deadstate);
        base.Die();
    }
}
