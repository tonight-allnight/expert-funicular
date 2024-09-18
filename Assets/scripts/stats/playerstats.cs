using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstats : characterstates
{
    private player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<player>();
    }

    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);
    }
    protected override void die()
    {
        base.die();
        player.Die();
        GetComponent<playeritemdrop>()?.generateDrop();
    }
    protected override void decreasehp(int _damage)
    {
        base.decreasehp(_damage);
        itemdata_equipment armor = storehouse.instance.getequipment(Equipmentype.Aromr);
        if (armor.itemtype != Itemtype.material)
        {
            //Debug.Log(armor.equipmentype);
            armor.excuteitemeffect(player.transform);
        }
        else
        {
            return;
        }
    }
}
