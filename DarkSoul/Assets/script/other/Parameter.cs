using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public int MaxHP;
    public int CurrHP;
    private int HPup;
    public int level;
    public int AttackVaule;
    private int Attackup;
    public int money;
    private void Awake()
    {
        CurrHP = MaxHP;
        HPup = MaxHP / 2;
        Attackup = AttackVaule / 2;
    }
    public void setMercenary()
    {
        MaxHP=MaxHP+ (HPup*level);
        CurrHP = CurrHP + (HPup * level);
        AttackVaule = AttackVaule + (Attackup * level);
    }
    public virtual bool getDamage(int damage)
    {
        CurrHP -= damage;

        if (CurrHP <= 0)
        {
            Destroy(gameObject);
        }
        return CurrHP<= 0;
    }
}
