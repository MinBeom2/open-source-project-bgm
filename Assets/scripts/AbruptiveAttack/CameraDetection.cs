using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CameraDetection : MonoBehaviour
{
    public Camera chaserCamera; // Chaser에 달린 카메라
    public float detectionRange = 20f; // 카메라 감지 거리
    public float chaseSpeed = 6.5f; // 추적 시 이동 속도
    public float patrolResumeDelay = 5f; // 추적 종료 후 순찰 재개 지연 시간
    public float rotationSpeed = 5f; // Chaser가 회전하는 속도

    [Header("Footstep Settings")]
    public AudioSource audioSource; // 발소리 재생용 AudioSource
    public AudioClip[] footStepSounds; // 발소리 AudioClip 배열
    public float footStepInterval = 0.5f; // 발소리 간격

    private Animator animator;
    private NavMeshAgent navMeshAgent; // Chaser의 NavMeshAgent
    private Transform playerTransform; // Player의 Transform
    private AIPatrol patrolScript; // AIPatrol 스크립트
    private bool isChasing = false; // 추적 중인지 여부
    private float footStepTimer = 0f; // 발소리 간격 타이머

    private float defaultSpeed; // NavMeshAgent의 초기 속도 저장

    void Start()
    {
        // NavMeshAgent 컴포넌트 가져오기
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on this GameObject.");
            return;
        }

        // NavMeshAgent 초기 속도 저장
        defaultSpeed = navMeshAgent.speed;

        // AIPatrol 스크립트 가져오기
        patrolScript = GetComponent<AIPatrol>();
        if (patrolScript == null)
        {
            Debug.LogError("AIPatrol script is missing on this GameObject.");
            return;
        }

        // Animator 가져오기
        GameObject enemyBody = GameObject.Find("EnemyBody");
        if (enemyBody != null)
        {
            animator = enemyBody.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("EnemyBody GameObject or Animator component is missing.");
        }

        // Player 태그로 객체 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // AudioSource 확인
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    void Update()
    {
        if (IsPlayerVisible())
        {
            if (!isChasing)
            {
                StartChase();
            }
            else
            {
                UpdateRotation(); // 계속해서 Player를 바라보도록 업데이트
                HandleFootSteps(); // 발소리 처리
            }
        }
    }

    private bool IsPlayerVisible()
    {
        if (chaserCamera == null || playerTransform == null) return false;

        // 카메라의 Viewport에서 플레이어 위치 확인
        Vector3 viewportPosition = chaserCamera.WorldToViewportPoint(playerTransform.position);

        // 플레이어가 카메라의 시야 안에 있는지 확인
        return viewportPosition.z > 0 && viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;
    }

    private void StartChase()
    {
        isChasing = true;

        // AIPatrol 스크립트 비활성화
        patrolScript.enabled = false;

        // NavMeshAgent 속도 조정
        navMeshAgent.speed = chaseSpeed;

        // Animator의 "isChasing" 값을 true로 설정
        if (animator != null)
        {
            animator.SetBool("isChasing", true);
        }

        // 추적 시작
        StartCoroutine(ChasePlayer());
    }

    private IEnumerator ChasePlayer()
    {
        navMeshAgent.stoppingDistance = 0.5f;

        while (IsPlayerVisible())
        {
            navMeshAgent.SetDestination(playerTransform.position);
            yield return null;
        }

        // 추적 종료 처리
        isChasing = false;

        // Animator의 "isChasing" 값을 false로 설정
        if (animator != null)
        {
            animator.SetBool("isChasing", false);
        }

        // Animator의 "isWaiting" 값을 true로 설정
        if (animator != null)
        {
            animator.SetBool("isWaiting", true);
        }

        // NavMeshAgent 속도를 초기 값으로 복구
        navMeshAgent.speed = defaultSpeed;

        // 딜레이 후 AIPatrol 재활성화
        yield return new WaitForSeconds(patrolResumeDelay);

        // Animator의 "isWaiting" 값을 false로 설정
        if (animator != null)
        {
            animator.SetBool("isWaiting", false);
        }

        patrolScript.enabled = true;
    }

    private void UpdateRotation()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void HandleFootSteps()
    {
        // NavMeshAgent가 이동 중인지 확인
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            footStepTimer += Time.deltaTime;

            if (footStepTimer >= footStepInterval)
            {
                PlayRandomFootstep();
                footStepTimer = 0f;
            }
        }
        else
        {
            footStepTimer = 0f;
        }
    }

    private void PlayRandomFootstep()
    {
        if (footStepSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, footStepSounds.Length);
            AudioClip footStepClip = footStepSounds[randomIndex];
            audioSource.PlayOneShot(footStepClip);
        }
    }
}
