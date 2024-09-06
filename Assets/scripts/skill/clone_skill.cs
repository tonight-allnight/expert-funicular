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
    public void createclone(Transform _cloneposition , float _facingdir)
    {
        GameObject newclone = Instantiate(cloneprefab);
        newclone.GetComponent<clone_skill_controller>().setupclone(_cloneposition , cloneduration ,_facingdir , canattack ,  canchangedir );
        Debug.Log(_cloneposition);
    }
}
