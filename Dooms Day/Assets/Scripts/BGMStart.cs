using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStart : MonoBehaviour
{
    public GameObject StartBGM;
    private GameObject StartBGMClone;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("StartBGM") == null){
            StartBGMClone = Instantiate(StartBGM);
            StartBGMClone.name = "StartBGM";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}