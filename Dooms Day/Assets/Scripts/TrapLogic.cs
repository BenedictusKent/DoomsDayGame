using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    public bool enemyFrozen = false;
    public bool playerFrozen = false;
    public bool allowReset = false;
    public bool allowUpdate = false;
    public int trapActivated = -1;
    public GameObject warning;

    private float x = -10.0f;
    private float y = -10.0f;
    private TrapMaster master;
    Animator animator;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        master = GameObject.Find("TrapMaster").GetComponent<TrapMaster>();
        if(master.randomgen == 0) {
            x = Random.Range(-6f, 6f);
            y = Random.Range(-3f, 3f);
            warning.transform.position = new Vector3(x, y, -8.5f);
            Invoke("AppearingTrap", 1);
        }
        else {
            transform.position = new Vector3(-10.0f, 0f, -8.5f);
        }
    }

    private void AppearingTrap()
    {
        warning.transform.position = new Vector3(-10.0f, -4.0f, -8.5f);
        transform.position = new Vector3(x, y, -8.5f);
    }

    void FixedUpdate()
    {
        if(allowUpdate) 
        {
            allowUpdate = false;
            x = Random.Range(-6f, 6f);
            y = Random.Range(-3f, 3f);
            warning.transform.position = new Vector3(x, y, -8.5f);
            Invoke("AppearingTrap", 1);
            trapActivated = -1;
        }
        else if(allowReset) 
        {
            allowReset = false;
            transform.position = new Vector3(-10.0f, 0f, -8.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(trapActivated == -1)
        {
            if(collider.tag == "Monster") {
                trapActivated = 0;
                enemyFrozen = true;
                animator.SetTrigger("isTriggered");
                Invoke("UnfreezeEnemy", 2);
            }
            else if(collider.tag == "Player"){
                player = collider.gameObject;
                if(!player.GetComponent<PlayerControl>().isdie)
                {
                    trapActivated = 0;
                    player.GetComponent<PlayerControl>().frozen = true;
                    //playerFrozen = true;
                    animator.SetTrigger("isTriggered");
                    Invoke("UnfreezePlayer", 2);
                }
            }
        }
    }

    private void UnfreezeEnemy()
    {
        enemyFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        transform.position = new Vector3(-10.0f, 0f, -23.0f);
        trapActivated = 1;
    }

    private void UnfreezePlayer()
    {
        player.GetComponent<PlayerControl>().frozen = false;
        //playerFrozen = false;
        animator.ResetTrigger("isTriggered");
        animator.Play("Trap_Idle");
        transform.position = new Vector3(-10.0f, 0f, -23.0f);
        trapActivated = 1;
    }
}
