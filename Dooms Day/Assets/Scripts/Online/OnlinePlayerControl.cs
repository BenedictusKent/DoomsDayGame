using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerControl : MonoBehaviour
{
    public bool isAI = false;

    public PlayerObject _nowObj;
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

    private PhotonView _pv;

    private GameObject GameService;

    // Start is called before the first frame update
    void Start()
    {
        rb = _nowObj.GetComponent<Rigidbody2D>();
        orgspeed = speed;
        frozen = false;
        slow = false;
        slow2 = false;
        isdie = false;
        GameService = GameObject.Find("GameService");
        _pv = this.gameObject.GetComponent<PhotonView>();

        if(isAI)
        {
            Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
            InvokeRepeating("ChangeMove", 0.5f, MoveNeedTime);

            _audioSource = this.gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.volume = 1f;
            _audioSource.clip = throwball;
        }

        Invoke("checkIsMine", 0.1f);
    }

    void checkIsMine()
    {
        if(!_pv.IsMine){
            Destroy(this.gameObject.GetComponent<OnlineGetMeteorite>());
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
            Destroy(this);
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
                if(_pv.IsMine){
                    HumanControl();
                }
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
        if (_nowObj.GetComponent<OnlineGetMeteorite>().haveMeteorite == true)
        {
            LocateTime -= Time.fixedDeltaTime;
            if(LocateTime <= 0)
            {
                LocateTime = LocateNeedTime;
                _nowObj.GetComponent<OnlineGetMeteorite>().haveMeteorite = false;
                attack();
            }
        }
    }

    void attack()
    {
        if(GameService.GetComponent<OnlineFinish>().playnum > 1){
            _audioSource.Play();
            Meteorite.GetComponent<OnlineMeteoriteTo>().AInum = _nowObj.GetComponent<OnlineGetMeteorite>().PlayerID;
        }
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
