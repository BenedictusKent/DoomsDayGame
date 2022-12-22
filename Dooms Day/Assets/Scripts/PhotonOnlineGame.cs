using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class PhotonOnlineGame : MonoBehaviour
{
    public PhotonView pv1, pv2, pv3, pv4, pv5;
    Dictionary<int, PhotonView> PlayerPhotonView = new Dictionary<int, PhotonView>();

    public GameObject blueindex;

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
        PlayerPhotonView.Add(1, pv1);
        PlayerPhotonView.Add(2, pv2);
        PlayerPhotonView.Add(3, pv3);
        PlayerPhotonView.Add(4, pv4);
        PlayerPhotonView.Add(5, pv5);

        int count = 0;
        foreach(var kvp in PhotonNetwork.PlayerList){
            count++;
            if(kvp == PhotonNetwork.CurrentRoom.Players[PhotonNetwork.LocalPlayer.ActorNumber]){
                break;
            }
        }
        DataBase.playerID = count;
        switch(count){
            case 1: {
                pv1.RequestOwnership();
                pv1.gameObject.GetComponent<OnlinePlayerControl>().isAI = false;
                Instantiate(blueindex, pv1.gameObject.transform);
                break;
            }
            case 2: {
                pv2.RequestOwnership();
                pv2.gameObject.GetComponent<OnlinePlayerControl>().isAI = false;
                Instantiate(blueindex, pv2.gameObject.transform);
                break;
            }
            case 3: {
                pv3.RequestOwnership();
                pv3.gameObject.GetComponent<OnlinePlayerControl>().isAI = false;
                Instantiate(blueindex, pv3.gameObject.transform);
                break;
            }
            case 4: {
                pv4.RequestOwnership();
                pv4.gameObject.GetComponent<OnlinePlayerControl>().isAI = false;
                Instantiate(blueindex, pv4.gameObject.transform);
                break;
            }
            case 5: {
                pv5.RequestOwnership();
                pv5.gameObject.GetComponent<OnlinePlayerControl>().isAI = false;
                Instantiate(blueindex, pv5.gameObject.transform);
                break;
            }
        }

        int tempnum = 0;
        foreach(var kvp in PhotonNetwork.PlayerList){
            tempnum++;
            if(tempnum != count){
                PlayerPhotonView[tempnum].gameObject.GetComponentInChildren<TMP_Text>().text = kvp.NickName;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
