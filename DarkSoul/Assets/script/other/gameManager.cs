using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject EnemyUI;
    [SerializeField] Text Enemy_Text;
    [SerializeField] Image Enemy_HP;

    [SerializeField] GameObject enemyPoint;

    EnemyPool enemyPool;
    float time;
    private void Awake()
    {
        enemyPool = GetComponent<EnemyPool>();
    }
    private void Update()
    {
         time+= Time.deltaTime;
        if (time >= 5)
        {
            spawnEnemy();
            time = 0;
        }
    }
    public void EnemyUIControllor(Collider collider)
    {
        EnemyParameter parameter = collider.gameObject.GetComponent<EnemyParameter>();

        EnemyUI.SetActive(true);
        Enemy_Text.text = collider.gameObject.name;
        Enemy_HP.fillAmount= (float)parameter.CurrHp/(float)parameter.MaxHP;
    }
    public void spawnEnemy()
    {
        int PointCount = enemyPoint.transform.childCount-1;
        int RangePoint = Random.Range(0,PointCount);

        GameObject enemy = enemyPool.createEnemy().gameObject;
        enemy.transform.position = enemyPoint.transform.GetChild(RangePoint).gameObject.transform.position;
    }
}
