using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private float timer = 0;

    public static bool fireballSound = false;
 
    private float timerMax = 5;

    private BoxCollider2D boxCollider;
    private Animator anim;


    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if(hit){
            fireballSound = false; 
            return;
        } 
        fireballSound = true;
        if(Delay(1f)) fireballSound = false; 
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed , 0 , 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5){
            anim.SetTrigger("explode");
            if(lifetime > 7)
                gameObject.SetActive(false);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if(collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
          localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX , transform.localScale.y , transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
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