using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject aiGameOver; // AI�� �� (SetActive�� ���� ����)
    public Image fadeOutPanel; // ������ �̹����� ���� UI ĵ������ �̹���
    public float fadeOutDuration = 2f; // ���̵�ƿ� ���� �ð�

    private bool isGameOver = false;

    private Movement movement;
    [SerializeField] AudioSource screamSource;
    [SerializeField] AudioSource footStepSource;

    public AudioClip scream;

    void Start()
    {
        // ó������ AI ���� ��Ȱ��ȭ
        if (aiGameOver != null)
            aiGameOver.SetActive(false);

        // ������ �̹����� ������ 0���� ���� (���� ����)
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
        Debug.Log("�浹�� ��ü: " + other.name);

        if (isGameOver) return;

        // AI�� �ݶ��̴��� ��Ҵ��� Ȯ��
        if (other.CompareTag("Enemy"))
        {
            isGameOver = true;

            movement.enabled = false;

            footStepSource.enabled = false;

            // �浹�� AI�� ��Ȱ��ȭ
            GameObject aiChaser = other.gameObject;
            aiChaser.SetActive(false);

            // AI �� Ȱ��ȭ
            if (aiGameOver != null)
                aiGameOver.SetActive(true);

            PlaySound(screamSource, scream);

            // ���̵�ƿ� ����
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
                color.a = Mathf.Clamp01(elapsedTime / fadeOutDuration); // ������ ���������� ����
                fadeOutPanel.color = color;
            }
            yield return null;
        }

        // ���̵�ƿ� �Ϸ� �� ���ӿ��� ó�� (�� ��ȯ �Ǵ� ���ӿ��� UI Ȱ��ȭ)
        Debug.Log("Game Over!");
        // ��: UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // ������ AudioClip ���
        }
    }
}
