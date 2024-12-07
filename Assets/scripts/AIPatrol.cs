using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints; // 순찰 지점 배열
    private int currentPointIndex = 0;

    private NavMeshAgent navMeshAgent; // NavMeshAgent 컴포넌트

    [Header("Footstep Settings")]
    public AudioSource audioSource; // 발소리 재생용 AudioSource
    public AudioClip[] footStepSounds; // 발소리 AudioClip 배열
    public float footStepInterval = 0.5f; // 발소리 간격

    private float footStepTimer = 0f; // 발소리 간격 타이머

    void Start()
    {
        // NavMeshAgent 컴포넌트 가져오기
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on this GameObject.");
            return;
        }

        // NavMeshAgent 기본 설정
        navMeshAgent.stoppingDistance = 0.5f;

        // 첫 번째 순찰 지점 설정
        SetNextDestination();

        // AudioSource 확인
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned.");
        }
    }

    void Update()
    {
        Patrol();
        HandleFootSteps();
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        // 목표 지점에 도달했는지 확인
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // 다음 순찰 지점으로 이동
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        if (patrolPoints.Length == 0) return;

        // 다음 순찰 지점으로 이동
        navMeshAgent.SetDestination(patrolPoints[currentPointIndex].position);
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
                footStepTimer = 0f; // 타이머 초기화
            }
        }
        else
        {
            footStepTimer = 0f; // 멈췄을 때 타이머 초기화
        }
    }

    private void PlayRandomFootstep()
    {
        if (footStepSounds.Length > 0 && audioSource != null)
        {
            // 랜덤으로 사운드 선택
            int randomIndex = Random.Range(0, footStepSounds.Length);
            AudioClip footStepClip = footStepSounds[randomIndex];

            // 발소리 재생
            audioSource.PlayOneShot(footStepClip);
        }
    }
}
