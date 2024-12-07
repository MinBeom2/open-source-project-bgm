using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public float interactionDistance; // 상호작용 거리
    public DoorAction doorAction;    // DoorAction 스크립트 참조
    public ClearDoorAction clearDoorAction;
    public GameObject haskeyImage;

    public GameObject keyPickupText;

    public AudioClip keyPickUpSound; // 열쇠 줍는 소리 클립

    void Start()
    {
        // DoorAction 스크립트를 카메라에 붙여서 참조하기
        doorAction = GetComponent<DoorAction>();
        clearDoorAction = GetComponent<ClearDoorAction>();
    }

    void Update()
    {
        keyPickupText.SetActive(false); // 텍스트 숨기기

        Ray ray = new Ray(transform.position, transform.forward); // 레이캐스트 생성
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance)) // 레이캐스트가 닿았는지 확인
        {
            if (hit.collider.gameObject.tag == "Key") // 태그가 "Key"인지 확인
            {
                keyPickupText.SetActive(true); // 텍스트 활성화
                GameObject key = hit.collider.transform.root.gameObject;

                if (Input.GetKeyDown(KeyCode.E)) // E 키 입력 확인
                {
                    PlaySound(keyPickUpSound);   // 열쇠 줍는 소리 재생
                    key.SetActive(false);       // 열쇠 오브젝트 숨기기
                    doorAction.haskey = true;   // DoorAction의 hasKey 설정
                    clearDoorAction.haskey = true;
                    haskeyImage.SetActive(true); // 열쇠 이미지 활성화
                }
            }
        }
    }

    // PlayClipAtPoint를 사용하여 소리 재생
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position); // 현재 위치에서 소리 재생
        }
    }
}
