using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyMovetion[] enemyList;
    [SerializeField] int minAmount = 30;
    [SerializeField] int maxAmount = 500;
    ObjectPool<EnemyMovetion> objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool<EnemyMovetion>(createEnemy, activeT, activeF, desEnemy, true, minAmount, maxAmount);
    }
    private void Start()
    {
        EnemyMovetion enemy = objectPool.Get();
    }
    private void desEnemy(EnemyMovetion obj)
    {
        Destroy(obj.gameObject);
    }

    private void activeF(EnemyMovetion obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void activeT(EnemyMovetion obj)
    {
        obj.gameObject.SetActive(true);
    }
    public void releaseOB(GameObject enemy)
    {
        objectPool.Release(enemy.GetComponent<EnemyMovetion>());
    }
    public EnemyMovetion createEnemy()
    {
        EnemyMovetion enemy = Instantiate(enemyList[0],transform);
        return enemy;
    }
}
