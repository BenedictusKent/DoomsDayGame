using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PhotonNetworkStatus : MonoBehaviour
{
    public TMP_Text pingText;
    private int ping;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnected){
            InvokeRepeating("UpdateNetworkStatus", 1f, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateNetworkStatus() {
		//string status = PhotonNetwork.NetworkStatisticsToString();
		ping = PhotonNetwork.GetPing();
        pingText.text = ping + "ms";
	}
}
