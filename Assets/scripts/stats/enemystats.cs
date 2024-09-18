using Assets.scripts.item_and_store;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystats : characterstates
{
    private enemy enemy;
    private itemdrop mydrop;

    [Header("µÈ¼¶Ï¸½Ú")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentagemodifier = .4f;
    protected override void Start()
    {
        applylevelmodifiers ();
        base.Start();
        enemy = GetComponent<enemy>();
        mydrop = GetComponent<itemdrop>();
    }

    private void applylevelmodifiers()
    {
        modify(strength);
        modify(agility);
        modify(intelligence);
        modify(vitality);


        modify(damage);
        modify(critchance);
        modify(critpower);

        modify(maxHp);
        modify(armor);
        modify(evasion);
        modify(magicresistance);

        modify(firedamage);
        modify(icedamage);
        modify(lightingdamage);
    }

    private void modify(stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.getvalue() * percentagemodifier;
            _stat.addmodifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);
    }
    protected override void die()
    {
        base.die();
        enemy.Die();
        mydrop.generateDrop();
    }
}
