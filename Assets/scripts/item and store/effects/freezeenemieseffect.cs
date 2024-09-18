using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "data/itemeffecct/freeze_effect", fileName = "Freeze effect")]
public class freezeenemieseffect : itemeffect
{
    [SerializeField] private float duration;
    public override void executeeffect(Transform _enemytransform)
    {
        playerstats playerstat = playermanager.instance.player.GetComponent<playerstats>();
        if (playerstat.currentHp > playerstat.maxHp.getvalue() * .1f)
            return;
        if (!storehouse.instance.canusearmor())
            return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_enemytransform.position, 2);

        foreach (var hit in colliders)
        {
            hit.GetComponent<enemy>()?.freezetimefor(duration);
        }
    }
}
