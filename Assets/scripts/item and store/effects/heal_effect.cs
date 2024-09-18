using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "data/itemeffecct/head_effect", fileName = "Heal effect")]
public class heal_effect : itemeffect
{
    [Range(0f,1f)]
    [SerializeField] private float healpercentage;
    public override void executeeffect(Transform _enemytransform)
    {
        playerstats playerstat = playermanager.instance.player.GetComponent<playerstats>();
        int healammount = Mathf.RoundToInt(playerstat.getmaxHP() * healpercentage);

        playerstat.increasehealth(healammount);
    }
}
