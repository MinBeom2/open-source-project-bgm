using UnityEngine;
using TMPro; // TextMeshPro 사용을 위한 네임스페이스 추가
using UnityEngine.UI; // UI 관련 네임스페이스 추가
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    [Header("Door Settings")]
    public string openDoorTrigger = "Open"; // Animator의 Trigger 이름 (애니메이션 관련, 주석 처리)

    [Header("Key Settings")]
    public GameObject[] keys; // 맵에 배치된 열쇠 오브젝트 배열
    private bool[] keysCollected; // 각 열쇠의 수집 여부를 추적

    [Header("UI Settings")]
    public TextMeshProUGUI warningText; // 경고 메시지용 TextMeshPro
    public float warningDisplayTime = 2f; // 경고 메시지 표시 시간

    [Header("Ending UI Settings")]

    public float fadeDuration = 2f; // Ending 이미지가 서서히 나타나는 시간

    [Header("Audio Settings")]
    public AudioSource audioSource; // 문 열림 효과음 재생용 AudioSource

    [Header("Enemy Settings")]
    public GameObject[] enemies; // 비활성화할 Enemy 오브젝트 배열

    private float warningTimer = 0f;
    private bool isPlayerNearby = false; // 플레이어가 문 근처에 있는지 여부

    void Start()
    {
        // TextMeshPro 초기 비활성화
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }



        // 열쇠 수집 상태 초기화
        if (keys != null)
        {
            keysCollected = new bool[keys.Length];
        }

        // AudioSource 확인
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    void Update()
    {
        // 경고 메시지 타이머 처리
        if (warningText != null && warningText.gameObject.activeSelf)
        {
            warningTimer += Time.deltaTime;
            if (warningTimer >= warningDisplayTime)
            {
                warningText.gameObject.SetActive(false);
                warningTimer = 0f;
            }
        }

        // 플레이어가 근처에 있고 E 키를 눌렀을 때 처리
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (AllKeysCollected())
            {
                OpenDoor();
            }
            else
            {
                ShowWarningMessage();
            }
        }
    }

    private void OpenDoor()
    {
        Debug.Log("The door has been opened.");
        DisableEnemies();

        // AudioSource 재생
        if (audioSource != null)
        {
            audioSource.Play();
        }
        DataManager.instance.nowPos.positionX = 1.1f;
        DataManager.instance.nowPos.positionY = 0;
        DataManager.instance.nowPos.positionZ = 11.13f;
        DataManager.instance.nowPos.rotationY = 0;
        DataManager.instance.nowPlayer.stage = "AISLE4";
        SceneManager.LoadScene("AISLE4");

    }

    private void ShowWarningMessage()
    {
        // 경고 메시지 활성화
        if (warningText != null)
        {
            warningText.gameObject.SetActive(true);
        }
    }

    public void CollectKey(GameObject key)
    {
        // 열쇠 획득 처리
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] == key)
            {
                keysCollected[i] = true;
                Destroy(key); // 열쇠 오브젝트 삭제
                Debug.Log($"Key {i + 1} collected.");
                break;
            }
        }
    }

    public bool AllKeysCollected()
    {
        // 모든 열쇠를 획득했는지 확인
        foreach (bool collected in keysCollected)
        {
            if (!collected)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 문 근처에 들어왔을 때
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 문 근처에서 벗어났을 때
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void DisableEnemies()
    {
        if (enemies != null && enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false); // 적 비활성화
                    Debug.Log($"Enemy {enemy.name} has been disabled.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No enemies to disable.");
        }
    }
}
