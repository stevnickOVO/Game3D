using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordHitBox : MonoBehaviour
{
    PlayerControllor HitBoxFather;
    gameManager manager;
    private void Awake()
    {
        HitBoxFather = gameObject.transform.parent.parent.parent.GetComponent<PlayerControllor>();
        manager = GameObject.Find("GameManager").gameObject.GetComponent<gameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"&& HitBoxFather.useSwordHitBox)
        {
            other.GetComponent<Parameter>().getDamage(HitBoxFather.totleAttack());
            manager.EnemyUIControllor(other);
        }
    }
}
