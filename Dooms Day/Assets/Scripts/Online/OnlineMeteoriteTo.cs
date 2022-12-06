using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineMeteoriteTo : MonoBehaviour
{
    Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    public GameObject one, two, three, four, five;
    public int to, AInum, AIfarnum;
    public float speed = 1f;
    private float firstSpeed;
    public bool speedbool;

    private float fardistance, fartmp;
    private int farID;

    private PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNum.Add(1, one);
        PlayerNum.Add(2, two);
        PlayerNum.Add(3, three);
        PlayerNum.Add(4, four);
        PlayerNum.Add(5, five);

        _pv = this.gameObject.GetComponent<PhotonView>();
        if(!_pv.IsMine){
            Destroy(this);
        }

        randomvalue();
        speedbool = true;
        AInum = 0;
        AIfarnum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(AInum > 0)
        {
            checkrandomvalue(AInum);
            AInum = 0;
        }

        if(AIfarnum > 0)
        {
            farvalue(AIfarnum);
            AIfarnum = 0;
        }

        if(speedbool)
        {
            GetFirstSpeed();
            speedbool = false;
        }
        
        MoveBall();
    }

    void GetFirstSpeed()
    {
        firstSpeed = 15 * speed;
    }

    void MoveBall()
    {
        moveball(PlayerNum[to]);
    }

    void randomvalue()
    {
        to = Random.Range(1, 6);
    }

    void checkrandomvalue(int ID)
    {
        to = Random.Range(1, 6);
        while (dead() || to == ID)
        {
            to = Random.Range(1, 6);
        }
    }

    void farvalue(int ID)
    {
        fardistance = 0;

        for(int i = 1; i <= PlayerNum.Count; i++)
        {
            if(PlayerNum[i] != null && ID != i)
            {
                fartmp = Vector3.Distance(transform.position, PlayerNum[i].transform.position);
                if(fartmp > fardistance)
                {
                    fardistance = fartmp;
                    farID = i;
                }
            }
        }

        to = farID;
    }

    bool dead()
    {
        if(PlayerNum[to] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void moveball(GameObject x)
    {
        speed = calculateNewSpeed(x);
        transform.position = Vector3.Lerp(transform.position, x.transform.position + new Vector3(0, 0.7f, 0), speed * Time.deltaTime);
    }

    float calculateNewSpeed(GameObject x)
    {
        float tmp = Vector3.Distance(transform.position, x.transform.position + new Vector3(0, 0.7f, 0));

        if (tmp == 0)
            return tmp;
        else
            return (firstSpeed / tmp);
    }
}
