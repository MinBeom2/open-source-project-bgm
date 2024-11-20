using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public float interactionDistance;
    public DoorAction doorAction;  // DoorAction ��ũ��Ʈ ����
    public ClearDoorAction clearDoorAction;
    public GameObject haskeyImage;

    // Start is called before the first frame update
    void Start()
    {
        // DoorAction ��ũ��Ʈ�� ī�޶� �ٿ��� �����ϱ�
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
                    hit.transform.gameObject.SetActive(false);  // Ű�� �����
                    doorAction.haskey = true;  // DoorAction�� hasKey�� true�� ����
                    clearDoorAction.haskey = true;
                    haskeyImage.SetActive(true);
                }
            }
        }
    }
}
