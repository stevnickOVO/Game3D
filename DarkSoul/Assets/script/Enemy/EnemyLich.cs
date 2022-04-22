using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLich : EnemyMovetion
{
    [SerializeField] GameObject FirePoint;
    [SerializeField] GameObject FireBall;
    
    public void frieBall()
    {
        GameObject fireBall=Instantiate(FireBall,FirePoint.transform.position,transform.rotation);
        fireBall.GetComponent<FireBall>().attackVaule = enemyParameter.AttackVaule;
    }
}
