using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic2 : MonoBehaviour
{
    public bool enemySlow = false;
    public bool playerSlow = false;
    
    private bool changePosition = false;
    private TrapMaster master;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        master = GameObject.Find("TrapMaster").GetComponent<TrapMaster>();
        if(master.randomgen == 1) {
            transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
            master.randomgen = -1;
            master.allowupdate = false;
        }
        else {
            transform.position = new Vector3(-10.0f, -2.0f, -8.5f);
        }
    }

    void FixedUpdate()
    {
        if(master.allowupdate) 
        {
            if(master.randomgen == 1) {
                transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), -8.5f);
                master.randomgen = -1;
                master.allowupdate = false;
            }
            else {
                transform.position = new Vector3(-10.0f, -2.0f, -8.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Monster") {
            if(playerSlow == false) {
                enemySlow = true;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezeEnemy", 2);
            }
        }
        else {
            if(enemySlow == false) {
                playerSlow = true;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezePlayer", 2);
            }
        }
    }

    private void UnfreezeEnemy()
    {
        enemySlow = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap2_Idle");
        master.activatedtrap[1] = 1;
        master.allowupdate = true;
    }

    private void UnfreezePlayer()
    {
        playerSlow = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap2_Idle");
        master.activatedtrap[1] = 1;
        master.allowupdate = true;
    }
}
