using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "data/itemeffecct/thunder_strike", fileName = "thunder strike effect")]
public class thunderstrikeeffect : itemeffect
{
    [SerializeField] private GameObject thunderprefab;
    private int chancetothunder = 66;
    private int count = 0;
    public override void executeeffect(Transform _enemytransform)
    {
        int random = Random.Range(0, 50);
        if(count + random > chancetothunder)
        {
            GameObject newthunder = Instantiate(thunderprefab,_enemytransform.position,Quaternion.identity);
            Destroy(newthunder,.5f);
            count = 0;
        }
        else
        {
            count += random;
        }
    }
}
