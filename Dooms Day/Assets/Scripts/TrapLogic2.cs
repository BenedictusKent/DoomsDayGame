using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic2 : MonoBehaviour
{
    public bool enemySlow = false;
    public bool playerSlow = false;

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
            enemySlow = true;
            animator.SetTrigger("isTriggered");
            Invoke("UnfreezeEnemy", 2);
        }
        else {
            playerSlow = true;
            animator.SetTrigger("isTriggered");
            Invoke("UnfreezePlayer", 2);
        }
    }

    private void UnfreezeEnemy()
    {
        enemySlow = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap2_Idle");
        transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }

    private void UnfreezePlayer()
    {
        playerSlow = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap2_Idle");
        transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }
}
