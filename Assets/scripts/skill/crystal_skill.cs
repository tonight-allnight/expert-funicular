using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class crystal_skill : skill
{
    [SerializeField] private float crystalduration;
    [Header("explode cystal")]
    [SerializeField] private bool canexplode;

    [Header("move cystal")]
    [SerializeField] private float movespeed;
    [SerializeField] private bool canmove;

    [SerializeField] private GameObject crystalprefab;
    private GameObject currentcrystal;

    public override bool canuseskill()
    {
        return base.canuseskill();
    }

    public override void Useskill()
    {
        base.Useskill();
        if(currentcrystal == null)//没有则创建
        {
            currentcrystal = Instantiate(crystalprefab , player.transform.position, Quaternion.identity);
            crystal_skill_controller crystalscript = currentcrystal.GetComponent<crystal_skill_controller>();
            crystalscript.setupcrystal(crystalduration , canexplode ,canmove , movespeed , findclosestenemy(currentcrystal.transform));
            Debug.Log(findclosestenemy(currentcrystal.transform));
        }
        else//有则进行状态判断
        {
            if (canmove && findclosestenemy(currentcrystal.transform) != null)
            {
                return; 
            }

            Vector2 playerpos = player.transform.position; 
            player.transform.position = currentcrystal.transform.position;

            currentcrystal.transform.position = playerpos;
            currentcrystal.GetComponent<crystal_skill_controller>().fininshexplode();
        }
    }

    
}
