using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isAI = false;

    public PlayerObject _nowObj;

    public GameObject Meteorite1, Meteorite2;
    private GameObject Meteorite;

    public float speed = 10.0f;
    private float LocateTime = DataBase.AIattacktime, LocateNeedTime = DataBase.AIattacktime, MoveNeedTime = DataBase.AImovetime;

    private Rigidbody2D rb;
    private Vector2 movement;

    public bool frozen, slow, slow2;
    public float orgspeed = 10.0f;

    public bool isdie;

    public AudioClip throwball;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = _nowObj.GetComponent<Rigidbody2D>();
        orgspeed = speed;
        frozen = false;
        slow = false;
        slow2 = false;
        isdie = false;

        if(isAI)
        {
            switch(DataBase.mapID){
                case 0: Meteorite = Meteorite1; break;
                case 1: Meteorite = Meteorite2; break;
            }
            InvokeRepeating("ChangeMove", 0.5f, MoveNeedTime);

            _audioSource = this.gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.volume = DataBase.EffectVolume1;
            _audioSource.clip = throwball;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!isdie)
        {
            if(frozen)
            {
                speed = 0f;
            }
            else if(slow)
            {
                speed = orgspeed / 2;
            }
            else if(slow2)
            {
                speed = orgspeed / 3;
            }
            else
            {
                speed = orgspeed;
            }

            if(isAI)
            {
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                AIattack();
            }
            else
            {
                HumanControl();
            }
        }
    }

    // Human
    void HumanControl()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //A,D
        movement.y = Input.GetAxisRaw("Vertical"); //W,S

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

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // AI
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
        _audioSource.Play();
        Meteorite.GetComponent<MeteoriteTo>().AInum = _nowObj.GetComponent<GetMeteorite>().PlayerID;
        Meteorite.GetComponent<MeteoriteTo>().speed = 2;
        Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
    }

    void ChangeMove()
    {
        if(!isdie)
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

}
