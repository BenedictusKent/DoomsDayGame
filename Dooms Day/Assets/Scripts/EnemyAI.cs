using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform enemyGFX;
    public GameObject trapObject;
    public float speed = 200f;
    public float nextWaypointDistance = 1.2f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool frozen = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = trapObject.GetComponent<Animator>();

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

        if(!frozen)
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
        if(frozen)
            rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Trap"))
        {
            frozen = true;
            animator.SetTrigger("isTriggered");
            Invoke("unfreezeEnemy", 2);
        }
    }

    private void unfreezeEnemy()
    {
        frozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
    }
}
