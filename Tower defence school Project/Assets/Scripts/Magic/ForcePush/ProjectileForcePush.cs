using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForcePush : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    public Vector3 Offset;

    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed , 0 , 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5){
            if(lifetime > 7)
                gameObject.SetActive(false);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        // anim.SetTrigger("explode");

        if(collision.tag == "Enemy"){
            gameObject.SetActive(false);
            Vector3 temp = new Vector3(1.0f,0,0);

            collision.transform.position += temp;
            collision.GetComponent<Health>().TakeDamage(1);
            
        }
        else gameObject.SetActive(false);
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

    public void stage2Animation(){
        anim.SetTrigger("Stage2");
    }
}
