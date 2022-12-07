using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class OnlineEndMenu : MonoBehaviourPunCallbacks
{
    public Text WinnerName;

    // Start is called before the first frame update
    void Start()
    {
        WinnerName.text = DataBase.WinnerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBackToRoom()
    {
        SceneManager.LoadScene("Room");
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
