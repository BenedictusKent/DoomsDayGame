using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class OnlineOptMenu : MonoBehaviourPunCallbacks
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
            if(DataBase.isOpt)
            {
                DataBase.isOpt = false;
                Menu.SetActive(false);
            }
            else
            {
                DataBase.isOpt = true;
                Menu.SetActive(true);
            }
        }
    }

    public void Return()
    {
        DataBase.isOpt = false;
        Menu.SetActive(false);
    }

    public void OnClickLeaveRoom()
    {
        DataBase.isOpt = false;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickBackToRoom()
    {
        DataBase.isOpt = false;
        SceneManager.LoadScene("Room");
    }

    public void OnClickQuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
