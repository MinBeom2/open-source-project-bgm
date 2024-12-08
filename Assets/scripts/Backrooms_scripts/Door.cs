using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject moveTarget;  // 문이 열릴 때 위치
    public bool isLocked = false;  // 잠금 상태 여부 설정 (기본값: 잠기지 않음)

    private Vector3 startingPosition;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        startingPosition = transform.position;
    }

    public void ToggleDoor()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public void Open()
    {
        transform.position = moveTarget.transform.position;
        isOpen = true;
    }

    public void Close()
    {
        transform.position = startingPosition;
        isOpen = false;
    }
}
