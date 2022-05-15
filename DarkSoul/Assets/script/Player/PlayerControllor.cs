using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashColdDown;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button attackBTN;
    [SerializeField] private Button dodgeBTN;
    [HideInInspector]public bool useSwordHitBox;
    [SerializeField] private Text riseText;
    float dieTime=15;
    float dashColdTimeCur;
    int   attackCount;
    float gravity = -18f;
    bool  attacking;
    [HideInInspector]public bool  isDie;
    Vector3             gravityVectory=Vector3.zero;
    CharacterController characterController;
    Parameter     parameter;
    Animator            animator;
    private void Awake()
    {
        parameter = GetComponent<Parameter>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        useSwordHitBox = false;
        attackBTN.onClick.AddListener(Attack);
        dodgeBTN.onClick.AddListener(Dash);
    }
    private void Update()
    {
        CharacterMovetion();
        dieTimeCount();
    }
    public void CharacterMovetion()
    {
        if (!attacking && !isDie&&PlayerManager.playerManagerInstance.currBuild==null) {
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;
            if (horizontal > 0) horizontal = 1;
            if (vertical > 0) vertical = 1;

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

            if(horizontal!=0&& vertical!=0)
            {
                float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            }
            
        }
            
    }
    public void Attack()
    {
        if (!isDie) {
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
    public void Dash()
    {
        StartCoroutine(PlayerDash());
    }
    public IEnumerator PlayerDash()
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
        useSwordHitBox = false;
    }
    public void dieTimeCount()
    {
        if (isDie)
        {
            dieTime -= Time.deltaTime;
            riseText.gameObject.SetActive(true);
            riseText.text = "復活:"+ dieTime;
            if (dieTime <= 0)
            {
                parameter.CurrHP = parameter.MaxHP;
                isDie = false;
                dieTime = 15;
                animator.SetBool("Die", false);
                transform.position = startPoint.transform.position;
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
        else riseText.gameObject.SetActive(false);
    }
    public void swordHitBox()
    {
        useSwordHitBox = true;
    }
    public int totleAttack()
    {
        return parameter.AttackVaule;
    }
}
