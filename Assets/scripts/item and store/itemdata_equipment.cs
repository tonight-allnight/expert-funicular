using System.Collections.Generic;
using UnityEngine;
public enum Equipmentype
{
    weapon,
    Aromr,
    amulet,//�����
    flask//ƿ��s
}


[CreateAssetMenu(menuName = "data/equipment", fileName = "new item")]
public class itemdata_equipment : itemdata
{
    public Equipmentype equipmentype;

    public float itemcooldown;
    public itemeffect[] itemeffects;

    public int strength;//1+1�˺�+1��������
    public int agility;//1 + 1%���ܼ��޵л���1% ����޵�֡
    public int intelligence;//1+ 1ħ���˺� 3ħ������
    public int vitality;// ������

    [Header("��������")]
    public int damage;
    public int critchance;//��������
    public int critpower; //�����˺� 130%
     
    [Header("��������")]
    public int maxHp;//�������
    public int armor;//����
    public int evasion;//
    public int magicresistance; //����


    [Header("ħ���˺�")]
    public int firedamage;
    public int icedamage;
    public int lightingdamage;

    [Header("�ϳɲ���")]
    public List<storehouseitem> craftingmaterials;

    public void excuteitemeffect(Transform _enemytransform)
    {
        //Debug.Log(itemeffects.Length);
        for (int i = 0; i < itemeffects.Length; i++)
        {
            //Debug.Log("ִ��"+ i);
            if (itemeffects[i] == null)
                continue;
            itemeffects[i].executeeffect(_enemytransform);//ÿһ����Ч���Լ��Ľű�������ִ��
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
