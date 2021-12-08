using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private CharacterController CharacterController;
    private float cooldownTimer = Mathf.Infinity;
   
    void Awake()
    {
        anim = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown )
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack() 
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[0].transform.position = FirePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
