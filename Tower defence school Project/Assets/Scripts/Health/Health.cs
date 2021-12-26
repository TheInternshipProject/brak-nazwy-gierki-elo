using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private HealthBarEnemy healthBar;
    public float currentHealth {get; private set; }
    private bool dead =false;
    public static bool isPlayerDead = false;
    private float timer = 0;
    private float timerMax = 5;

    [SerializeField] private AudioClip deathSound;

    public bool isPassive;

    public GameObject HitPoints;

    private Animator anim;

    public GameObject PlayerDeadMenu;

    private void Awake()
    {
        isPlayerDead = false;
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    private void Start(){
       if(getObjectTag() == "Enemy" || getObjectTag() == "PassiveEnemy")  healthBar.SetHealth(currentHealth  , startingHealth);
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage , 0 , startingHealth);

        if (getObjectTag() == "Enemy" || getObjectTag() == "PassiveEnemy")
        {
            healthBar.SetHealth(currentHealth, startingHealth);
            GameObject points = Instantiate(HitPoints, transform.position, Quaternion.identity);
            points.transform.GetChild(0).GetComponent<TextMesh>().text = _damage.ToString();
        }
        if (currentHealth > 0 && !dead)
        {
            GameObject points = Instantiate(HitPoints, transform.position, Quaternion.identity);
            points.transform.GetChild(0).GetComponent<TextMesh>().text = _damage.ToString();
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
            if(!dead && getObjectTag() != "Enemy" && getObjectTag() != "PassiveEnemy")
            {
                SoundManager.instance.PlaySound(deathSound);
                anim.SetTrigger("die");
                GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
                dead = true;
                isPlayerDead = true;

                Time.timeScale = 0.6f;
                PlayerDeadMenu.SetActive(true);

                GameObject.Find("EnemyBot").GetComponent<MeleeEnemy>().enabled = false;
                GameObject.Find("UiCanvas").GetComponent<PauseMenu>().enabled = false;

                GameObject.Find("Player").GetComponent<CharacterAttack>().enabled = false;
                GameObject.Find("Player").GetComponent<Dash>().enabled = false;
            }
            else if(getObjectTag() == "Enemy" || getObjectTag() == "PassiveEnemy"){
                anim.SetTrigger("die");
                // GameObject.Find("EnemyBot").GetComponent<MeleeEnemy>().enabled = false;
                dead =true;
            }

        }
    }

    private void Update()
    {
        if(dead && getObjectTag() == "Enemy")
            if(Delay(0.7f))
                 Destroy(gameObject);
        if(Input.GetKeyDown(KeyCode.H))
            TakeDamage(1);
    }

    private string getObjectTag(){
        return gameObject.tag;
    }

    public bool Delay(float seconds)
        {
            timerMax = seconds;
            timer += Time.deltaTime;
        
            if (timer >= timerMax)
            {
                return true; //max reached - waited x - seconds
            }
        
            return false;
    }

}
