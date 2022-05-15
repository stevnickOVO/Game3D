using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayControllor : MonoBehaviour
{
    [SerializeField] PlayerParameter player;
    [SerializeField] CastleParameter castle;
    [SerializeField] MercenaryTable tank;
    [SerializeField] MercenaryTable tower;
    [SerializeField] MercenaryTable pubby;

    [SerializeField] private Text money;

    [SerializeField] private Text player_HP_text;
    private int player_HP_gold=100;
    [SerializeField] private Text player_Attack_text;
    private int player_Attack_gold = 100;
    [SerializeField] private Text player_HPs_text;
    private int player_HPs_gold = 10;
    [SerializeField] private Text Castle_HP_text;
    private int Castle_HP_gold = 200;
    [SerializeField] private Text Mercenary_Tank_text;
    private int Mercenary_Tank_gold = 20;
    [SerializeField] private Text Mercenary_Tower_text;
    private int Mercenary_Tower_gold = 100;
    [SerializeField] private Text Mercenary_Pubby_text;
    private int Mercenary_Pubby_gold = 100;
    private void Awake()
    {
        playerHP();
        playerAttack();
        playerHPs();
        CastleHP();
        MercenaryTank();
        MercenaryTower();
        MercenaryPubby();
    }
    private void Update()
    {
        money.text = PlayerManager.playerManagerInstance.totleMoney.ToString();
    }
    private void playerHP()
    {
        player_HP_text.text = player_HP_gold.ToString();
    }
    public void playerHPBTN()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= player_HP_gold) {
            player.MaxHP += 50;
            player.CurrHP += 50;
            PlayerManager.playerManagerInstance.totleMoney -= player_HP_gold;
            player_HP_gold *= 2;
            playerHP();
        }
        
    }

    private void playerAttack()
    {
        player_Attack_text.text = player_Attack_gold.ToString();
    }
    public void playerAttackBTN()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= player_Attack_gold)
        {
            player.AttackVaule += 10;
            PlayerManager.playerManagerInstance.totleMoney -= player_Attack_gold;
            player_Attack_gold *= 2;
            playerAttack();
        }
            
    }

    private void playerHPs()
    {
        player_HPs_text.text = player_HPs_gold.ToString();
    }
    public void playerHPsBtn()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= player_HPs_gold)
        {
            player.hps++;
            PlayerManager.playerManagerInstance.totleMoney -= player_HPs_gold;
            player_HPs_gold *= 2;
            playerHPs();
        }
    }

    private void CastleHP()
    {
        Castle_HP_text.text = Castle_HP_gold.ToString();
    }
    public void CastleHPBTN()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= Castle_HP_gold)
        {
            castle.MaxHP += 250;
            castle.CurrHP += 250;
            PlayerManager.playerManagerInstance.totleMoney -= Castle_HP_gold;
            Castle_HP_gold *= 2;
            CastleHP();
        }
    }

    private void MercenaryTank()
    {
        Mercenary_Tank_text.text = Mercenary_Tank_gold.ToString();
    }
    public void MercenaryTankBTN()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= Mercenary_Tank_gold)
        {
            tank.level++;
            tank.levelToMercenary();
            PlayerManager.playerManagerInstance.totleMoney -= Mercenary_Tank_gold;
            Mercenary_Tank_gold *= 2;
            MercenaryTank();
        }
    }

    private void MercenaryTower()
    {
        Mercenary_Tower_text.text = Mercenary_Tower_gold.ToString();
    }
    public void MercenaryTowerBTN()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= Mercenary_Tower_gold)
        {
            tower.level++;
            tower.levelToMercenary();
            PlayerManager.playerManagerInstance.totleMoney -= Mercenary_Tower_gold;
            Mercenary_Tower_gold *= 2;
            MercenaryTower();
        }
    }

    private void MercenaryPubby()
    {
        Mercenary_Pubby_text.text = Mercenary_Pubby_gold.ToString();
    }
    public void MercenaryPubbyBTN()
    {
        if (PlayerManager.playerManagerInstance.totleMoney >= Mercenary_Pubby_gold)
        {
            pubby.level++;
            pubby.levelToMercenary();
            PlayerManager.playerManagerInstance.totleMoney -= Mercenary_Pubby_gold;
            Mercenary_Pubby_gold *= 2;
            MercenaryPubby();
        }
    }
    public void gameStart()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void gameExit()
    {
        Application.Quit();
    }
}
