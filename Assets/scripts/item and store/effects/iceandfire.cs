using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "data/itemeffecct/iceandfire", fileName = "iceandfire effect")]
public class iceandfire : itemeffect
{
    [SerializeField] private GameObject iceandfireprefab;
    [SerializeField] private float xvelocity;
    public override void executeeffect(Transform _respondransform)
    {
        player player1 = playermanager.instance.player;
        bool thridattack = player1.playerprimaryattackstate.combocounter == 2;
        if (thridattack)
        {   
            GameObject newiceandfire = Instantiate(iceandfireprefab, _respondransform.position, player1.transform.rotation);
            newiceandfire.GetComponent<Rigidbody2D>().velocity = new Vector2(xvelocity * player1.facingdir , 0);
            Destroy(newiceandfire, 5);
        }
        
        
        
    }
}
