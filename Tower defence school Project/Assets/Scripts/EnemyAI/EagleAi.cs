using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleAi : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float damage;
    private float cooldownTimer = Mathf.Infinity;

    private bool PlayerInSight=false;
    private Collider2D PlayerCollision;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    AIPath aiPath;
    Path path;

    void Start(){
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position , target.position , OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path= p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if( path == null)
        return;

        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        }else{
            reachedEndOfPath = false;
        }

        cooldownTimer += Time.deltaTime;
        if(PlayerInSight)
        {

            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer =0;
                attackPlayer(PlayerCollision);
            }
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed  * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position , path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(aiPath.desiredVelocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(-0.1349895f,0.1481311f , 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(0.1349895f,0.1481311f , 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackPlayer(collision);
    }

    private void attackPlayer(Collider2D collision){
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerCollision = collision;
            PlayerInSight= true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           PlayerInSight=false;
        }
    }
    
}
