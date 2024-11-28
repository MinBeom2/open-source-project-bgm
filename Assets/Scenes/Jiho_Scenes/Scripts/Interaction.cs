//has to be attached to the player's camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WCP;
public class Interaction : MonoBehaviour
{
    float minDist = 5f;

    public bool hasKey;
    public GameObject Canvas;

    DoorOpening dO;
    WChatPanel chat;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, minDist))
        {
            
            if(hit.transform.gameObject.tag == "Door")
            {
                Debug.Log("Door");
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

            if(hit.transform.gameObject.tag == "Doll")
            {
                Debug.Log("Doll");
                chat= hit.transform.gameObject.GetComponent<WChatPanel>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Canvas.gameObject.SetActive(true);
                    
                }
            }
        }
    }
}
