using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject Player;
    public int playernum = 5;
    private float TimeRun = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(playernum == 1)
        {
            TimeRun -= Time.fixedDeltaTime;
            if (TimeRun <= 0)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                SceneManager.LoadScene("WinMenu");
            }
        }

        if(Player == null)
        {
            TimeRun -= Time.fixedDeltaTime;
            if (TimeRun <= 0)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                SceneManager.LoadScene("LoseMenu");
            }
        }
    }
}
