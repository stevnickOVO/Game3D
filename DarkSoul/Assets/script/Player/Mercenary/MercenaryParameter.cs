using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryParameter : MonoBehaviour
{
    public int MaxHP;
    public int CurrHP;
    public int AttackVaule;

    private void Awake()
    {
        CurrHP = MaxHP;
    }
}
