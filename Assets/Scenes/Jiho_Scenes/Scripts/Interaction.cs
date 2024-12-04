//has to be attached to the player's camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WCP;
public class Interaction : MonoBehaviour
{
    float minDist = 5f;

    public bool hasKey;
    public bool hasKey1;
    public bool hasEye = false;
    public bool hascardkey = false;
    private bool delay = false;




    public GameObject Soundmanager;
    public GameObject Canvas;
    private GameObject Perdit;
    public GameObject Doll;
    private bool isAnimating = false;
    private int count = 0;

    public GameObject[] Triggerzone;

    DoorOpening dO;
    ChangeMatarial d1;
    Sound sound;
    WChatPanel chat;
    ActionTriggerChange action_change;
    ActionTriggerChange1 action_change1;
    private Animator animator;
    


    private void Awake()
    {
        sound = Soundmanager.gameObject.GetComponent<Sound>();
        action_change = GetComponent<ActionTriggerChange>();
        action_change1 = GetComponent<ActionTriggerChange1>();
        animator = transform.GetComponent<Animator>();

    }

    void actiondelay()
    {
        Debug.Log("delay false");
        delay = false;
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * minDist, Color.yellow);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, minDist))
        {

            if (hit.transform.gameObject.tag == "Door")
            {
                dO = hit.transform.gameObject.GetComponent<DoorOpening>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Press E");
                    if (hasKey)
                    {
                        if (dO.isClose)
                        {
                            dO.Open();
                        }
                        else if (dO.isOpen)
                        {
                            dO.Close();
                        }
                    }
                }
            }


            else if (hit.transform.gameObject.tag == "Locked_Door")
            {
                dO = hit.transform.gameObject.GetComponent<DoorOpening>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Press E");
                    if (hasKey1)
                    {
                        if (dO.isClose)
                        {
                            dO.Open();
                        }
                    }
                }
            }

            else if (hit.transform.gameObject.tag == "Doll")
            {
                chat = hit.transform.gameObject.GetComponent<WChatPanel>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<Movement>().enabled = false;
                    Canvas.gameObject.SetActive(true);

                }
            }

            else if (hit.transform.gameObject.tag == "Perdit")
            {
                if (Input.GetKeyDown(KeyCode.E) && delay == false)
                {
                    animator.SetTrigger("StartUpwardThrust");
                    delay = true;
                    Debug.Log("delay true");
                    Invoke("actiondelay", 2.33f);
                    Perdit = hit.transform.gameObject;
                    if (!Perdit.transform.parent.Find("Maze").gameObject.activeSelf)
                    {
                        sound.creature();
                        Triggerzone[0].transform.transform.gameObject.SetActive(true);
                        Perdit.transform.parent.Find("Blood").gameObject.SetActive(true);
                        Perdit.transform.parent.Find("Heart").gameObject.SetActive(true);
                        action_change.Action_change_position();
                    }
                    else if (!Perdit.transform.parent.Find("Zweihander").gameObject.activeSelf)
                    {
                        sound.creature();
                        Perdit.transform.parent.Find("Blood").gameObject.SetActive(true);
                        Perdit.transform.parent.Find("Heart").gameObject.SetActive(true);
                        action_change1.Action_change_position();
                    }


                }
            }

            else if (hit.transform.gameObject.tag == "Eye")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.SetActive(false);
                    hasEye = true;
                }
            }

            else if (hit.transform.gameObject.tag =="CardKey")
            {
                d1 = hit.transform.gameObject.GetComponent<ChangeMatarial>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Press E");
                    if (hascardkey) d1.changematerial();
                }
            }

            if (hit.transform.gameObject.tag == "Heart")
            {
                if (Input.GetKeyDown(KeyCode.E) )
                {
                    Destroy(hit.transform.gameObject);
                    count++;
                    if (count == 2)
                    {
                        Doll.gameObject.SetActive(true);
                    }

                }
            }
        }
    }
}
