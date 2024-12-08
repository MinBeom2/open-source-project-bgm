//has to be attached to the player's camera

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using WCP;
public class Interaction : MonoBehaviour
{
    float minDist = 7f;

    public bool hasKey;
    public bool hasKey1;
    public bool hasEye = false;
    public bool hascardkey = false;
    private bool delay = false;
    public bool has2heart = false;
    public bool haslastkey = false;

    public GameObject Canvas;
    private GameObject Perdit;
    public GameObject Doll;
    public GameObject Sword;
    AudioManager audioManager;

    private bool isAnimating = false;
    private int count = 0;

    public GameObject[] Triggerzone;

    DoorOpening dO;
    FinalDoorOpening d2;
    ChangeMatarial d1;
    WChatPanel chat;
    ActionTriggerChange action_change;
    ActionTriggerChange1 action_change1;
    private Animator animator;



    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        action_change = GetComponent<ActionTriggerChange>();
        action_change1 = GetComponent<ActionTriggerChange1>();
        animator = transform.GetComponent<Animator>();
    }


    IEnumerator actiondelay()
    {
        yield return new WaitForSeconds(2.33f);
        delay = false;
    }

    IEnumerator piercing()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySFX(audioManager.piercing);

    }


    IEnumerator blooding(GameObject gameobject1, GameObject gameobject2)
    {
        yield return new WaitForSeconds(0.5f);
        gameobject1.SetActive(true);
        gameobject2.SetActive(true);
    }

    IEnumerator heart(GameObject gameobject)
    {
        yield return new WaitForSeconds(0.5f);
        gameobject.SetActive(true);
    }

    IEnumerator creature_scream()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySFX(audioManager.creature);
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, minDist))
        {
            if (hit.transform.gameObject.tag == "Door")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                dO = hit.transform.gameObject.GetComponent<DoorOpening>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hasKey)
                    {
                        if (dO.isClose)
                        {
                            dO.Open();
                            audioManager.PlaySFX(audioManager.normaldoor);

                        }
                        else if (dO.isOpen)
                        {
                            dO.Close();
                            audioManager.PlaySFX(audioManager.normaldoor);
                        }
                    }
                }
            }


            else if (hit.transform.gameObject.tag == "Locked_Door")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                dO = hit.transform.gameObject.GetComponent<DoorOpening>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hasKey1)
                    {
                        if (dO.isClose)
                        {
                            dO.Open();
                            audioManager.PlaySFX(audioManager.normaldoor);
                        }
                    }
                    else
                    {
                        audioManager.PlaySFX(audioManager.lockeddoor);
                    }
                }
            }

            else if (hit.transform.gameObject.tag == "Doll")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                chat = hit.transform.gameObject.GetComponent<WChatPanel>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<playgroundMovement>().enabled = false;
                    Canvas.gameObject.SetActive(true);
                    Canvas.transform.parent.Find("press_E").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                    Canvas.transform.parent.Find("press_SpaceBar").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);

                }
            }

            else if (hit.transform.gameObject.tag == "Perdit")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && delay == false)
                {
                    animator.SetTrigger("StartUpwardThrust");
                    Sword.SetActive(true);
                    StartCoroutine(piercing());
                    delay = true;
                    StartCoroutine(actiondelay());
                    Perdit = hit.transform.gameObject;
                    if (!Perdit.transform.parent.Find("Maze").gameObject.activeSelf)
                    {
                        StartCoroutine(creature_scream());
                        Triggerzone[0].transform.transform.gameObject.SetActive(true);
                        StartCoroutine(blooding(Perdit.transform.parent.Find("Blood").gameObject, Perdit.transform.parent.Find("BloodEffect").transform.gameObject));
                        StartCoroutine(heart(Perdit.transform.parent.Find("Heart").gameObject));
                        action_change.Action_change_position();
                    }
                    else if (!Perdit.transform.parent.Find("Zweihander").gameObject.activeSelf)
                    {
                        StartCoroutine(creature_scream());
                        StartCoroutine(blooding(Perdit.transform.parent.Find("Blood").gameObject, Perdit.transform.parent.Find("BloodEffect").transform.gameObject));
                        StartCoroutine(heart(Perdit.transform.parent.Find("Heart").gameObject));
                        action_change1.Action_change_position();
                    }


                }
            }

            else if (hit.transform.gameObject.tag == "Eye")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.SetActive(false);
                    hasEye = true;
                }
            }

            else if (hit.transform.gameObject.tag == "CardKey")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                d1 = hit.transform.gameObject.GetComponent<ChangeMatarial>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audioManager.PlaySFX(audioManager.normaldoor);
                    if (hascardkey) d1.changematerial();
                }
            }

            if (hit.transform.gameObject.tag == "Heart")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);
                    count++;
                    if (count == 2)
                    {
                        Doll.gameObject.SetActive(true);
                        Doll.GetComponent<Animator>().SetTrigger("Crouching_idle");
                        has2heart = true;
                    }

                }
            }

            if (hit.transform.gameObject.tag == "FinalDoor")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                d2 = hit.transform.gameObject.GetComponent<FinalDoorOpening>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (haslastkey)
                    {
                        if (d2.isClose)
                        {
                            DataManager.instance.nowPos.positionX = 1.1f;
                            DataManager.instance.nowPos.positionY = 0;
                            DataManager.instance.nowPos.positionZ = 11.13f;
                            DataManager.instance.nowPos.rotationY = 0;
                            DataManager.instance.nowPlayer.stage = "AISLE3";
                            audioManager.PlaySFX(audioManager.DoorOpen);
                            SceneManager.LoadScene("AISLE3");
                        }
                    }
                    else
                    {
                        audioManager.PlaySFX(audioManager.lockeddoor);
                    }
                }
            }

            if (hit.transform.gameObject.tag == "Untagged")
            {
                Canvas.transform.parent.Find("press_E").gameObject.SetActive(false);
            }
        }

        else
        {
            Canvas.transform.parent.Find("press_E").gameObject.SetActive(false);
        }
    }
}
