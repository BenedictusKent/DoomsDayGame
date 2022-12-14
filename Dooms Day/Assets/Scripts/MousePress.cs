using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePress : MonoBehaviour
{
    public GameObject player;
    private GameObject Meteorite, GameService;

    public AudioClip throwball;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Meteorite = GameObject.FindGameObjectsWithTag("Meteorite")[0];
        GameService = GameObject.Find("GameService");

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = DataBase.EffectVolume1;
        _audioSource.clip = throwball;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(player != null)
        {
            if (player.GetComponent<GetMeteorite>().haveMeteorite == true)
            {
                _audioSource.Play();
                Meteorite.GetComponent<MeteoriteTo>().to = this.transform.parent.gameObject.GetComponent<GetMeteorite>().PlayerID;
                Meteorite.GetComponent<MeteoriteTo>().speed = 2;
                Meteorite.GetComponent<MeteoriteTo>().speedbool = true;
                player.GetComponent<GetMeteorite>().haveMeteorite = false;
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
