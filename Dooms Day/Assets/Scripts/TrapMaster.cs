using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMaster : MonoBehaviour
{
    public int randomgen = -1;
    public int[] activatedtrap = {-1, -1};
    public bool allowupdate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        randomgen = Random.Range(0, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activatedtrap[0] == 0 && activatedtrap[1] == -1) {
            randomgen = Random.Range(0, 2);
            activatedtrap[0] = -1;
            activatedtrap[1] = -1;
        }
        else if(activatedtrap[0] == -1 && activatedtrap[1] == 1) {
            randomgen = Random.Range(0, 2);
            activatedtrap[0] = -1;
            activatedtrap[1] = -1;
        }
    }
}
