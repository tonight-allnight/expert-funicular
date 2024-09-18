using System.Collections.Generic;
using UnityEngine;

public class storehouse : MonoBehaviour
{
    public static storehouse instance;
    public List<itemdata> startingequipment;


    public List<storehouseitem> equipment;//创建装备并实体化在人物中
    public Dictionary<itemdata_equipment, storehouseitem> equipmentdictionary;

    public List<storehouseitem> storehouseitems;//创建武器列表
    public Dictionary<itemdata, storehouseitem> storedictionary;

    public List<storehouseitem> stashitems;//创建材料列表
    public Dictionary<itemdata, storehouseitem> stashdictionary;

    [Header("物品冷却")]
    private float lasttimeuseflask;
    private float lasttimeusearmor;

    private float flaskcooldown;
    private float armorcooldown;

    [Header("仓库UI")]
    [SerializeField] private Transform storeslotparent;
    [SerializeField] private Transform stashslotparent;
    [SerializeField] private Transform equipmentslotparent;
    private UI_itemslot[] storehouseitemslots;
    private UI_itemslot[] stashitemslots;
    private UI_equipmentslot[] equipmentslots;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    private void Start()
    {
        storehouseitems = new List<storehouseitem>();
        storedictionary = new Dictionary<itemdata, storehouseitem>();
        stashitems = new List<storehouseitem>();
        stashdictionary = new Dictionary<itemdata, storehouseitem>();
        equipment = new List<storehouseitem>();
        equipmentdictionary = new Dictionary<itemdata_equipment, storehouseitem>();
        storehouseitemslots = storeslotparent.GetComponentsInChildren<UI_itemslot>();
        stashitemslots = stashslotparent.GetComponentsInChildren<UI_itemslot>();
        equipmentslots = equipmentslotparent.GetComponentsInChildren<UI_equipmentslot>();
        addstartingitems();
    }

    private void addstartingitems()
    {
        for (int i = 0; i < startingequipment.Count; i++)
        {
            additem(startingequipment[i]);
        }
    }
    #region 拾取与穿脱装备
    public void equipitem(itemdata _item)
    {
        itemdata_equipment equipmentitem = _item as itemdata_equipment;
        storehouseitem newitem = new storehouseitem(equipmentitem);

        itemdata_equipment oldequipment = null;
        foreach (KeyValuePair<itemdata_equipment, storehouseitem> item in equipmentdictionary)
        {
            if (item.Key.equipmentype == equipmentitem.equipmentype)
            {
                oldequipment = item.Key;

            }
        }
        if (oldequipment != null)
        {
            Unequipitem(oldequipment);
            additem(oldequipment);
        }

        equipment.Add(newitem);
        equipmentdictionary.Add(equipmentitem, newitem);
        equipmentitem.Addmodifiers();
        removeitem(_item);
        //updateslot_UI();
    }

    public void Unequipitem(itemdata_equipment itemtodelete)
    {
        if (equipmentdictionary.TryGetValue(itemtodelete, out storehouseitem value))
        {
            equipment.Remove(value);
            equipmentdictionary.Remove(itemtodelete);
            itemtodelete.Removemodifiers();
        }
    }

    private void updateslot_UI()//因为是先遍历再添加，所以只要添加序列没变就可以正确显示
    {
        for (int i = 0; i < equipmentslots.Length; i++)
        {
            foreach (KeyValuePair<itemdata_equipment, storehouseitem> item in equipmentdictionary)
            {
                if (item.Key.equipmentype == equipmentslots[i].equipmenttype)
                {
                    equipmentslots[i].updateslot(item.Value);

                }
            }
        }
        for (int i = 0; i < storehouseitemslots.Length; i++)
        {
            storehouseitemslots[i].clearslot();
        }
        for (int i = 0; i < stashitemslots.Length; i++)
        {
            stashitemslots[i].clearslot();
        }
        for (int i = 0; i < storehouseitems.Count; i++)
        {
            storehouseitemslots[i].updateslot(storehouseitems[i]);
        }
        for (int i = 0; i < stashitems.Count; i++)
        {
            stashitemslots[i].updateslot(stashitems[i]);
        }
    }
    public void additem(itemdata _item)
    {
        if (_item.itemtype == Itemtype.equipment)
        {
            addtostorehouse(_item);
        }
        else if (_item.itemtype == Itemtype.material)
        {
            addtostash(_item);
        }
        updateslot_UI();
    }

    private void addtostash(itemdata _item)
    {
        if (stashdictionary.TryGetValue(_item, out storehouseitem value))//如果此物品已存在，可以找到列表，直接加1
        {
            value.addstack();
        }
        else//否则，创建列表并赋予权值。
        {
            storehouseitem newitem = new storehouseitem(_item);
            stashitems.Add(newitem);
            stashdictionary.Add(_item, newitem);
        }
    }

    private void addtostorehouse(itemdata _item)
    {
        if (storedictionary.TryGetValue(_item, out storehouseitem value))//如果此物品已存在，可以找到列表，直接加1
        {
            value.addstack();
        }
        else//否则，创建列表并赋予权值。
        {
            storehouseitem newitem = new storehouseitem(_item);
            storehouseitems.Add(newitem);
            storedictionary.Add(_item, newitem);
        }
    }

    public void removeitem(itemdata _item)
    {
        if (storedictionary.TryGetValue(_item, out storehouseitem value))
        {
            if (value.stacksize <= 1)
            {
                storehouseitems.Remove(value);
                storedictionary.Remove(_item);
            }
            else
            {
                value.removestack();
            }
        }

        if (stashdictionary.TryGetValue(_item, out storehouseitem stashvalue))
        {
            if (stashvalue.stacksize <= 1)
            {
                stashitems.Remove(value);
                stashdictionary.Remove(_item);
            }
            else
            {
                stashvalue.removestack();
            }
        }
        updateslot_UI();
    }
    #endregion
    public bool cancraft(itemdata_equipment _itemtocraft, List<storehouseitem> _requiredmaterials)
    {
        List<storehouseitem> materialstodelete = new List<storehouseitem>();
        int num = 0;

        for (int i = 0; i < _requiredmaterials.Count; i++)
        {
            if (stashdictionary.TryGetValue(_requiredmaterials[i].data, out storehouseitem stashvalue))
            {
                if (stashvalue.stacksize < _requiredmaterials[i].stacksize)
                {
                    Debug.Log("材料不够");
                    return false;
                }
                else
                {
                    //num[i] = _requiredmaterials[i].stacksize;
                    materialstodelete.Add(stashvalue);
                    materialstodelete[num].stacksize = _requiredmaterials[i].stacksize;
                    num++;
                    //Debug.Log(num + _requiredmaterials[i].data.name + _requiredmaterials[i].stacksize);
                }
            }
            else
            {
                Debug.Log("材料不够");
                return false;
            }
        }

        for (int i = 0; i < materialstodelete.Count; i++)
        {
            for (int j = 0; j < materialstodelete[i].stacksize; j++)
            {
                removeitem(materialstodelete[i].data);
                //Debug.Log(j);
            }
        }
        additem(_itemtocraft);
        return true;
    }

    public List<storehouseitem> getequipment() => equipment;
    public List<storehouseitem> getstash() => stashitems;

    public itemdata_equipment getequipment(Equipmentype _type)
    {
        itemdata_equipment equipeditem = ScriptableObject.CreateInstance<itemdata_equipment>();
        foreach (KeyValuePair<itemdata_equipment, storehouseitem> item in equipmentdictionary)
        {
            if (item.Key.equipmentype == _type)
            {
                equipeditem = item.Key;
            }
        }
        return equipeditem;
    }

    public void useflask()
    {
        itemdata_equipment currentflask = getequipment(Equipmentype.flask);
        if(currentflask.itemtype == Itemtype.material)
        {
            return;
        }
        currentflask.itemcooldown = 4;
        bool canuseflask = Time.time > flaskcooldown + lasttimeuseflask;
        if (canuseflask)
        {
            flaskcooldown = currentflask.itemcooldown;
            currentflask.excuteitemeffect(null);
            lasttimeuseflask = Time.time;
        }
        else
        {
            Debug.Log("血瓶冷却中");
        }
    }
    public bool canusearmor()
    {
        itemdata_equipment currentarmor = getequipment(Equipmentype.Aromr);
        if (currentarmor.itemtype == Itemtype.material)
        {
            return false;
        }
        bool canusearmor = Time.time >armorcooldown + lasttimeusearmor;
        if (canusearmor)
        {
            armorcooldown = currentarmor.itemcooldown;
            lasttimeusearmor = Time.time;
            return true;
        }
        else
        {
            Debug.Log("装备冷却中");
            //Debug.Log(canusearmor);
            return false;
        }
    }
}
