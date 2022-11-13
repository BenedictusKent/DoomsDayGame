using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    private Image front, back;
    private bool iscold = false;
    private float times, coldtime;
    private GameObject skill01;

    public GameObject Player01;
    private PlayerControl player01_control;
    private float orgspeed;

    public GameObject Particle01;
    public GameObject Particle02;
    private GameObject Particle01_copy;
    private GameObject Particle02_copy;

    // Start is called before the first frame update
    void Start()
    {
        skill01 = GameObject.Find("skill01");
        front = skill01.GetComponent<Image>();
        back = skill01.transform.GetChild(0).GetComponent<Image>();
        back.fillAmount = 0f;
        coldtime = 5f;

        player01_control = Player01.GetComponent<PlayerControl>();
        orgspeed = player01_control.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(iscold)
        {
            times += Time.deltaTime;
            back.fillAmount = (coldtime - times) / coldtime;
            if(times >= 5f)
            {
                times = 0f;
                iscold = false;
                back.fillAmount = 0f;
            }
        }

        if(Input.GetKeyDown("z") && !iscold)
        {
            show();
            player01_control.speed = orgspeed * 2;
            Particle01_copy = Instantiate(Particle01);
            Particle01_copy.transform.parent = Player01.transform;
            Particle01_copy.transform.localPosition = Vector3.zero;
            Particle02_copy = Instantiate(Particle02);
            Particle02_copy.transform.parent = Player01.transform;
            Particle02_copy.transform.localPosition = Vector3.zero;
            Invoke("endskill01", 2.0f);
        }
    }

    public void show()
    {
        iscold = true;
        back.fillAmount = 1f;
    }

    void endskill01()
    {
        player01_control.speed = orgspeed;
        Destroy(Particle01_copy);
        Destroy(Particle02_copy);
    }
}
