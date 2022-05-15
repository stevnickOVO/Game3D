using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyMovetion : MonoBehaviour
{
    [SerializeField] float findTargetRadius;
    [SerializeField] protected float attackRange;
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] float CastleRange;
    bool isDead;
    protected bool isAttack;
    protected enum enemyAction {idle, moveToCastle, attackTarget,die }
    protected enemyAction enemyState;
    protected GameObject target;
    Animator animator;
    NavMeshAgent agent;
    protected Parameter enemyParameter;
    Action<EnemyPool> action;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyParameter = GetComponent<Parameter>();
        target = GameObject.Find("Castle").gameObject;
        enemyState = new enemyAction();
        enemyState = enemyAction.moveToCastle;
        agent.stoppingDistance = attackRange;
        isDead = false;
        enemyPool = GameObject.Find("GameManager").GetComponent<EnemyPool>();
    }

    void Update()
    {
        switchControllor();
    }
    public void switchControllor()
    {
        if (!deadCheck())
        {
            if (foundPlayer())
            {
                enemyState = enemyAction.attackTarget;
            }else enemyState = enemyAction.moveToCastle;
        }
        else enemyState = enemyAction.die;



        switch (enemyState)
        {
            case enemyAction.idle:
                agent.SetDestination(gameObject.transform.position);
                animator.SetFloat("Speed", 0);
                break;
            case enemyAction.moveToCastle:
                target = GameObject.Find("Castle").gameObject;
                agent.stoppingDistance = CastleRange;
                if (Vector3.Distance(transform.position, target.transform.position) <= CastleRange)
                {
                    gameObject.transform.LookAt(target.transform.position);
                    animator.SetBool("isAttack", true);
                }
                else
                {
                    agent.SetDestination(target.transform.position);
                    animator.SetBool("isAttack", false);
                    animator.SetFloat("Speed", 1);
                }

                break;
            case enemyAction.attackTarget:
                agent.stoppingDistance = attackRange;
                if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                {
                    gameObject.transform.LookAt(target.transform.position);
                    animator.SetBool("isAttack", true);
                }
                else
                {
                    agent.SetDestination(target.transform.position);
                    animator.SetBool("isAttack", false);
                    animator.SetFloat("Speed", 1);
                }
                break;
            case enemyAction.die:
                animator.SetBool("Die", true);
                break;
        }
    }
    public bool foundPlayer()
    {
        var collider = Physics.OverlapSphere(transform.position,findTargetRadius, targetMask);
        if (collider.Length > 0)
        {
            target = collider[0].gameObject;
            return true;
        }
        return false;
    }
    public bool deadCheck()
    {
        if (enemyParameter.CurrHP <= 0)
        {
            isDead = true;
        }
        else isDead = false;
        return isDead;
    }
    public void deadToPool()
    {
        enemyPool.releaseOB(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findTargetRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}
