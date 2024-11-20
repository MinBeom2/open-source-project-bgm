using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public float interactionDistance;
    public DoorAction doorAction;  // DoorAction 스크립트 참조
    public ClearDoorAction clearDoorAction;
    public GameObject haskeyImage;

    // Start is called before the first frame update
    void Start()
    {
        // DoorAction 스크립트를 카메라에 붙여서 참조하기
        doorAction = GetComponent<DoorAction>();
        clearDoorAction = GetComponent<ClearDoorAction>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.transform.gameObject.tag == "Key")
                {
                    hit.transform.gameObject.SetActive(false);  // 키를 숨기고
                    doorAction.haskey = true;  // DoorAction의 hasKey를 true로 설정
                    clearDoorAction.haskey = true;
                    haskeyImage.SetActive(true);
                }
            }
        }
    }
}
