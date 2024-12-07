using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class EnemyJumpScareHandler : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeDuration = 0.5f; // 카메라 흔들림 지속 시간
    public float shakeMagnitude = 0.3f; // 카메라 흔들림 강도

    [Header("UI Elements")]
    public Image endingImage; // Ending Image (UI)
    public TextMeshProUGUI gameOverText; // GameOver 텍스트 (TextMeshPro)

    private CameraShake cameraShake; // CameraShake 컴포넌트

    private void OnEnable()
    {
        // CameraShake 컴포넌트 가져오기
        cameraShake = GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogError("CameraShake script is missing on this GameObject!");
            return;
        }

        // CameraShake 흔들림 실행
        StartCoroutine(HandleShakeAndEffects());
    }

    private IEnumerator HandleShakeAndEffects()
    {
        // CameraShake 효과 실행
        yield return StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));

        // Ending Image 투명도 증가 효과
        if (endingImage != null)
        {
            yield return StartCoroutine(FadeInEndingImage());
        }

        // GameOver 텍스트 활성화
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
        //김민범
    }

    private IEnumerator FadeInEndingImage()
    {
        float duration = 1.5f; // 투명도 증가 시간
        float elapsedTime = 0f;
        Color color = endingImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration); // Alpha 값 0에서 1로 변화
            endingImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 최종적으로 Alpha를 1로 설정
        endingImage.color = new Color(color.r, color.g, color.b, 1f);
    }
}
