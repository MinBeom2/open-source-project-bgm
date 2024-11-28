//has to be attached to the door

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public GameObject moveTarget;

    Quaternion startingPosition;

    public bool isOpen;
    public bool isClose;

    private void Awake()
    {
        startingPosition = transform.rotation;
        isClose = true;
    }

    public void Close()
    {
        Debug.Log("Close()");
        transform.rotation = startingPosition;
        isClose = true;
        isOpen = false;
    }

    public void Open()
    {
        transform.rotation = moveTarget.transform.rotation;
        Debug.Log("Open()");
        isClose = false;
        isOpen = true;
    }
}
