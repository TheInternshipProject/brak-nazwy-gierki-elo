using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private HealthBarEnemy healthBar;
    public float currentHealth {get; private set; }
    private bool dead =false;

    private Animator anim;

    public GameObject PlayerDeadMenu;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    private void Start(){
        healthBar.SetHealth(currentHealth  , startingHealth);
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage , 0 , startingHealth);

        healthBar.SetHealth(currentHealth -1, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");

            //StartCoroutine(Invunerability());
            Time.timeScale = 1f;
            PlayerDeadMenu.SetActive(false);
            GameObject.Find("UiCanvas").GetComponent<PauseMenu>().enabled = true;
            GameObject.Find("Player").GetComponent<CharacterAttack>().enabled = true;
            GameObject.Find("Player").GetComponent<Dash>().enabled = true;
        }
        else 
        {
            if(!dead)
            {
                anim.SetTrigger("die");
                GetComponent<CharacterController>().enabled = false;
                dead = true;

                Time.timeScale = 0f;
                PlayerDeadMenu.SetActive(true);

                GameObject.Find("UiCanvas").GetComponent<PauseMenu>().enabled = false;

                GameObject.Find("Player").GetComponent<CharacterAttack>().enabled = false;
                GameObject.Find("Player").GetComponent<Dash>().enabled = false;
            }

        }
    }
}
