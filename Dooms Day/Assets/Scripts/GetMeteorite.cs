using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeteorite : MonoBehaviour
{
    private GameObject Meteorite, main, GameService;
    public bool haveMeteorite, haveMonster;
    public int PlayerID, HP = 1;
    private PlayerObject _nowObj;

    public GameObject Particle01;
    private GameObject Particle01_copy;
    public GameObject Particle02;
    private GameObject Particle02_copy;

    private bool isalive;

    public AudioClip playerdead01;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        main = GameObject.Find("GameService");
        haveMeteorite = false;
        haveMonster = false;
        isalive = true;
        _nowObj = GetComponent<PlayerObject>();

        GameService = GameObject.Find("GameService");

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = 0.5f;
        _audioSource.clip = playerdead01;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        if (isalive)
        {
            if (Coll.gameObject.tag == "Monster")
            {
                haveMonster = true;
            }

            if (Coll.gameObject.tag == "Monster" && haveMeteorite)
            {
                _nowObj.Death();
                GetComponent<PlayerControl>().isdie = true;
                isalive = false;
                GameService.GetComponent<CursorControl>().LeavePlayer();
                Particle01_copy = Instantiate(Particle01);
                Particle01_copy.transform.parent = transform;
                Particle01_copy.transform.localPosition = Vector3.zero;
                Particle02_copy = Instantiate(Particle02);
                Particle02_copy.transform.parent = transform;
                Particle02_copy.transform.localPosition = Vector3.zero;
                _audioSource.Play();
                Invoke("attackAndDestroy", 2f);
            }
        }
    }

    void OnCollisionExit2D(Collision2D Coll)
    {
        if (Coll.gameObject.tag == "Monster")
        {
            haveMonster = false;
        }
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (isalive)
        {
            if (Coll.gameObject.tag == "Meteorite")
            {
                haveMeteorite = true;
            }

            if (Coll.gameObject.tag == "Meteorite" && haveMonster)
            {
                _nowObj.Death();
                GetComponent<PlayerControl>().isdie = true;
                isalive = false;
                GameService.GetComponent<CursorControl>().LeavePlayer();
                Particle01_copy = Instantiate(Particle01);
                Particle01_copy.transform.parent = transform;
                Particle01_copy.transform.localPosition = Vector3.zero;
                Particle02_copy = Instantiate(Particle02);
                Particle02_copy.transform.parent = transform;
                Particle02_copy.transform.localPosition = Vector3.zero;
                _audioSource.Play();
                Invoke("attackAndDestroy", 2f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.gameObject.tag == "Meteorite")
        {
            haveMeteorite = false;
        }
    }

    void attack()
    {
        Meteorite.GetComponent<MeteoriteTo>().AIfarnum = PlayerID;
        Meteorite.GetComponent<MeteoriteTo>().speed = 2;
        Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
    }

    void attackAndDestroy()
    {
        main.GetComponent<Finish>().playernum -= 1;
        HP = 0;
        attack();
        Destroy(gameObject);
    }
}
