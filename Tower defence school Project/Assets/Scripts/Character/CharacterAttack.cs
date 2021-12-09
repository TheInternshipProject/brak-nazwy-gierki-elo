using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject[] fireballs;
    Dash dash;

    private Animator anim;
    private CharacterController CharacterController;
    private float cooldownTimer = Mathf.Infinity;
   
    void Awake()
    {
        dash = GetComponent<Dash>();
        anim = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1) && cooldownTimer > attackCooldown )
        {
            Attack();
        }

        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown )
        {
            MeleeSwordAttack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void MeleeSwordAttack()
    {
        cooldownTimer = 0;
        Sword.transform.position = AttackPoint.position;
        Sword.GetComponent<MeleeAttack>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void Attack() 
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = FirePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for(int i = 0 ;i <fireballs.Length ; i++)
        {
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
