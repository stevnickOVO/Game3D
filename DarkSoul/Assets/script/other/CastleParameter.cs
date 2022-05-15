using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleParameter : Parameter
{
    [SerializeField] GameObject All_UI_Father;
    [SerializeField] GameObject gameOverUI;
    private void Awake()
    {
        CurrHP = MaxHP;
    }
    public override bool getDamage(int damage)
    {
        CurrHP -= damage;

        if (CurrHP <= 0)
        {
            GameOver();
        }
        return CurrHP <= 0;
    }
    public void GameOver()
    {
        if (CurrHP <= 0)
        {
            int allUI= All_UI_Father.transform.childCount;
            for (int a = 0; a < allUI; a++)
            {
                All_UI_Father.transform.GetChild(a).gameObject.SetActive(false);
            }
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
