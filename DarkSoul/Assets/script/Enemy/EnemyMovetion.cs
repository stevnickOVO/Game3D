using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyMovetion : MonoBehaviour
{
    [SerializeField] float findTargetRadius;
    [SerializeField] float attackRange;
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] GameObject target;
    [SerializeField] EnemyPool enemyPool;
    bool isDead;
    bool isAttack;
    enum enemyAction {idle,move,attack,die,getHit }
    enemyAction enemyState;
    Animator animator;
    NavMeshAgent agent;
    protected EnemyParameter enemyParameter;
    Action<EnemyPool> action;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyParameter = GetComponent<EnemyParameter>();
        enemyState = new enemyAction();
        enemyState = enemyAction.idle;
        target = gameObject;
        agent.stoppingDistance = attackRange;
        isDead = false;
        enemyPool = GameObject.Find("GameManager").GetComponent<EnemyPool>();
    }

    void Update()
    {
        if (!isDead)
        {
            targetCheck();
            attckTarget();
        }
        
        switchControllor();
    }
    public void switchControllor()
    {
        switch (enemyState)
        {
            case enemyAction.idle:
                agent.SetDestination(gameObject.transform.position);
                animator.SetFloat("Speed", 0);
                break;
            case enemyAction.move:
                agent.SetDestination(target.transform.position);
                animator.SetFloat("Speed", 1);
                break;
            case enemyAction.attack:
                gameObject.transform.LookAt(target.transform.position);
                animator.SetBool("Attack", isAttack);
                break;
            case enemyAction.getHit:
                break;
            case enemyAction.die:
                animator.SetBool("Die", true);
                break;
        }
    }
    public void targetCheck()
    {
        if (target.tag != "Player"||target==null)
        {
            var colliders = Physics.OverlapSphere(transform.position, findTargetRadius, targetMask);
            if (colliders.Length>0)
            {
                target = colliders[0].gameObject;
                Array.Clear(colliders, 0, colliders.Length);
                enemyState = enemyAction.move;
            }
            else
            {
                target = gameObject;
            }
        }
    }
    public void attckTarget()
    {
        if (target.tag == "Player" && Vector3.Distance(gameObject.transform.position, target.transform.position) < attackRange)
        {
            enemyState = enemyAction.attack;
            isAttack = true;
        }
        else
        {
            isAttack = false;
            enemyState = enemyAction.move;
        }
    }
    public void getDanage(int damage)
    {
        enemyParameter.CurrHp -= damage;
        animator.SetBool("getHit",true);
        deadCheck();
    }
    public void deadCheck()
    {
        if (enemyParameter.CurrHp <= 0)
        {
            isDead = true;
            enemyState = enemyAction.die;
        }
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
