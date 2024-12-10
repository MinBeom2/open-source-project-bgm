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
                Door door = ClearDoor.GetComponent<Door>(); // Door 스크립트 참조
                AudioSource clearDoorAudio = ClearDoor.GetComponent<AudioSource>(); // 문에서 AudioSource 가져오기

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

                        // 문 열림 소리 재생
                        PlaySound(clearDoorAudio, clearDoorOpenSound);                        
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

                        // 문 닫힘 소리 재생
                        PlaySound(clearDoorAudio, clearDoorCloseSound);
                    }
                }
            }

            else if (hit.collider.gameObject.tag == "ClearDoor")
            {
                GameObject ClearDoor = hit.collider.transform.root.gameObject;
                Animator doorAnim = ClearDoor.GetComponent<Animator>();
                Door door = ClearDoor.GetComponent<Door>(); // Door 스크립트 참조
                AudioSource clearDoorAudio = ClearDoor.GetComponent<AudioSource>(); // 문에서 AudioSource 가져오기
                Debug.Log("클리어 도어 레이캐스트 인식");

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

                        // 문 열림 소리 재생
                        PlaySound(clearDoorAudio, clearDoorOpenSound);

                        SetPanelVisibility(1f);
                        movement.enabled = false;
                        footStepSource.enabled = false;

                        Debug.Log("민범씨 여기가 클리어지점이에요");
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

                        // 문 닫힘 소리 재생
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
            audioSource.PlayOneShot(clip); // 지정된 AudioClip 재생
            Debug.Log("오디오가 재생중입니다.");
        }
    }
    void SetPanelVisibility(float alpha)
    {
        Debug.Log("불투명도 함수 호출");
        if (clearPanel == null)
        {
            Debug.LogError("clearPanel이 설정되지 않았습니다!");
            return;
        }

        Color color = clearPanel.color;
        color.a = alpha; // 알파값 설정
        clearPanel.color = color;

        // 디버깅용 로그
        Debug.Log($"SetPanelVisibility 호출됨 - 설정된 알파값: {alpha}, 현재 알파값: {clearPanel.color.a}");
    }


}
