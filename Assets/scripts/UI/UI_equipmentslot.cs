using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class UI_equipmentslot : UI_itemslot
{
    public Equipmentype equipmenttype;
    private void OnValidate()
    {
        gameObject.name =  "ÎäÆ÷¿ò-"+equipmenttype.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        storehouse.instance.Unequipitem(item.data as itemdata_equipment);
        storehouse.instance.additem(item.data as itemdata_equipment);
        clearslot();
    }


}
