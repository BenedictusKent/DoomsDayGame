using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && DataBase.PauseMenuAvailable)
        {
            if(DataBase.isPause)
            {
                DataBase.isPause = false;
                Time.timeScale = 1f;
                Menu.SetActive(false);
            }
            else
            {
                DataBase.isPause = true;
                Time.timeScale = 0f;
                Menu.SetActive(true);
            }
        }
    }

    public void Return()
    {
        DataBase.isPause = false;
        Time.timeScale = 1f;
        Menu.SetActive(false);
    }

    public void MainMenu()
    {
        DataBase.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
