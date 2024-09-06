using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystatemachine 
{
    public enemystate currentstate {  get; private set; }
    public void initialize(enemystate _startstate)
    {
        currentstate = _startstate;
        currentstate.enter();
    }
    public void changestate(enemystate _newstate)
    {
        currentstate.exit();
        currentstate = _newstate;
        currentstate.enter();
    }
}
