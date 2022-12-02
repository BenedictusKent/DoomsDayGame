using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public TrapLogic trapvar;
    public TrapLogic2 trap2var;
    public Transform target;
    public Transform enemyGFX;
    public Finish endScript;
    public float speed = 200f;
    public float nextWaypointDistance = 1.2f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    
    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("IncreaseSpeed", 2f, 2f);
    }

    void IncreaseSpeed()
    {
        speed += 100;
        Debug.Log(speed);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, onPathComplete);
    }

    void onPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
            reachedEndOfPath = false;

        if(!trapvar.enemyFrozen)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.fixedDeltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if(distance < nextWaypointDistance)
                currentWaypoint++;

            if(rb.velocity.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
            else if(rb.velocity.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        if(trapvar.enemyFrozen)
            rb.velocity = Vector3.zero;
        else if(trap2var.enemySlow){
            rb.velocity /= 2;
        }
        if(endScript.enemyDead) {
            animator.SetTrigger("isDead");
            animator.Play("Enemy_Dead");
        }
    }
}
