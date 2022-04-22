using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float speed;
    float desTime;
    [HideInInspector]public int attackVaule;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        desTime += Time.deltaTime;
        if (desTime >= 5)
        {
            if (gameObject)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControllor>().getdamage(attackVaule);
            Destroy(gameObject);
        }
    }
}
