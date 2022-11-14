using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    public bool enemyFrozen = false;
    public bool playerFrozen = false;
    
    private bool changePosition = false;
    private TrapMaster master;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        master = GameObject.Find("TrapMaster").GetComponent<TrapMaster>();
        if(master.randomgen == 0) {
            transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
            master.randomgen = -1;
            master.allowupdate = false;
        }
        else {
            transform.position = new Vector3(-10.0f, 0f, -8.5f);
        }
    }

    void Update()
    {
        if(master.allowupdate) 
        {
            // Debug.Log(master.allowupdate);
            if(master.randomgen == 0) {
                transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
                master.randomgen = -1;
                master.allowupdate = false;
            }
            else {
                transform.position = new Vector3(-10.0f, 0f, -8.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Monster") {
            if(playerFrozen == false) {
                enemyFrozen = true;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezeEnemy", 2);
            }
        }
        else {
            if(enemyFrozen == false) {
                playerFrozen = true;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezePlayer", 2);
            }
        }
    }

    private void UnfreezeEnemy()
    {
        enemyFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        master.activatedtrap[0] = 0;
        master.allowupdate = true;
        // changePosition = true;
        // transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }

    private void UnfreezePlayer()
    {
        playerFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        master.activatedtrap[0] = 0;
        master.allowupdate = true;
        // changePosition = true;
        // transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
    }
}
