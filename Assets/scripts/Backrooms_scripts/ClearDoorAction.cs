using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClearDoorAction : MonoBehaviour
{
    private bool isCoroutineRunning = false;

    public float interactionDistance;
    public GameObject doorOpenText;
    public GameObject doorCloseText;
    public GameObject doorLockedText;
    public GameObject haskeyImage;
    public string doorOpenAnimName, doorCloseAnimName;

    public bool haskey;

    public AudioClip clearDoorOpenSound;
    public AudioClip clearDoorCloseSound;

    [SerializeField] AudioSource footStepSource;
    [SerializeField] Movement movement;
    public Image clearPanel;

    public DoorAction doorAction;

    // Start is called before the first frame update
    void Start()
    {
        doorAction = GetComponent<DoorAction>();
        if (clearPanel != null)
        {
            Color color = clearPanel.color;
            color.a = 0f;
            clearPanel.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                GameObject ClearDoor = hit.collider.transform.root.gameObject;
                Animator doorAnim = ClearDoor.GetComponent<Animator>();
                Door door = ClearDoor.GetComponent<Door>(); // Door ��ũ��Ʈ ����
                AudioSource clearDoorAudio = ClearDoor.GetComponent<AudioSource>(); // ������ AudioSource ��������

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
                        doorAction.haskey = false;
                        haskeyImage.SetActive(false);

                        // �� ���� �Ҹ� ���
                        PlaySound(clearDoorAudio, clearDoorOpenSound);                        
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && (!haskey || door.isLocked))
                    {
                        if (!isCoroutineRunning) // �ߺ� ȣ�� ����
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

                        // �� ���� �Ҹ� ���
                        PlaySound(clearDoorAudio, clearDoorCloseSound);
                    }
                }
            }

            else if (hit.collider.gameObject.tag == "ClearDoor")
            {
                GameObject ClearDoor = hit.collider.transform.root.gameObject;
                Animator doorAnim = ClearDoor.GetComponent<Animator>();
                Door door = ClearDoor.GetComponent<Door>(); // Door ��ũ��Ʈ ����
                AudioSource clearDoorAudio = ClearDoor.GetComponent<AudioSource>(); // ������ AudioSource ��������
                Debug.Log("Ŭ���� ���� ����ĳ��Ʈ �ν�");

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

                        // �� ���� �Ҹ� ���
                        PlaySound(clearDoorAudio, clearDoorOpenSound);

                        SetPanelVisibility(1f);
                        movement.enabled = false;
                        footStepSource.enabled = false;

                        Debug.Log("�ι��� ���Ⱑ Ŭ���������̿���");
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && (!haskey || door.isLocked))
                    {
                        if (!isCoroutineRunning) // �ߺ� ȣ�� ����
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

                        // �� ���� �Ҹ� ���
                        PlaySound(clearDoorAudio, clearDoorCloseSound);                     
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
    void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // ������ AudioClip ���
            Debug.Log("������� ������Դϴ�.");
        }
    }
    void SetPanelVisibility(float alpha)
    {
        Debug.Log("������ �Լ� ȣ��");
        if (clearPanel == null)
        {
            Debug.LogError("clearPanel�� �������� �ʾҽ��ϴ�!");
            return;
        }

        Color color = clearPanel.color;
        color.a = alpha; // ���İ� ����
        clearPanel.color = color;

        // ������ �α�
        Debug.Log($"SetPanelVisibility ȣ��� - ������ ���İ�: {alpha}, ���� ���İ�: {clearPanel.color.a}");
    }


}
