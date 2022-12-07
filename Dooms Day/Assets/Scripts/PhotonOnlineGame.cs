using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PhotonOnlineGame : MonoBehaviour
{
    public PhotonView pv1, pv2, pv3, pv4, pv5;
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            InitGame();
        }
    }

    public void InitGame()
    {
        DataBase.playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        switch(PhotonNetwork.LocalPlayer.ActorNumber){
            case 1: pv1.RequestOwnership(); pv1.gameObject.GetComponent<OnlinePlayerControl>().isAI = false; break;
            case 2: pv2.RequestOwnership(); pv2.gameObject.GetComponent<OnlinePlayerControl>().isAI = false; break;
            case 3: pv3.RequestOwnership(); pv3.gameObject.GetComponent<OnlinePlayerControl>().isAI = false; break;
            case 4: pv4.RequestOwnership(); pv4.gameObject.GetComponent<OnlinePlayerControl>().isAI = false; break;
            case 5: pv5.RequestOwnership(); pv5.gameObject.GetComponent<OnlinePlayerControl>().isAI = false; break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
