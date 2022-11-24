using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyEvents : UnityEvent<PlayerObject.PlayerState>
{

}

[ExecuteInEditMode]
public class PlayerObject : MonoBehaviour
{
    public SPUM_Prefabs _prefabs;
    public enum PlayerState
    {
        idle,
        run,
        attack,
        death,
    }
    private PlayerState _currentState;
    public PlayerState CurrentState{
        get => _currentState;
        set {
            _stateChanged.Invoke(value);
            _currentState = value;
        }
    }

    private MyEvents _stateChanged = new MyEvents();
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        _stateChanged.AddListener(PlayStateAnimation);
    }

    private void PlayStateAnimation(PlayerState state){
        _prefabs.PlayAnimation(state.ToString());
    }

    void Update()
    {
        /*
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.localPosition.y * 0.01f);
        switch(_currentState)
        {
            case PlayerState.idle:
            break;

            case PlayerState.run:
            break;
        }
        */
    }

    
    public void Idle()
    {
        _currentState = PlayerState.idle;
        PlayStateAnimation(_currentState);
    }

    public void Run()
    {
        _currentState = PlayerState.run;
        PlayStateAnimation(_currentState);
    }

    public void Attack()
    {
        _currentState = PlayerState.attack;
        PlayStateAnimation(_currentState);
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
