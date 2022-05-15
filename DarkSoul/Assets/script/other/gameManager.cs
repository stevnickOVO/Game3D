using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] int CurrRound;
    [SerializeField] int EnemySTR;
    [SerializeField] int EnemyCount;
    [SerializeField] GameObject EnemyUI;
    [SerializeField] Text Enemy_Text;
    [SerializeField] Image Enemy_HP;
    [SerializeField] Text timerText;
    [SerializeField] GameObject gameReplayUI;
    [SerializeField] GameObject enemyPoint;

    public bool isFight;
    float startTime=5;
    EnemyPool enemyPool;
    float time;
    int firstEnemyCount;

    public static gameManager gameManagerInstance;
    private void Awake()
    {
        gameManagerInstance = this;
        enemyPool = GetComponent<EnemyPool>();
        firstEnemyCount = 10;
        GameReplay();
    }
    private void Update()
    {
        timeControllor();

         time += Time.deltaTime;
        if (time >= 5&&isFight)
        {
            spawnEnemy();
            time = 0;
        }
    }
    public void EnemyUIControllor(Collider collider)
    {
        Parameter parameter = collider.gameObject.GetComponent<Parameter>();

        EnemyUI.SetActive(true);
        Enemy_Text.text = collider.gameObject.name;
        Enemy_HP.fillAmount= (float)parameter.CurrHP/(float)parameter.MaxHP;
    }
    public void spawnEnemy()
    {
        int PointCount = enemyPoint.transform.childCount;
        int RangePoint = Random.Range(0,PointCount);

        int RandomEnemy = Random.Range(0,EnemySTR);
        enemyPool.setEnemyCurList(RandomEnemy);
        GameObject enemy = enemyPool.createEnemy().gameObject;
        enemy.transform.position = enemyPoint.transform.GetChild(RangePoint).gameObject.transform.position;
        EnemyCount--;
        if (EnemyCount < 1)
        {
            startTime = 20;
            isFight = false;
            GameReplay();
        }
    }
    public void timeControllor()
    {
        if (startTime > 0)
        {
            timerText.gameObject.SetActive(true);
            int a = (int)startTime;
            timerText.text = a.ToString();
            startTime -= Time.deltaTime;
        }
        else
        {
            if (!isFight)
            {
                CurrRound++;
                EnemyCount = firstEnemyCount+ (CurrRound * 2);
                enemySTRControllor();
                isFight = true;
            }
            timerText.gameObject.SetActive(false);
        }
    }
    public void enemySTRControllor()
    {
        if (CurrRound % 5 == 0)
        {
            EnemySTR = enemyPool.getEnemyListLentgh();
        }
        else if (CurrRound % 3 == 0 || CurrRound % 3 == 1 && CurrRound > 2)
        {
            EnemySTR = enemyPool.getEnemyListLentgh()-1;
        }else EnemySTR = enemyPool.getEnemyListLentgh() -2;
    }
    public void playAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void GameReplay()
    {
        gameReplayUI.SetActive(true);
        Time.timeScale = 0;
    }
}
