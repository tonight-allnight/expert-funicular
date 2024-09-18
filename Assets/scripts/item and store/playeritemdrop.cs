using Assets.scripts.item_and_store;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeritemdrop : itemdrop
{
    [Header("Íæ¼ÒµôÂä")]
    [SerializeField] private float chancetoloseitem;
    [SerializeField] private float chancetolosematerials;

    public override void generateDrop()
    {
        storehouse store = storehouse.instance; 
        List<storehouseitem> itemstounequip = new List<storehouseitem>();
        List<storehouseitem> stashtounequip = new List<storehouseitem>();
        foreach(storehouseitem item in store.getequipment())
        {
            if(Random.Range(0,100) < chancetoloseitem)
            {
                dropitem(item.data);
                itemstounequip.Add(item);
            }
        }
        for(int i = 0;i < itemstounequip.Count; i++)
        {

            store.Unequipitem(itemstounequip[i].data as itemdata_equipment);
        }
        foreach (storehouseitem item in store.getstash())
        {
            if (Random.Range(0, 100) < chancetolosematerials)
            {
                dropitem(item.data);
                stashtounequip.Add(item);
            }
        }
        for (int i = 0; i < stashtounequip.Count; i++)
        {

            store.removeitem(stashtounequip[i].data);
        }
    }

}
