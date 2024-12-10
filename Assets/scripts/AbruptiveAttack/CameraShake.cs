using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Camera Reference")]
    public Transform cameraTransform; // 흔들릴 카메라의 Transform

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned!");
            yield break;
        }

        Vector3 originalPosition = cameraTransform.localPosition; // 카메라의 초기 위치 저장
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition; // 카메라 위치 복원
    }
}
