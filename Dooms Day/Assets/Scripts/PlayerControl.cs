using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerObject _nowObj;

    public float speed = 10.0f;

    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = _nowObj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal"); //A,D
        movement.y = Input.GetAxis("Vertical"); //W,S

        if(movement.x != 0 && movement.y != 0)
        {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

        if(movement.x == 0 && movement.y == 0)
        {
            _nowObj.Idle();
        }
        else
        {
            _nowObj.Run();
        }

        if(movement.x > 0)
        {
            _nowObj.TurnRight();
        }
        else if(movement.x < 0)
        {
            _nowObj.TurnLeft();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
