﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour
{
    public int MaxHP;
    public int CurrHp;
    public int AttackVaule;
    private void Awake()
    {
        CurrHp = MaxHP;
    }
}
