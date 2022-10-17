using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeteorite : MonoBehaviour
{
    private GameObject Meteorite, main;
    public bool haveMeteorite, haveMonster;
    public int PlayerID, HP = 1;

    // Start is called before the first frame update
    void Start()
    {
        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        main = GameObject.Find("GameService");
        haveMeteorite = false;
        haveMonster = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        if (Coll.gameObject.tag == "Monster")
        {
            haveMonster = true;
        }

        if (Coll.gameObject.tag == "Monster" && haveMeteorite)
        {
            main.GetComponent<Finish>().playernum -= 1;
            HP = 0;
            attack();
            Destroy(gameObject);
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
        if (Coll.gameObject.tag == "Meteorite")
        {
            haveMeteorite = true;
        }

        if (Coll.gameObject.tag == "Meteorite" && haveMonster)
        {
            main.GetComponent<Finish>().playernum -= 1;
            HP = 0;
            attack();
            Destroy(gameObject);
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
}
