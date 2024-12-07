using UnityEngine;

public class PlayerOnTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyJumpScare; // 활성화할 Enemy Jump Scare 오브젝트
    public GameObject jumpScareImage; // 활성화할 Jump Scare 이미지 UI
    public GameObject playerObject; // Player 오브젝트 (PlayerController 스크립트를 포함)

    private Collider enemyCollider; // 자신의 Collider
    private MonoBehaviour playerController; // PlayerController 스크립트

    private void Start()
    {
        // 자신의 Collider 가져오기
        enemyCollider = GetComponent<Collider>();
        if (enemyCollider == null)
        {
            Debug.LogError("Collider component is missing on this GameObject!");
        }

        // 초기 상태에서 JumpScareImage와 EnemyJumpScare 비활성화
        if (enemyJumpScare != null)
        {
            enemyJumpScare.SetActive(false);
        }
        else
        {
            Debug.LogError("EnemyJumpScare object is not assigned!");
        }

        if (jumpScareImage != null)
        {
            jumpScareImage.SetActive(false);
        }


        // PlayerController 스크립트 가져오기
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<MonoBehaviour>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController script is missing on the Player object!");
            }
        }
        else
        {
            Debug.LogError("Player object is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyCollider != null && other.gameObject == playerObject) // Player 오브젝트와 충돌 확인
        {
            // EnemyJumpScare 활성화
            if (enemyJumpScare != null)
            {
                enemyJumpScare.SetActive(true);
            }

            // JumpScareImage 활성화
            if (jumpScareImage != null)
            {
                jumpScareImage.SetActive(true);
            }

            // PlayerController 스크립트 비활성화
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            // 본인 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }
}
