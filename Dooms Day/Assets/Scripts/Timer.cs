using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int time_int = 3;
    public TMP_Text time_UI;
    private bool start_timer = true, start_game = false;

    // Start is called before the first frame update
    void Start()
    {
        DataBase.isPause = true;
        DataBase.PauseMenuAvailable = false;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(start_timer && !start_game)
        {
            if(time_int == 0)
            {
                time_UI.text = "Start!";
            }
            else if(time_int == -1)
            {
                time_UI.gameObject.SetActive(false);
                DataBase.isPause = false;
                DataBase.PauseMenuAvailable = true;
                Time.timeScale = 1f;
                start_game = true;
            }
            else
            {
                time_UI.text = time_int + "";
            }
            StartCoroutine(timer());
            start_timer = false;
        }

    }

    IEnumerator timer()
    {
        yield return new WaitForSecondsRealtime(1);
        time_int--;
        start_timer = true;
    }
}
