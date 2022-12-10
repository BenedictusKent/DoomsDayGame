using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public GameObject Grid, Grid2;
    public GameObject Enemy1, Enemy2;
    public GameObject Meteorite1, Meteorite2;

    // Start is called before the first frame update
    void Start()
    {
        switch(DataBase.mapID)
        {
            case 0: {
                Grid.SetActive(true);
                Grid2.SetActive(false);
                Enemy1.SetActive(true);
                Enemy2.SetActive(false);
                Meteorite1.SetActive(true);
                Meteorite2.SetActive(false);
                break;
            }
            case 1: {
                Grid.SetActive(false);
                Grid2.SetActive(true);
                Enemy1.SetActive(false);
                Enemy2.SetActive(true);
                Meteorite1.SetActive(false);
                Meteorite2.SetActive(true);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
