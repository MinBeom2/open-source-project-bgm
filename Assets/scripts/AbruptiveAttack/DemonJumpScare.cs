using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DemonJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject GameoverPanel;
    public Transform door; // Door 오브젝트의 Transform을 받음
    private DoorOpen doorOpenScript; // DoorOpen 스크립트 참조

    private SkinnedMeshRenderer bodyRenderer; // Body의 SkinnedMeshRenderer
    private Light spotLight; // Spot Light
    private AudioSource audioSource; // Audio Source
    private Transform demonCenter; // DemonCenter의 Transform
    private CameraShake cameraShake; // CameraShake 컴포넌트

    public float rotationSpeed = 2.0f; // 회전 속도

    [Header("Camera Shake Settings")]
    public float shakeDuration = 0.5f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.2f; // 흔들림 강도

    [Header("UI Elements")]
    public Image endingImage; // 흑백 화면 (UI Image)
    public TextMeshProUGUI gameOverText; // "Game Over" 텍스트 (TextMeshPro)

    private void Start()
    {
        // Door의 DoorOpen 스크립트 가져오기
        if (door != null)
        {
            doorOpenScript = door.GetComponent<DoorOpen>();
        }

        // DemonCenter Transform 찾기
        demonCenter = transform.Find("DemonCenter");
        if (demonCenter == null)
        {
            Debug.LogError("DemonCenter 자식 오브젝트를 찾을 수 없습니다!");
        }

        // Body의 SkinnedMeshRenderer 찾기
        Transform body = transform.Find("DemonBody");
        if (body != null)
        {
            bodyRenderer = body.GetComponent<SkinnedMeshRenderer>();
            if (bodyRenderer != null)
                bodyRenderer.enabled = false; // 처음엔 비활성화
        }

        // Spot Light 찾기
        spotLight = transform.Find("DemonSpotLight")?.GetComponent<Light>();
        if (spotLight != null)
        {
            spotLight.enabled = false; // 처음엔 비활성화
        }

        // Audio Source 찾기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }

        // CameraShake 컴포넌트 찾기
        cameraShake = GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogError("CameraShake component is missing on this GameObject.");
        }

        // Ending Image 및 GameOver Text 초기화
        if (endingImage != null)
        {
            endingImage.color = new Color(0, 0, 0, 0); // 초기 투명도 설정
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // 초기 비활성화
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // PlayerTag와 충돌 여부 확인
        if (other.CompareTag("Player"))
        {
            if (doorOpenScript != null && !doorOpenScript.AllKeysCollected())
            {
                // Player의 PlayerController 비활성화
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.enabled = false;
                }

                // Body와 Spot Light 활성화
                if (bodyRenderer != null)
                {
                    bodyRenderer.enabled = true;

                    // AudioSource 재생
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }
                }

                if (spotLight != null)
                {
                    spotLight.enabled = true;
                }

                // CameraShake 실행 및 UI 효과 호출
                if (cameraShake != null)
                {
                    StartCoroutine(HandleJumpScareEffects());
                }

                // 플레이어의 카메라를 DemonCenter로 회전시키기 시작
                Camera playerCamera = other.GetComponentInChildren<Camera>();
                if (playerCamera != null && demonCenter != null)
                {
                    StartCoroutine(RotateCameraTowards(playerCamera.transform, demonCenter.position));
                }
            }
        }
    }

    private IEnumerator HandleJumpScareEffects()
    {
        // CameraShake 효과 실행
        yield return StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));

        // Ending Image 페이드 인 효과 실행
        if (endingImage != null)
        {
            yield return StartCoroutine(FadeInEndingImage());
        }

        GameoverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator FadeInEndingImage()
    {
        float duration = 1.5f; // 페이드 인 지속 시간
        float elapsedTime = 0f;
        Color color = endingImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration); // Alpha 값을 0에서 1로 증가
            endingImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Alpha를 최종적으로 1로 설정
        endingImage.color = new Color(color.r, color.g, color.b, 1f);
    }

    private IEnumerator RotateCameraTowards(Transform cameraTransform, Vector3 targetPosition)
    {
        // 목표 회전을 모든 축을 포함하여 계산
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - cameraTransform.position);

        while (Quaternion.Angle(cameraTransform.rotation, targetRotation) > 0.1f)
        {
            // 현재 회전과 목표 회전 사이를 보간
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null; // 다음 프레임으로 대기
        }

        // 최종적으로 정확히 타겟 회전으로 설정
        cameraTransform.rotation = targetRotation;
    }
}