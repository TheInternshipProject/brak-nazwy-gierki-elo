using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
     // how fast you want to dash
    public float dashSpeed;
    // how long you want to dash for
    public float dashTime;
    // cooldown for when dash ability is ready.
    private float dashCooldown;
    // resets the cooldown to specified time. higher numbers = longer CD
    public float resetDashCooldown;

    public void Update()
    {
        // starts the dash cooldown timer
        dashCooldown -= Time.deltaTime;
        // stops the timer once the cooldown is ready
        if (dashCooldown < 0) {
            dashCooldown = -1;
        } else {
            dashCooldown -= Time.deltaTime;
        }

   //You can change KeyCode to whatever you like. currently on the NUMPAD 0 "zero" button
        if (Input.GetKey(KeyCode.Alpha2)) {
            if (dashCooldown <= 0) {
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
 
            yield return null;
        }

    }

}
