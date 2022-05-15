using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParameter : Parameter
{
    [SerializeField] Image HPbar;
    float time;
    public int hps;
    PlayerControllor player;
    private void Awake()
    {
        CurrHP = MaxHP;
        player = GetComponent<PlayerControllor>();
    }
    private void Update()
    {
        HPbar.fillAmount = (float)CurrHP / (float)MaxHP;
        PlayerHps();
    }
    public void PlayerHps()
    {
        if (CurrHP < MaxHP)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                CurrHP += hps;
                time = 0;
            }
        }
        else if (CurrHP > MaxHP)
        {
            CurrHP = MaxHP;
        }
    }
    public override bool getDamage(int damage)
    {
        CurrHP -= damage;

        if (CurrHP <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            player.isDie = true;
            GetComponent<Animator>().SetBool("Die", true);
        }
        return CurrHP <= 0;
    }
}
