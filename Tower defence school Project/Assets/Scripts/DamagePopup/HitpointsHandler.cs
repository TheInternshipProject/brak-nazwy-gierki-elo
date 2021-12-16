using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointsHandler : MonoBehaviour
{
    private bool toDestroy= false;
    private string NameOfObjectToDestroy="HitpointsParent(Clone)";

    private float timer = 0;
    private float timerMax = 5;

    void Start()
    {
        transform.localPosition += new Vector3(0, 0.5f, 0);
        GameObject clone = GameObject.Find(NameOfObjectToDestroy);
        if(clone){
            toDestroy = true;
        }
        
    }

    private void Update(){
        if(toDestroy){
            if(Delay(1f)){
                toDestroy= false;
                Destroy(gameObject);
            }
        }
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
