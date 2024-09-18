 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stattype
{
    strength,
    agility,
    intelligence,
    vitality,
    maxHp,
    armor,
    evasion,
    magicresistance,
    damage,
    critchance,
    critpower,
    firedamage,
    icedamage,
    lightingdamage
}

[CreateAssetMenu(menuName = "data/itemeffecct/buff_effect", fileName = "Buff effect")]
public class buff_effect : itemeffect
{
    private playerstats stats;
    [SerializeField] private stattype bufftype;
    [SerializeField] private int buffamount;
    [SerializeField] private float buffduration;
    public override void executeeffect(Transform _enemytransform)
    {
        stats = playermanager.instance.player.GetComponent<playerstats>();
        stats.increasestatby(buffamount,buffduration,stattomodify());
    }
    private stat stattomodify()
    {
        switch (bufftype){
            case stattype.strength:
            {
                return stats.strength;
            }
                case stattype.agility:
                { return stats.agility; }
                case stattype.intelligence:
                { return stats.intelligence; }
                case stattype.vitality:
                { return stats.vitality; }
                case stattype.maxHp:
                {  return stats.maxHp; }
                case stattype.armor:
                { return stats.armor; }
                case stattype.evasion:
                { return stats.evasion; }
                case stattype.magicresistance:
                { return stats.magicresistance; }
                case stattype.damage:
                { return stats.damage; }
                case stattype.critchance:
                { return stats.critchance; }
                case stattype.critpower:
                { return stats.critpower; }
                case stattype.firedamage:
                { return stats.firedamage; }
                case stattype.icedamage:
                { return stats.icedamage; }
                case stattype.lightingdamage:
                { return stats.lightingdamage; }
            default:
                return null;
                

        }
    }

}
