using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_craftslot : UI_itemslot//������Ʒ
{
    private void OnEnable()
    {
        updateslot(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        itemdata_equipment craftdata = item.data as itemdata_equipment;
        if (storehouse.instance.cancraft(craftdata, craftdata.craftingmaterials))
        {
            //������Ч
            return;
        }
    }
}
