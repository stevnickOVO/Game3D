using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleControllor : MonoBehaviour
{
    [SerializeField] private int MaxHP;
    [SerializeField] private int CurrHP;
    private void Awake()
    {
        CurrHP = MaxHP;
    }
    public void getdamage(int damage)
    {
        CurrHP -= damage;
    }
    public void GameOver()
    {
        print("GameOver");
    }
}
