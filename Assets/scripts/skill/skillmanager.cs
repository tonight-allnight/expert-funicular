using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillmanager : MonoBehaviour
{
    public static skillmanager instance;
    public dashskill dash { get; private set; }
    public clone_skill clone { get; private set; }
    private void Awake()
    {
        if(instance != null) 
        { 
            Destroy(instance.gameObject); 
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        dash = GetComponent<dashskill>();
        clone = GetComponent<clone_skill>();
    }
}
