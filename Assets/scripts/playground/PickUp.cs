//has to be attached to the player's camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    float minDist = 20f;
    public GameObject canvas;
    Interaction DO;

    // Start is called before the first frame update
    void Start()
    {
        DO = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, minDist))
        {
            
            if (hit.transform.gameObject.tag == "Key")
            {
                canvas.transform.parent.Find("press_E").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    hit.transform.gameObject.SetActive(false);
                    DO.hasKey1 = true;
                }
            }
        }
    }
}
