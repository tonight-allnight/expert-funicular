using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class thunderstrike_controller : MonoBehaviour
{
    [SerializeField] private characterstates targetstates;
    [SerializeField] private float speed;
    private int damage;
    private Animator animator;
    private bool triggered;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void setup(int _damahe , characterstates _targetstats)
    {
        damage = _damahe;
        targetstates = _targetstats;
    }
    // Update is called once per frame
    void Update()
    {
        if (!targetstates)
            return;
        if (triggered)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetstates.transform.position ,speed * Time.deltaTime);
        transform.right = transform.position - targetstates.transform.position;
        if(Vector2.Distance(transform.position,targetstates.transform.position) < .1f)
        {
            animator.transform.localPosition = new Vector3(0, .5f);
            animator.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3,3);

            Invoke("DamageANDdeatroy",.2f);
            triggered = true;
            
            animator.SetTrigger("hit");
        }
    }
    private void DamageANDdeatroy()
    {
        targetstates.applyshock(true);
        targetstates.takedamage(damage);
        Destroy(gameObject, .4f);
    }
}
