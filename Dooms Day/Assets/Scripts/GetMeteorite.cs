using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeteorite : MonoBehaviour
{
    private GameObject Meteorite, main;
    public bool haveMeteorite, havedog;
    public int Num, HP = 1;
    int to;
    // Start is called before the first frame update
    void Start()
    {
        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        main = GameObject.Find("GameService");
        haveMeteorite = false;
        havedog = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject.tag == "Meteorite")
        {
            haveMeteorite = true;
        }
        if (Coll.gameObject.tag == "whitedog")
        {
            havedog = true;
        }
        if (Coll.gameObject.tag == "whitedog" && haveMeteorite)
        {
            main.GetComponent<Finish>().playernum -= 1;
            HP = 0;
            attack();
            Destroy(gameObject);
        }
        if (Coll.gameObject.tag == "Meteorite" && havedog)
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
        if (Coll.gameObject.tag == "whitedog")
        {
            havedog = false;
        }
    }

    void attack()
    {
        Meteorite.GetComponent<MeteoriteTo>().AIfarnum = Num;
        Meteorite.GetComponent<MeteoriteTo>().speed = 2;
        Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
    }
}
