using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhole_skill : skill
{
    [SerializeField] private GameObject blackholeprefab;
    [SerializeField] private float maxsize;
    [SerializeField] private float growspeed;
    [SerializeField] private float shrinkspeed;
    [Space]
    [SerializeField] private int amountofattack = 4;
    [SerializeField] private float clonecooldown = 2;
    [SerializeField] private float blackholeduration;

    blackhole_skill_controller currentblackhole;
    public override bool canuseskill()
    {
        return base.canuseskill();
    }

    public override void Useskill()
    {
        base.Useskill();
        GameObject newblackhole = Instantiate(blackholeprefab , player.transform.position , Quaternion.identity);
        currentblackhole = newblackhole.GetComponent<blackhole_skill_controller>();
        currentblackhole.setupblackhole(maxsize , growspeed , shrinkspeed , amountofattack , clonecooldown,blackholeduration );
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    public bool skillcompleted()
    {
        if(!currentblackhole)
            return false;
        if (currentblackhole.playercanexitstate)
        {
            currentblackhole = null;
            return true;
        }
        return false;
    }
    
}
