using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashColdDown;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundCheck;
    float dashColdTimeCur;
    int   attackCount;
    float gravity = -18f;
    bool  attacking;
    bool  isDie;
    Vector3             gravityVectory=Vector3.zero;
    CharacterController characterController;
    PlayerParameter     parameter;
    Animator            animator;
    private void Awake()
    {
        parameter = GetComponent<PlayerParameter>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        CharacterMovetion();
        Attack();

        if (Input.GetButtonDown("Jump")) StartCoroutine(PlayerDash());
    }
    public void CharacterMovetion()
    {
        if (!attacking && !isDie) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
            var move = dir * speed * Time.deltaTime;


            if (!Physics.CheckSphere(groundCheck.transform.position, 0.1f, groundLayer))
            {
                gravityVectory.y += gravity * Time.deltaTime;
            }
            else gravityVectory = Vector3.zero;


            characterController.Move(gravityVectory * Time.deltaTime);
            characterController.Move(move);

            int playerCurrSpeed = (horizontal != 0 || vertical != 0) ? 1 : 0;
            animator.SetFloat("Speed", playerCurrSpeed);

            var PlayerPoint = Camera.main.WorldToScreenPoint(transform.position);
            var point = Input.mousePosition - PlayerPoint;
            float angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }
            
    }
    public void Attack()
    {
        if (!isDie) {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attacking = true;
                if (attackCount <= 0)
                {
                    animator.SetBool("Attack1", true);
                    attackCount++;
                }
                else if (attackCount <= 1)
                {
                    animator.SetBool("Attack2", true);
                    attackCount = 0;
                }
            }
        }
    }
    IEnumerator PlayerDash()
    {
        if (!attacking && !isDie&& dashColdTimeCur < Time.time)
        {
            float time = Time.time;
            
            while (Time.time < time + dashTime)
            {
                Vector3 forword = transform.forward;
                forword.y = 0;

                characterController.Move(forword * dashSpeed * Time.deltaTime);
                yield return null;
            }
            dashColdTimeCur = time + dashColdDown;
        }
    }
    public void AttackCancel()
    {
        attacking = false;
    }
    public void getdamage()
    {
        animator.SetBool("getHit", true);
    }
    public void youDead()
    {
        isDie = true;
        animator.SetBool("Die",isDie);
    }
    
}
