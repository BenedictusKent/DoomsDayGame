using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "OnlineGame")
        {
            Destroy(GameObject.Find("StartBGM"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
