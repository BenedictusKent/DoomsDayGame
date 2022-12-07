using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using HashTable = ExitGames.Client.Photon.Hashtable;

public class OnlineMousePress : MonoBehaviour
{
    Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    public GameObject p1, p2, p3, p4, p5;
    private GameObject Meteorite, GameService;
    public int PlayerID;

    public AudioClip throwball;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNum.Add(1, p1);
        PlayerNum.Add(2, p2);
        PlayerNum.Add(3, p3);
        PlayerNum.Add(4, p4);
        PlayerNum.Add(5, p5);

        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        GameService = GameObject.Find("GameService");

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = 1f;
        _audioSource.clip = throwball;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(PlayerNum[DataBase.playerID] != null)
        {
            if (PlayerNum[DataBase.playerID].GetComponent<OnlineGetMeteorite>().haveMeteorite == true)
            {
                _audioSource.Play();
                HashTable table = new HashTable();
                table.Add("Action", "Mto");
                table.Add("to", PlayerID);
                PhotonNetwork.LocalPlayer.SetCustomProperties(table);
                PlayerNum[DataBase.playerID].GetComponent<OnlineGetMeteorite>().haveMeteorite = false;
            }
        }
    }

    void OnMouseEnter()
    {
        GameService.GetComponent<CursorControl>().MoveOnPlayer();
    }

    void OnMouseExit()
    {
        GameService.GetComponent<CursorControl>().LeavePlayer();
    }
}
