using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteTo : MonoBehaviour
{
    Dictionary<int, GameObject> PlayerNum = new Dictionary<int, GameObject>();
    public GameObject one, two, three, four, five;
    public int to, AInum, AIfarnum;
    public float speed = 1f;
    public float firstSpeed;
    public bool speedbool;
    float fardistance, fartmp;
    int farID;
    // Start is called before the first frame update
    void Start()
    {
        PlayerNum.Add(1, one);
        PlayerNum.Add(2, two);
        PlayerNum.Add(3, three);
        PlayerNum.Add(4, four);
        PlayerNum.Add(5, five);
        randomvalue();
        speedbool = true;
        AInum = 0;
        AIfarnum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (AInum == 1)
        {
            checkrandomvalue(1);
            AInum = 0;
        }
        else if (AInum == 2)
        {
            checkrandomvalue(2);
            AInum = 0;
        }
        else if (AInum == 3)
        {
            checkrandomvalue(3);
            AInum = 0;
        }
        else if (AInum == 4)
        {
            checkrandomvalue(4);
            AInum = 0;
        }
        else if (AInum == 5)
        {
            checkrandomvalue(5);
            AInum = 0;
        }
        if (AIfarnum == 1)
        {
            farvalue(1);
            AIfarnum = 0;
        }
        else if (AIfarnum == 2)
        {
            farvalue(2);
            AIfarnum = 0;
        }
        else if (AIfarnum == 3)
        {
            farvalue(3);
            AIfarnum = 0;
        }
        else if (AIfarnum == 4)
        {
            farvalue(4);
            AIfarnum = 0;
        }
        else if (AIfarnum == 5)
        {
            farvalue(5);
            AIfarnum = 0;
        }
        if (speedbool)
        {
            GetFirstSpeed();
            speedbool = false;
        }
        MoveBall();
    }

    void GetFirstSpeed()
    {
        if (to == 1)
        {
            getfirstspeed();
        }
        else if (to == 2)
        {
            getfirstspeed();
        }
        else if (to == 3)
        {
            getfirstspeed();
        }
        else if (to == 4)
        {
            getfirstspeed();
        }
        else if (to == 5)
        {
            getfirstspeed();
        }
    }

    void MoveBall()
    {
        if (to == 1)
        {
            moveball(one);
        }
        else if (to == 2)
        {
            moveball(two);
        }
        else if (to == 3)
        {
            moveball(three);
        }
        else if (to == 4)
        {
            moveball(four);
        }
        else if (to == 5)
        {
            moveball(five);
        }
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
        if(one != null && ID != 1)
        {
            fartmp = Vector3.Distance(transform.position, one.transform.position);
            if(fartmp > fardistance)
            {
                fardistance = fartmp;
                farID = 1;
            }
        }
        if (two != null && ID != 2)
        {
            fartmp = Vector3.Distance(transform.position, two.transform.position);
            if (fartmp > fardistance)
            {
                fardistance = fartmp;
                farID = 2;
            }
        }
        if (three != null && ID != 3)
        {
            fartmp = Vector3.Distance(transform.position, three.transform.position);
            if (fartmp > fardistance)
            {
                fardistance = fartmp;
                farID = 3;
            }
        }
        if (four != null && ID != 4)
        {
            fartmp = Vector3.Distance(transform.position, four.transform.position);
            if (fartmp > fardistance)
            {
                fardistance = fartmp;
                farID = 4;
            }
        }
        if (five != null && ID != 5)
        {
            fartmp = Vector3.Distance(transform.position, five.transform.position);
            if (fartmp > fardistance)
            {
                fardistance = fartmp;
                farID = 5;
            }
        }
        to = farID;
    }

    bool dead()
    {
        if(one == null && to == 1)
        {
            return true;
        }
        else if(two == null && to == 2)
        {
            return true;
        }
        else if(three == null && to == 3)
        {
            return true;
        }
        else if(four == null && to == 4)
        {
            return true;
        }
        else if(five == null && to == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void getfirstspeed()
    {
        firstSpeed = 15 * speed;
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
