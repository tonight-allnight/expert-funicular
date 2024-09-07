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
    private Transform clclc;
    public void createclone(Transform _cloneposition , float _facingdir , Vector3 _offset)
    {
        GameObject newclone = Instantiate(cloneprefab);
        newclone.GetComponent<clone_skill_controller>().setupclone(_cloneposition , cloneduration ,_facingdir , canattack ,  canchangedir , _offset , findclosestenemy(newclone.transform));
        //Debug.Log("冲刺时判定最近" +findclosestenemy(newclone.transform));
    }
}
