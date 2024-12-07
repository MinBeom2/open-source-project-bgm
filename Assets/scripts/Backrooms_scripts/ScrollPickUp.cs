using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPickUp : MonoBehaviour
{
    private bool isCoroutineRunning = false;

    public float interactionDistance;
    public DoorAction doorAction;  // DoorAction ��ũ��Ʈ ����
    public ClearDoorAction clearDoorAction;
    public GameObject haskeyImage;

    public GameObject HintPickUpText;
    public GameObject HintCloseText;
    public GameObject HintText;

    private bool isHintActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        HintPickUpText.SetActive(false);
        HintCloseText.SetActive(false);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform.gameObject.tag == "Scroll")
            {
                if (!isHintActive)
                {
                    HintPickUpText.SetActive(true);
                }
                else
                {
                    HintCloseText.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // ��Ʈ Ȱ��ȭ ���¸� ���
                    isHintActive = !isHintActive;

                    if (!isCoroutineRunning) // �ߺ� ȣ�� ����
                    {
                        HintText.SetActive(true);
                        StartCoroutine(HideHintTextAfterDelay(2f));
                    }
                }
            }
        }
    }
    IEnumerator HideHintTextAfterDelay(float delay)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(delay);
        HintText.SetActive(false);
        isCoroutineRunning = false;
    }
}
