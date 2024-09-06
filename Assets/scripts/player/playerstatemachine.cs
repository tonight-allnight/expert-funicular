using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstatemachine 
{
    public playerstate currentstate{ get; private set; }

    public void initialize(playerstate _startstate)
    {
        currentstate = _startstate;
        currentstate.enter();
    }
    public void changestate(playerstate _newstate)
    {
        currentstate.exit();
        currentstate = _newstate;
        currentstate.enter();
    }
    
}
