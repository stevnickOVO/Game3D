using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryTower : MonoBehaviour
{
    [SerializeField] private GameObject towerHead;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackTime;
    private enum TowerState {idle,attack,die };
    private TowerState towerState;
    private LineRenderer lr;
    public GameObject target;
    private Parameter parameter;

    private void Awake()
    {
        parameter = GetComponent<Parameter>();
        lr = GetComponent<LineRenderer>();
        towerState = TowerState.idle;
    }
    private void Update()
    {
        towerSwitch();
    }
    public void towerSwitch()
    {
        if (!dieCheck())
        {
            if (findTarget())
            {
                towerState = TowerState.attack;
            }
            else towerState = TowerState.idle;
        }
        else towerState = TowerState.die;

        switch (towerState)
        {
            case TowerState.idle:
                lr.enabled = false;
                break;
            case TowerState.attack:
                if (target != null && target.GetComponent<Parameter>().CurrHP >= 0) attackTarget();
                else
                {
                    target = null;
                } 
                break;
            case TowerState.die:
                Destroy(this.gameObject);
                lr.enabled = false;
                break;
        }
    }
    public bool findTarget()
    {
        var collider = Physics.OverlapSphere(transform.position, maxRange, enemyLayer);

        if (collider.Length > 0)
        {
            target = collider[0].gameObject;
            return true;
        }
        else return false;
    }
    public void attackTarget()
    {
        towerHead.transform.LookAt(target.transform.position);

        attackTime -= Time.deltaTime;

        if (attackTime <= 1 && attackTime > 0)
        {
            lr.enabled = true;
            lr.SetPosition(0, towerHead.transform.position);
            lr.SetPosition(1, target.transform.position);
        }
        else if (attackTime < 0)
        {
            target.GetComponent<Parameter>().getDamage(parameter.AttackVaule);
            attackTime = 1.3f;
        }else lr.enabled = false;

    }
    public bool dieCheck()
    {
        if (parameter.CurrHP <= 0)
        {
            return true;
        }else return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minRange);
    }
}
