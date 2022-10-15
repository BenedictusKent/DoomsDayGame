using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int playernum = 5;
    float TimeRun = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playernum == 1)
        {
            TimeRun -= Time.deltaTime;
            if (TimeRun <= 0)
            {
                SceneManager.LoadScene("WinMenu");
            }
        }
    }
}
