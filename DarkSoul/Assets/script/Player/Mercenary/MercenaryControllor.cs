using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MercenaryControllor : MonoBehaviour
{
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] float FindRange;
    [SerializeField] float AttackRange;
    Vector3 startPoint;
    enum Type { find,attack,die }
    Type type;
    int DEpointCount;
    public GameObject target;
    Animator animator;
    Parameter parameter;
    NavMeshAgent agent;
    bool isDie;
    private void Awake()
    {
        parameter = GetComponent<Parameter>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        switchCon();
    }
    public void switchCon()
    {
        if (!deadCheck())
        {
            if (FindEnemy())
            {
                type = Type.attack;
            }
            else type = Type.find;
        }
        else type = Type.die;

        switch (type)
        {
            case Type.find:
                if (Vector3.Distance(transform.position, startPoint) > AttackRange)
                {
                    agent.SetDestination(startPoint);
                    animator.SetFloat("Speed", 1);
                    animator.SetBool("Attack1", false);
                }
                else
                {
                    animator.SetFloat("Speed", 0);
                    animator.SetBool("Attack1", false);
                }
                break;
            case Type.attack:
                if (Vector3.Distance(transform.position, target.transform.position) > AttackRange)
                {
                    agent.SetDestination(target.transform.position);
                    animator.SetFloat("Speed", 1);
                    animator.SetBool("Attack1", false);
                }
                else
                {
                    transform.LookAt(target.transform.position);
                    animator.SetFloat("Speed", 0);
                    animator.SetBool("Attack1", true);
                }
                break;
            case Type.die:
                animator.SetBool("Die",true);
                Destroy(this.gameObject, 2);
                break;
        }
    }
    public bool FindEnemy()
    {
        var collider = Physics.OverlapSphere(transform.position, FindRange,EnemyLayer);
        if (collider.Length > 0)
        {
            target = collider[0].gameObject;
            return true;
        }
        else return false;
    }
    public void getdamage(int damage)
    {
        parameter.CurrHP -= damage;
        if (deadCheck())
        {
            Destroy(gameObject);
        }
    }
    public bool deadCheck()
    {
        if (parameter.CurrHP <= 0)
        {
            isDie = true;
        }
        else isDie = false;
        return isDie;
    }
    public void Attack()
    {
        target.GetComponent<Parameter>().getDamage(parameter.AttackVaule);
    }
    public void DeadToDestroy()
    {
        Destroy(this.gameObject,1);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FindRange);
    }

}
