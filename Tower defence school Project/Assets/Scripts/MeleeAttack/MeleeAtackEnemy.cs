using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtackEnemy : MonoBehaviour
{
   private float direction;
   private BoxCollider2D boxCollider;
   private bool hit;
   private float lifetime;

   private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        lifetime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
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
        if(Mathf.Sign(localScaleX) == _direction)
          localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX , transform.localScale.y , transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
