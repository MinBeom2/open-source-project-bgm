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
    public GameObject Canvas;
    private bool isAnimating = false;

    DoorOpening dO;
    WChatPanel chat;
    ActionTriggerChange action_change;
    private Animator animator;


    private void Awake()
    {
        action_change = GetComponent<ActionTriggerChange>();
        animator = transform.GetComponent<Animator>();

    }

    

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, minDist))
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


            if (hit.transform.gameObject.tag == "Locked_Door")
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

            if (hit.transform.gameObject.tag == "Doll")
            {
                chat= hit.transform.gameObject.GetComponent<WChatPanel>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<Movement>().enabled = false;
                    Canvas.gameObject.SetActive(true);
                    
                }
            }

            if (hit.transform.gameObject.tag == "Perdit")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetTrigger("StartUpwardThrust");
                    action_change.Action_change_position();

                }
            }
            
            if (hit.transform.gameObject.tag == "Eye")
            {
                Debug.Log("Eye");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.SetActive(false);
                    hasEye = true;
                }
            }
        }
    }
}
