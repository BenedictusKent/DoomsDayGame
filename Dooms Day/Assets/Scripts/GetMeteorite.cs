using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMeteorite : MonoBehaviour
{
    public GameObject Meteorite1, Meteorite2;
    private GameObject Meteorite, GameService;
    public bool haveMeteorite, haveMonster;
    public int PlayerID, HP = 1;
    private PlayerObject _nowObj;

    public GameObject Particle01;
    private GameObject Particle01_copy;
    public GameObject Particle02;
    private GameObject Particle02_copy;
    public GameObject Particle03;
    private GameObject Particle03_copy;

    private bool isalive;

    public AudioClip playerdead01;
    private AudioSource _audioSource;

    private Image backp;

    // Start is called before the first frame update
    void Start()
    {
        switch(DataBase.mapID){
            case 0: Meteorite = Meteorite1; break;
            case 1: Meteorite = Meteorite2; break;
        }
        GameService = GameObject.Find("GameService");
        haveMeteorite = false;
        haveMonster = false;
        isalive = true;
        _nowObj = GetComponent<PlayerObject>();

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = 0.5f;
        _audioSource.clip = playerdead01;

        switch(DataBase.characterID)
        {
            case 3: {
                backp = GameObject.Find("PassiveSkill").transform.GetChild(0).GetComponent<Image>();
                break;
            }
        }
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
                if(HP == 1)
                {
                    _nowObj.Death();
                    GetComponent<PlayerControl>().isdie = true;
                    isalive = false;
                    Particle01_copy = Instantiate(Particle01);
                    Particle01_copy.transform.parent = transform;
                    Particle01_copy.transform.localPosition = Vector3.zero;
                    Particle02_copy = Instantiate(Particle02);
                    Particle02_copy.transform.parent = transform;
                    Particle02_copy.transform.localPosition = Vector3.zero;
                    _audioSource.Play();
                    Invoke("attackAndDestroy", 2f);
                }
                else
                {
                    HP -= 1;
                    backp.fillAmount += 0.5f;
                    Particle03_copy = Instantiate(Particle03);
                    Particle03_copy.transform.parent = transform;
                    Particle03_copy.transform.localPosition = Vector3.zero;
                    if (haveMeteorite)
                    {
                        attack();
                        haveMeteorite = false;
                    }
                }
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
                if(HP == 1)
                {
                    _nowObj.Death();
                    GetComponent<PlayerControl>().isdie = true;
                    isalive = false;
                    Particle01_copy = Instantiate(Particle01);
                    Particle01_copy.transform.parent = transform;
                    Particle01_copy.transform.localPosition = Vector3.zero;
                    Particle02_copy = Instantiate(Particle02);
                    Particle02_copy.transform.parent = transform;
                    Particle02_copy.transform.localPosition = Vector3.zero;
                    _audioSource.Play();
                    Invoke("attackAndDestroy", 2f);
                }
                else
                {
                    HP -= 1;
                    backp.fillAmount += 0.5f;
                    Particle03_copy = Instantiate(Particle03);
                    Particle03_copy.transform.parent = transform;
                    Particle03_copy.transform.localPosition = Vector3.zero;
                    if (haveMeteorite)
                    {
                        attack();
                        haveMeteorite = false;
                    }
                }
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
        GameService.GetComponent<Finish>().playernum -= 1;
        HP = 0;
        attack();
        Destroy(gameObject);
    }
}
