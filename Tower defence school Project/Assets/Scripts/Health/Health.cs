using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set; }
    private bool dead =false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage , 0 , startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //StartCoroutine(Invunerability());
        }
        else 
        {
            if(!dead)
            {
                anim.SetTrigger("die");
                GetComponent<CharacterController>().enabled = false;
                dead = true;

                GetComponent<MeleeAttack>().enabled = false;
            }

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
}
