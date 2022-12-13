using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject Player;
    public int playernum = 5;
    public bool enemyDead = false;
    //private float TimeRun = 0.25f;
    private bool isend;

    public GameObject Monster;
    public GameObject Monster2;
    public GameObject Particle01;
    private GameObject Particle01_copy;

    public AudioClip monsterdead01;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        isend = false;

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = DataBase.EffectVolume1;
        _audioSource.clip = monsterdead01;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(!isend)
        {
            if(Player == null)
            {
                isend = true;
                Invoke("toLoseMenu", 0.25f);
            }
            else if(playernum == 1)
            {
                isend = true;
                enemyDead = true;
                Particle01_copy = Instantiate(Particle01);
                switch(DataBase.mapID)
                {
                    case 0: {
                        Particle01_copy.transform.parent = Monster.transform;
                        Particle01_copy.transform.localPosition = Vector3.zero;
                        break;
                    }
                    case 1: {
                        Particle01_copy.transform.parent = Monster2.transform;
                        Particle01_copy.transform.localPosition = Vector3.zero;
                        break;
                    }
                }
                _audioSource.Play();
                Invoke("toWinMenu", 2.25f);
            }
        }
    }

    void toWinMenu()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("WinMenu");
    }

    void toLoseMenu()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("LoseMenu");
    }
}
