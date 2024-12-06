//has to be attached to the door

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public GameObject moveTarget;

    Quaternion startingRotation;
    Vector3 startingPosition;

    public bool isOpen;
    public bool isClose;

    private void Awake()
    {
        startingRotation = transform.rotation;
        startingPosition = transform.position;
        isClose = true;
    }

    public void Close()
    {
        Debug.Log("Close()");
        transform.rotation = startingRotation;
        transform.position = startingPosition;
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
