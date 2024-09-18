using System;

[Serializable]
public class storehouseitem
{
    public itemdata data;
    public int stacksize;
    public storehouseitem(itemdata _item)
    {
        data = _item;
        addstack();
    }
    public void addstack()
    {
        stacksize++;
    }
    public void removestack() { stacksize--; }
}
