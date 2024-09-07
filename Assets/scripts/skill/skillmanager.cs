using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillmanager : MonoBehaviour
{
    public static skillmanager instance;
    public dashskill dash { get; private set; }
    public clone_skill clone { get; private set; }
    public sword_skill sword { get; private set; }
    public blackhole_skill blackhole { get; private set; }
    public crystal_skill crystal { get; private set; }
    private void Awake()
    {
        sword = GetComponent<sword_skill>();
        if (instance != null) 
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
        sword = GetComponent<sword_skill>();
        blackhole   = GetComponent<blackhole_skill>();
        crystal = GetComponent<crystal_skill>();
    }
}
