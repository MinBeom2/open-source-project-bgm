using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject aiGameOver; // AI의 모델 (SetActive를 통해 제어)
    public Image fadeOutPanel; // 검은색 이미지를 담을 UI 캔버스의 이미지
    public float fadeOutDuration = 2f; // 페이드아웃 지속 시간

    private bool isGameOver = false;

    private Movement movement;
    [SerializeField] AudioSource screamSource;
    [SerializeField] AudioSource footStepSource;

    public AudioClip scream;

    void Start()
    {
        // 처음에는 AI 모델을 비활성화
        if (aiGameOver != null)
            aiGameOver.SetActive(false);

        // 검은색 이미지의 투명도를 0으로 설정 (투명 상태)
        if (fadeOutPanel != null)
        {
            Color color = fadeOutPanel.color;
            color.a = 0f;
            fadeOutPanel.color = color;
        }

        movement = GetComponent<Movement>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌한 객체: " + other.name);

        if (isGameOver) return;

        // AI의 콜라이더에 닿았는지 확인
        if (other.CompareTag("Enemy"))
        {
            isGameOver = true;

            movement.enabled = false;

            footStepSource.enabled = false;

            // 충돌한 AI를 비활성화
            GameObject aiChaser = other.gameObject;
            aiChaser.SetActive(false);

            // AI 모델 활성화
            if (aiGameOver != null)
                aiGameOver.SetActive(true);

            PlaySound(screamSource, scream);

            // 페이드아웃 시작
            StartCoroutine(FadeOutAndGameOver());
        }
    }

    IEnumerator FadeOutAndGameOver()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            if (fadeOutPanel != null)
            {
                Color color = fadeOutPanel.color;
                color.a = Mathf.Clamp01(elapsedTime / fadeOutDuration); // 투명도를 점진적으로 증가
                fadeOutPanel.color = color;
            }
            yield return null;
        }

        // 페이드아웃 완료 후 게임오버 처리 (씬 전환 또는 게임오버 UI 활성화)
        Debug.Log("Game Over!");
        // 예: UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // 지정된 AudioClip 재생
        }
    }
}
