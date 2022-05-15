using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : Parameter
{
    private void Awake()
    {
        CurrHP = MaxHP;
    }
    public override bool getDamage(int damage)
    {
        CurrHP -= damage;
        if (CurrHP <= 0)
        {
            PlayerManager.playerManagerInstance.totleMoney += money;
            GetComponent<EnemyMovetion>().deadToPool();
        }
        return GetComponent<EnemyMovetion>().deadCheck();
    }
}
