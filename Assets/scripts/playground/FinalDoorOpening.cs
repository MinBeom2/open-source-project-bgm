//has to be attached to the door

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorOpening : MonoBehaviour
{
    public GameObject moveTarget;

    Quaternion starting_rotation;
    Vector3 starting_position;

    public bool isOpen;
    public bool isClose;

    private void Awake()
    {
        starting_rotation = transform.rotation;
        starting_position = transform.position;
        isClose = true;
    }

    public void Close()
    {
        Debug.Log("Close()");
        transform.rotation = starting_rotation;
        isClose = true;
        isOpen = false;
    }

    public void Open()
    {
        transform.rotation = moveTarget.transform.rotation;
        transform.position = moveTarget.transform.position;
        Debug.Log("Open()");
        isClose = false;
        isOpen = true;
    }
}
