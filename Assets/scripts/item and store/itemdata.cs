using System;
using UnityEngine;
public enum Itemtype
{
    material,
    equipment
}
[CreateAssetMenu(menuName = "data/item",fileName = "new item")]
public class itemdata : ScriptableObject//��Ʒ����
{
    public Itemtype itemtype;
    public string itemname;
    public Sprite icon;
    [Range(0,100)]
    public float dropchance;
}
