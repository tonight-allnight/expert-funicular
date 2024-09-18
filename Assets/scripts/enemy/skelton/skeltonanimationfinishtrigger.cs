using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonanimationfinishtrigger : MonoBehaviour
{
    private enemy_skelton enemy => GetComponentInParent<enemy_skelton>();
    private void animationtrigger()
    {
        enemy.animationfinishtrigger();
    }
    private void attacktrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackcheck.position, enemy.attackcheckradius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<player>() != null)
            {
                playerstats playerstats = hit.GetComponent<playerstats>();
                enemy.states.DoDamage(playerstats);
            }
        }
    }
    private void opencounterwindow() => enemy.opencounterattackwindow();
    private void closecounterwindow() => enemy.closecounterattackwindow();
    private void destroyenemy()
    {
        Debug.Log("����");
        gameObject.SetActive(false);//���ö�������֡����������ʾ
    }
}
