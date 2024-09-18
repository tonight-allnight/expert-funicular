using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class crystal_skill : skill
{
    [Header("crystal mirage")]
    [SerializeField] private bool cloneinsteadofcrystal;

    [SerializeField] private float crystalduration;
    [Header("explode cystal")]
    [SerializeField] private bool canexplode;

    [Header("move cystal")]
    [SerializeField] private float movespeed;
    [SerializeField] private bool canmove;

    [SerializeField] private GameObject crystalprefab;
    private GameObject currentcrystal;




    [Header("multi stacking crystal")]
    [SerializeField] private bool canusemultistacks;
    [SerializeField] private int amountostacks;
    [SerializeField] private float multicooldown;
    [SerializeField] private float usetimewindow;
    [SerializeField] public List<GameObject> crystalleft = new List<GameObject>();


    public override bool canuseskill()
    {
        return base.canuseskill();
    }

    public override void Useskill()
    {
        base.Useskill();
        if (canusemulti())
        {
            return;
        }
        if(currentcrystal == null)//没有则创建
        {
            createclonecrystal();
            //Debug.Log(findclosestenemy(currentcrystal.transform));
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

            if (cloneinsteadofcrystal)
            {
                skillmanager.instance.clone.createclone(currentcrystal.transform,player.facingdir,Vector3.zero);
                Destroy(currentcrystal);
            }
            else
            {
                currentcrystal.GetComponent<crystal_skill_controller>().fininshexplode();

            }

        }
    }

    public void createclonecrystal()
    {
        currentcrystal = Instantiate(crystalprefab, player.transform.position, Quaternion.identity);
        crystal_skill_controller crystalscript = currentcrystal.GetComponent<crystal_skill_controller>();
        crystalscript.setupcrystal(crystalduration, canexplode, canmove, movespeed, findclosestenemy(currentcrystal.transform),player);
        crystalscript.chooserandomenemy();
    }

    public void currentcrystalchooserandomtarget() => currentcrystal.GetComponent<crystal_skill_controller>().chooserandomenemy();

    private bool canusemulti()
    {
        if (canusemultistacks)
        {
            if (crystalleft.Count > 0)
            {
                if(crystalleft.Count == amountostacks)
                {
                    Invoke("resetability" , usetimewindow);
                }

                cooldown = 0;
                GameObject crystaltospan = crystalleft[crystalleft.Count - 1];
                GameObject newcrystal = Instantiate(crystaltospan, player.transform.position, Quaternion.identity);
                crystalleft.Remove(crystaltospan);
                newcrystal.GetComponent<crystal_skill_controller>().
                    setupcrystal(crystalduration, canexplode, canmove, movespeed, findclosestenemy(newcrystal.transform), player);
                if(crystalleft.Count <= 0)
                {
                    cooldown = multicooldown;
                    refillcrystal();
                }
                return true;
            }
        }
        return false;
    }
    private void refillcrystal()
    {
        int amountofadd = amountostacks - crystalleft.Count;
        for (int i = 0; i < amountofadd; i++)
        {
            crystalleft.Add(crystalprefab);
        }
    }
    private void resetability()
    {
        if(cooldowntimer > 0)
        {
            return;
        }
        cooldowntimer = multicooldown;
        refillcrystal() ;
    }
}
