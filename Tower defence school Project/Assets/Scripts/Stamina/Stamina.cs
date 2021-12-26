using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Range(0, 4000)]
    public int stamina;
    public int maxStamina = 2000;
    public RectTransform uiBar;

    private float timer = 0;

    private float timerMax = 5;

    float percentUnit;
    float staminaPercentUnit;
    float DashcoolDown;

    bool isRecoverNeeded = false;

    private void Start()
    {
        percentUnit = 1f / uiBar.anchorMax.x;
        staminaPercentUnit = 100f / maxStamina;
    }


    private void FixedUpdate()
    {
        if (stamina < maxStamina)
        {
            if(Delay(DashcoolDown))
            {
                stamina += 300;
            }
            
        }
        
        if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;

        float currentStaminaPercent = stamina * staminaPercentUnit;

        uiBar.anchorMax = new Vector2(currentStaminaPercent * percentUnit / 100f ,  uiBar.anchorMax.y);

    }

    private bool checkIfStaminaIsFull(){
        return stamina == maxStamina ? true : false;
    }

    public void SetStaminaToZero()
    {
        stamina = 0;
    }

    public void SlowlyRecoverStamina(float coolDown)
    {
        isRecoverNeeded = true;
        DashcoolDown = coolDown;
    }

    private void onValidate()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;
    }

    public bool Delay(float seconds)
    {
        float howMuchStaminaForSecond = maxStamina / seconds;
        timerMax = seconds;

        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            return true;
        }
        else return false;
    }

    IEnumerator DoCheck()
    {
        for (int i=0; i< DashcoolDown; i++)
        {
            stamina += 300;
            yield return new WaitForSeconds(5);
        }
    }


}
