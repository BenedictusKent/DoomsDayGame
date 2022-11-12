using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    public bool enemyFrozen = false;
    public bool playerFrozen = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Monster") {
            enemyFrozen = true;
            animator.SetTrigger("isTriggered");
            Invoke("UnfreezeEnemy", 2);
        }
        else {
            playerFrozen = true;
            animator.SetTrigger("isTriggered");
            Invoke("UnfreezePlayer", 2);
        }
    }

    private void UnfreezeEnemy()
    {
        enemyFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }

    private void UnfreezePlayer()
    {
        playerFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }
}