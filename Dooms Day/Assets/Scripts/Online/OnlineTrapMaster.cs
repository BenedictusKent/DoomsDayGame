using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineTrapMaster : MonoBehaviour
{
    public int randomgen = -1;

    private OnlineTrapLogic trap;
    private OnlineTrapLogic2 trap2;
    private OnlineTrapLogic3 trap3;
    
    private PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        if(!_pv.IsMine){
            Destroy(this);
        }

        trap = GameObject.Find("Trap").GetComponent<OnlineTrapLogic>();
        trap2 = GameObject.Find("Trap2").GetComponent<OnlineTrapLogic2>();
        trap3 = GameObject.Find("Trap3").GetComponent<OnlineTrapLogic3>();
        randomgen = Random.Range(0, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(trap.trapActivated == 1) {
            trap.trapActivated = -1;
            randomgen = Random.Range(0, 3);
            ChangeUpdate(randomgen);
        }
        else if(trap2.trapActivated == 1) {
            trap2.trapActivated = -1;
            randomgen = Random.Range(0, 3);
            ChangeUpdate(randomgen);
        }
        else if(trap3.trapActivated == 1) {
            trap3.trapActivated = -1;
            randomgen = Random.Range(0, 3);
            ChangeUpdate(randomgen);
        }
    }

    void ChangeUpdate(int randomgen)
    {
        if(randomgen == 0) {
            trap.allowUpdate = true;
            trap2.allowReset = true;
            trap3.allowReset = true;
        }
        else if(randomgen == 1) {
            trap2.allowUpdate = true;
            trap.allowReset = true;
            trap3.allowReset = true;
        }
        else if(randomgen == 2) {
            trap3.allowUpdate = true;
            trap.allowReset = true;
            trap2.allowReset = true;
        }
    }
}
