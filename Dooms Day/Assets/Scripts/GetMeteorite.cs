using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMeteorite : MonoBehaviour
{
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

    public AudioClip playerdead01, audioSkill05;
    private AudioSource _audioSource, _audioSourceSkill05;

    private Image backp;

    public bool isSkill05;

    // Start is called before the first frame update
    void Start()
    {
        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        GameService = GameObject.Find("GameService");
        haveMeteorite = false;
        haveMonster = false;
        isalive = true;
        isSkill05 = false;
        _nowObj = GetComponent<PlayerObject>();

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = DataBase.EffectVolume2;
        _audioSource.clip = playerdead01;

        _audioSourceSkill05 = this.gameObject.AddComponent<AudioSource>();
        _audioSourceSkill05.loop = false;
        _audioSourceSkill05.volume = DataBase.EffectVolume1;
        _audioSourceSkill05.clip = audioSkill05;

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
                    Particle01_copy = Instantiate(Particle01, transform);
                    Particle02_copy = Instantiate(Particle02, transform);
                    _audioSource.Play();
                    Invoke("attackAndDestroy", 2f);
                }
                else
                {
                    HP -= 1;
                    backp.fillAmount += 0.5f;
                    Particle03_copy = Instantiate(Particle03, transform);
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
                if(Meteorite.GetComponent<MeteoriteTo>().newOwner != PlayerID){
                    Meteorite.GetComponent<MeteoriteTo>().preOwner = Meteorite.GetComponent<MeteoriteTo>().newOwner;
                    Meteorite.GetComponent<MeteoriteTo>().newOwner = PlayerID;
                }

                if(isSkill05){
                    if(lottery(4)){
                        int preOwner = Meteorite.GetComponent<MeteoriteTo>().preOwner;
                        if(Meteorite.GetComponent<MeteoriteTo>().PlayerNum[preOwner] != null){
                            _audioSourceSkill05.Play();
                            Meteorite.GetComponent<MeteoriteTo>().to = preOwner;
                            Meteorite.GetComponent<MeteoriteTo>().speed = 2;
                            Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
                        }
                    }
                }
            }

            if (Coll.gameObject.tag == "Meteorite" && haveMonster)
            {
                if(HP == 1)
                {
                    _nowObj.Death();
                    GetComponent<PlayerControl>().isdie = true;
                    isalive = false;
                    Particle01_copy = Instantiate(Particle01, transform);
                    Particle02_copy = Instantiate(Particle02, transform);
                    _audioSource.Play();
                    Invoke("attackAndDestroy", 2f);
                }
                else
                {
                    HP -= 1;
                    backp.fillAmount += 0.5f;
                    Particle03_copy = Instantiate(Particle03, transform);
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

    bool lottery(int p)
    {
        if(Random.Range(1, p + 1) == 1){
            return true;
        }
        return false;
    }

}
