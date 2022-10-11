using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerObject _nowObj;

    public float speed = 10.0f;
    float HorizontalInput;
    float VerticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal"); //A,D
        VerticalInput = Input.GetAxis("Vertical"); //W,S

        if(HorizontalInput != 0 && VerticalInput != 0)
        {
            HorizontalInput *= 0.7f;
            VerticalInput *= 0.7f;
        }

        _nowObj.transform.Translate(Vector3.right * HorizontalInput * Time.deltaTime * speed);
        _nowObj.transform.Translate(Vector3.up * VerticalInput * Time.deltaTime * speed);

        if(HorizontalInput == 0 && VerticalInput == 0)
        {
            _nowObj.Idle();
        }
        else
        {
            _nowObj.Move();
        }

        if(HorizontalInput > 0)
        {
            _nowObj.TurnRight();
        }
        else if(HorizontalInput < 0)
        {
            _nowObj.TurnLeft();
        }
    }

}
