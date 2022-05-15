using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolem :EnemyMovetion
{
    [SerializeField] float Range;
    [SerializeField] GameObject AttackPoint;
    
    public void attack()
    {
        //if (target.GetComponent<Parameter>().getDamage(enemyParameter.AttackVaule))
        //{
        //    target = GameObject.Find("Castle").gameObject;
        //    enemyState = enemyAction.move;
        //    isAttack = false;
        //}
        target.SendMessage("getDamage", enemyParameter.AttackVaule);
    }
}
