using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject Sword;
    [SerializeField] private Transform AttackPoint;

    [SerializeField] private AudioClip meleeSound;

    [SerializeField] private Transform player;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        LookAtPlayer();

        cooldownTimer += Time.deltaTime;
        if(PlayerInSight())
        {

            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer =0;
                anim.SetTrigger("meleeAttack");
                MeleeSwordAttack();
            }
        }
        
    }

    private void MeleeSwordAttack()
    {
        SoundManager.instance.PlaySound(meleeSound);
        cooldownTimer = 0;
        Sword.transform.position = AttackPoint.position;
        Sword.GetComponent<MeleeAtackEnemy>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x *colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range , boxCollider.bounds.size.y , boxCollider.bounds.size.z),
         0 , Vector2.left, 0 ,  playerLayer);

        if(hit.collider != null) 
        playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * range , boxCollider.bounds.size.y , boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight()) playerHealth.TakeDamage(damage);
    }

    public bool isFlipped = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

    }
}
