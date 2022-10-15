using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIControl : MonoBehaviour
{
    public PlayerObject _nowObj;
    private GameObject Meteorite;

    public float speed = 10.0f;
    private float LocateTime = DataBase.AIattacktime, LocateNeedTime = DataBase.AIattacktime, MoveNeedTime = DataBase.AImovetime;

    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = _nowObj.GetComponent<Rigidbody2D>();
        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        InvokeRepeating("ChangeMove", 0.5f, MoveNeedTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        AIattack();
    }

    void AIattack()
    {
        if (_nowObj.GetComponent<GetMeteorite>().haveMeteorite == true)
        {
            LocateTime -= Time.fixedDeltaTime;
            if(LocateTime <= 0)
            {
                LocateTime = LocateNeedTime;
                _nowObj.GetComponent<GetMeteorite>().haveMeteorite = false;
                attack();
            }
        }
    }

    void attack()
    {
        Meteorite.GetComponent<MeteoriteTo>().AInum = _nowObj.GetComponent<GetMeteorite>().Num;
        Meteorite.GetComponent<MeteoriteTo>().speed = 2;
        Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
    }

    void ChangeMove()
    {
        movement.x = Random.Range(-1, 2); //A,D
        movement.y = Random.Range(-1, 2); //W,S

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
}
