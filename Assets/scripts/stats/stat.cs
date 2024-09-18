using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class stat

{
    [SerializeField] private int basevalue;

    public List<int> modifiers;

    public int getvalue()
    {
        int finalvalue = basevalue;
        foreach(int modifier in modifiers)
        {
            finalvalue += modifier;
        }
        return finalvalue;
    }

    public void setdefaultvalue(int value)
    {
        basevalue = value;
    }

    public void addmodifier(int _modifier)
    {
        modifiers.Add( _modifier );
    }
    public void removemodifier(int _modifier)
    {
        modifiers.Remove( _modifier );
    }
}
