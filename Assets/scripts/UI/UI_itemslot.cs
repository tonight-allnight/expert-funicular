using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;

public class UI_itemslot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemimage;
    [SerializeField] private TextMeshProUGUI itemtext;

    public storehouseitem item;

    public void updateslot(storehouseitem _newitem)//创造物品格子
    {
        item = _newitem;
        itemimage.color = Color.white;  
        if (item != null)
        {
            itemimage.sprite = item.data.icon;
            if (item.stacksize > 1)
            {
                itemtext.text = item.stacksize.ToString();
            }
            else
            {
                itemtext.text = "";
            }
        }
    }
    public void clearslot()
    {
        item = null;
        itemimage.sprite =  null;
        itemimage.color= Color.clear;
        itemtext.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.data == null)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            storehouse.instance.removeitem(item.data);
            return;
        }
        if (item.data.itemtype == Itemtype.equipment)
        {
            storehouse.instance.equipitem(item.data);
        }
    }
}
