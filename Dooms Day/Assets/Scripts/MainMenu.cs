using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SelectMenu");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("SelectMenu");
    }

    public void PlayGameEasy()
    {
        DataBase.AIattacktime = 1f;
        DataBase.AImovetime = 0.75f;
        SceneManager.LoadScene("Game");
    }

    public void PlayGameNormal()
    {
        DataBase.AIattacktime = 0.5f;
        DataBase.AImovetime = 0.5f;
        SceneManager.LoadScene("Game");
    }

    public void PlayGameHard()
    {
        DataBase.AIattacktime = 0.4f;
        DataBase.AImovetime = 0.4f;
        SceneManager.LoadScene("Game");
    }

    public void Again()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
