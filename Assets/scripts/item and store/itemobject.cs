using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class itemobject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private itemdata item;

    private void setvisual()
    {
        if (item == null)
            return;
        GetComponent<SpriteRenderer>().sprite = item.icon;
        gameObject.name = "itemobject-" + item.name;
    }

    public void setupitem(itemdata _itemdata , Vector2 _velocity)
    {
        item = _itemdata;
        rb.velocity = _velocity;
        setvisual();
    }

    public void pickitem()
    {
        storehouse.instance.additem(item);
        Destroy(gameObject);
    }
}
