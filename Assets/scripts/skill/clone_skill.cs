using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_skill : skill
{
    [Header("clone info")]
    [SerializeField] private float cloneduration;
    [SerializeField] private GameObject cloneprefab;
    [Space]
    [SerializeField] private bool canattack;
    [SerializeField] private bool canchangedir;

    [SerializeField] private bool createcloneondashstart;
    [SerializeField] private bool createcloneondashover;
    [SerializeField] private bool createcloneoncounterattack;
    [Header("can duplicate")]
    [SerializeField] private bool canduplicateclone;
    [SerializeField] private float chancecanduplicate  = 20;

    [Header("crystal instead of clone")]
    public bool crystalinsteaedofclone;

    public void createclone(Transform _cloneposition , float _facingdir , Vector3 _offset)
    {
        if (crystalinsteaedofclone)
        {
            skillmanager.instance.crystal.createclonecrystal();
            //skillmanager.instance.crystal.currentcrystalchooserandomtarget();
            return;
        }

        GameObject newclone = Instantiate(cloneprefab);
        newclone.GetComponent<clone_skill_controller>().setupclone(_cloneposition , cloneduration ,_facingdir , canattack ,  canchangedir , _offset , findclosestenemy(newclone.transform) , canduplicateclone , chancecanduplicate ,player);
        //Debug.Log("冲刺时判定最近" +findclosestenemy(newclone.transform));
    }

    public void Createcloneondashstart()
    {
        if (createcloneondashstart)
        {
            createclone(player.transform, player.facingdir,Vector3.zero);
        }
    }
    public void Createclonondashover()
    {
        if (createcloneondashover)
        {
            createclone(player.transform, player.facingdir, Vector3.zero);
        }
    }
    public void Createcloneoncounnterattack( Transform _enemytransform)
    {
        if (createcloneoncounterattack)
        {
            StartCoroutine(createclonewithdelay(_enemytransform , new Vector3(2 * player.facingdir, 0)));
        }
    }

    private IEnumerator createclonewithdelay(Transform enemy , Vector3 _offset)
    {
        createclone(enemy, player.facingdir, _offset);
        yield return new WaitForSeconds(.4f);
    }
}
