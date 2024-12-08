using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public float interactionDistance; // ��ȣ�ۿ� �Ÿ�
    public DoorAction doorAction;    // DoorAction ��ũ��Ʈ ����
    public ClearDoorAction clearDoorAction;
    public GameObject haskeyImage;

    public GameObject keyPickupText;

    public AudioClip keyPickUpSound; // ���� �ݴ� �Ҹ� Ŭ��

    void Start()
    {
        // DoorAction ��ũ��Ʈ�� ī�޶� �ٿ��� �����ϱ�
        doorAction = GetComponent<DoorAction>();
        clearDoorAction = GetComponent<ClearDoorAction>();
    }

    void Update()
    {
        keyPickupText.SetActive(false); // �ؽ�Ʈ �����

        Ray ray = new Ray(transform.position, transform.forward); // ����ĳ��Ʈ ����
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance)) // ����ĳ��Ʈ�� ��Ҵ��� Ȯ��
        {
            if (hit.collider.gameObject.tag == "Key") // �±װ� "Key"���� Ȯ��
            {
                keyPickupText.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
                GameObject key = hit.collider.transform.root.gameObject;

                if (Input.GetKeyDown(KeyCode.E)) // E Ű �Է� Ȯ��
                {
                    PlaySound(keyPickUpSound);   // ���� �ݴ� �Ҹ� ���
                    key.SetActive(false);       // ���� ������Ʈ �����
                    doorAction.haskey = true;   // DoorAction�� hasKey ����
                    clearDoorAction.haskey = true;
                    haskeyImage.SetActive(true); // ���� �̹��� Ȱ��ȭ
                }
            }
        }
    }

    // PlayClipAtPoint�� ����Ͽ� �Ҹ� ���
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position); // ���� ��ġ���� �Ҹ� ���
        }
    }
}
