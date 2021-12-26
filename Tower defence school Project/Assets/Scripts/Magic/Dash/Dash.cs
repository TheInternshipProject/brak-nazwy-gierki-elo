using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private ParticleSystem dash;
     // how fast you want to dash
    public float dashSpeed;
    // how long you want to dash for
    public float dashTime;
    // cooldown for when dash ability is ready.
    private float dashCooldown;
    // resets the cooldown to specified time. higher numbers = longer CD
    public float resetDashCooldown;


    //Stamina

    [Range(0, 4000)]
    public int stamina;
    public int maxStamina = 2000;
    public RectTransform uiBar;

    float percentUnit;
    float staminaPercentUnit;
    float DashcoolDown;
    float currentStaminaPercent;

     private void Start()
    {
        percentUnit = 1f / uiBar.anchorMax.x;
        staminaPercentUnit = 100f / maxStamina;
    }


    public void Update()
    {
        
        // starts the dash cooldown timer
        dashCooldown -= Time.deltaTime;
        // stops the timer once the cooldown is ready
        if (dashCooldown < 0) {
            dashCooldown = -1;
            stamina +=200;
        } else {
            stamina += 5;
            currentStaminaPercent = stamina * staminaPercentUnit;
        uiBar.anchorMax = new Vector2(currentStaminaPercent * percentUnit / 100f ,  uiBar.anchorMax.y);
            dashCooldown -= Time.deltaTime;
        }
    if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;
        
        

   //You can change KeyCode to whatever you like. currently on the NUMPAD 0 "zero" button
        if (Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift))
        {
            if (dashCooldown <= 0) {
                createDashParticle();
                StartCoroutine(DashPower());
    }
        }
    }

    public IEnumerator DashPower() {

        float startTime = Time.time;
        float localScaleX = transform.localScale.x;
   
        while (Time.time < startTime + dashTime) {

            float movementSpeed = dashSpeed * Time.deltaTime;

            if (Mathf.Sign(-localScaleX) == 1) {
                transform.Translate(movementSpeed, 0, 0);
            } else {
                transform.Translate(-movementSpeed, 0, 0);
            }

            dashCooldown = resetDashCooldown;
            stamina = 45;
            // stamina.SetStaminaToZero();
            // stamina.SlowlyRecoverStamina(resetDashCooldown);
            yield return null;
        }

    }

    void createDashParticle()
    {
        dash.Play();
    }

}
