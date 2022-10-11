using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public SPUM_Prefabs _prefabs;
    public enum PlayerState
    {
        idle,
        move,
        attack,
        death,
    }
    public PlayerState _playerState = PlayerState.idle;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        /*
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.localPosition.y * 0.01f);
        switch(_playerState)
        {
            case PlayerState.idle:
            break;

            case PlayerState.move:
            break;
        }
        */
    }

    public void Idle()
    {
        _playerState = PlayerState.idle;
        _prefabs.PlayAnimation(0);
    }

    public void Move()
    {
        _playerState = PlayerState.move;
        _prefabs.PlayAnimation(1);
    }

    public void TurnRight()
    {
        _prefabs.transform.localScale = new Vector3(-1,1,1);
    }

    public void TurnLeft()
    {
        _prefabs.transform.localScale = Vector3.one;
    }
}
