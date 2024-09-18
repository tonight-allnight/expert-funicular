using System.Collections.Generic;
using UnityEngine;
public enum Equipmentype
{
    weapon,
    Aromr,
    amulet,//护身符
    flask//瓶子s
}


[CreateAssetMenu(menuName = "data/equipment", fileName = "new item")]
public class itemdata_equipment : itemdata
{
    public Equipmentype equipmentype;

    public float itemcooldown;
    public itemeffect[] itemeffects;

    public int strength;//1+1伤害+1攻击威力
    public int agility;//1 + 1%闪避加无敌机会1% 随机无敌帧
    public int intelligence;//1+ 1魔法伤害 3魔法抗性
    public int vitality;// 加体力

    [Header("攻击能力")]
    public int damage;
    public int critchance;//暴击机会
    public int critpower; //暴击伤害 130%
     
    [Header("防守能力")]
    public int maxHp;//最大生命
    public int armor;//护甲
    public int evasion;//
    public int magicresistance; //法抗


    [Header("魔法伤害")]
    public int firedamage;
    public int icedamage;
    public int lightingdamage;

    [Header("合成材料")]
    public List<storehouseitem> craftingmaterials;

    public void excuteitemeffect(Transform _enemytransform)
    {
        //Debug.Log(itemeffects.Length);
        for (int i = 0; i < itemeffects.Length; i++)
        {
            //Debug.Log("执行"+ i);
            if (itemeffects[i] == null)
                continue;
            itemeffects[i].executeeffect(_enemytransform);//每一个特效有自己的脚本，依次执行
        }
    }
    public void Addmodifiers()
    {
        playerstats playerstat = playermanager.instance.player.GetComponent<playerstats>();

        playerstat.strength.addmodifier(strength);
        playerstat.agility.addmodifier(agility);
        playerstat.intelligence.addmodifier(intelligence);
        playerstat.vitality.addmodifier(vitality);

        playerstat.damage.addmodifier(damage);
        playerstat.critchance.addmodifier(critchance);
        playerstat.critpower.addmodifier(critpower);

        playerstat.maxHp.addmodifier(maxHp);
        playerstat.armor.addmodifier(armor);
        playerstat.evasion.addmodifier(evasion);
        playerstat.magicresistance.addmodifier(magicresistance);

        playerstat.firedamage.addmodifier(firedamage);
        playerstat.icedamage.addmodifier(icedamage);
        playerstat.lightingdamage.addmodifier(lightingdamage);

    }
    public void Removemodifiers() {
        playerstats playerstat = playermanager.instance.player.GetComponent<playerstats>();

        playerstat.strength.removemodifier(strength);
        playerstat.agility.removemodifier(agility);
        playerstat.intelligence.removemodifier(intelligence);
        playerstat.vitality.removemodifier(vitality);

        playerstat.damage.removemodifier(damage);
        playerstat.critchance.removemodifier(critchance);
        playerstat.critpower.removemodifier(critpower);

        playerstat.maxHp.removemodifier(maxHp);
        playerstat.armor.removemodifier(armor);
        playerstat.evasion.removemodifier(evasion);
        playerstat.magicresistance.removemodifier(magicresistance);

        playerstat.firedamage.removemodifier(firedamage);
        playerstat.icedamage.removemodifier(icedamage);
        playerstat.lightingdamage.removemodifier(lightingdamage);
    }

}
