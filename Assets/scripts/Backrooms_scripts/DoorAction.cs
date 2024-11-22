using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    private bool isCoroutineRunning = false;

    public float interactionDistance;
    public GameObject doorOpenText;
    public GameObject doorCloseText;
    public GameObject doorLockedText;
    public GameObject haskeyImage;
    public string doorOpenAnimName, doorCloseAnimName;

    public bool haskey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorOpenText.SetActive(false);
        doorCloseText.SetActive(false);

    Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.gameObject.tag == "Door")
            {
                GameObject DoorPivot = hit.collider.transform.root.gameObject;
                Animator doorAnim = DoorPivot.GetComponent<Animator>();
                Door door = DoorPivot.GetComponent<Door>(); // DoorOpening 스크립트 참조

                if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                {
                    doorCloseText.SetActive(false);
                    doorOpenText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && (haskey || !door.isLocked))
                    {
                        doorAnim.ResetTrigger("Close");
                        doorAnim.SetTrigger("Open");
                        door.isLocked = false;
                        haskey = false;
                        haskeyImage.SetActive(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && (!haskey || door.isLocked))
                    {
                        if (!isCoroutineRunning) // 중복 호출 방지
                        {
                            doorLockedText.SetActive(true);
                            StartCoroutine(HideLockedTextAfterDelay(1f));
                        }
                    }
                }

                if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                {
                    doorOpenText.SetActive(false);
                    doorCloseText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && (!door.isLocked))
                    {
                        doorAnim.ResetTrigger("Open");
                        doorAnim.SetTrigger("Close");
                    }
                }
            }
            else
            {
                doorOpenText.SetActive(false);
                doorCloseText.SetActive(false);
            }
        }
    }
    IEnumerator HideLockedTextAfterDelay(float delay)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(delay);
        doorLockedText.SetActive(false);
        isCoroutineRunning = false;
    }
}
