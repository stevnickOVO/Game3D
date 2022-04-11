using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordHitBox : MonoBehaviour
{
    PlayerControllor HitBoxFather;
    private void Awake()
    {
        HitBoxFather = gameObject.transform.parent.parent.parent.GetComponent<PlayerControllor>();
        print(HitBoxFather.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyMovetion>().getDanage();
        }
    }
}
