using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolem :EnemyMovetion
{
    [SerializeField] float Range;
    [SerializeField] GameObject AttackPoint;
    
    public void attack()
    {
        var colliders = Physics.OverlapSphere(AttackPoint.transform.position, Range, targetMask);
        if (colliders.Length > 0)
        {
            colliders[0].GetComponent<PlayerControllor>().getdamage(enemyParameter.AttackVaule);
        }
    }
}
