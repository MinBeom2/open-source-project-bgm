using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MoveMode
{
    patrol, chase, wait
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class AI : MonoBehaviour
{
    public MoveMode moveMode;
    public Animator animator;

    private float footstepInterval = 0.4f;
    private float footstepTimer = 0f;

    [Header("Steering")]
    public float patrolSpeed;
    public float chaseSpeed;
    public float maxTimeChasing;
    public float maxTimeWaiting;
    public float radiusHit;

    [Header("Field Of View")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    [Header("Transform")]
    public Transform[] patrolPoint;
    public NavMeshAgent agent;
    public Transform currentTarget;

    [Header("Footstep Audio")]
    public AudioClip[] footstepClips;
    private AudioSource audioSource;
    private int currentFootstepIndex = 0;

    Vector3 destination, SoundPositionHeared;
    int index_patrolpoint;
    float currentTimeChasing, currentTimeWaiting;
    bool isDetectTarget, isHearingSound;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent.stoppingDistance < 0.5f)
            agent.stoppingDistance = 0.5f;

        audioSource = GetComponent<AudioSource>();
        if (footstepClips.Length == 0)
        {
            Debug.LogWarning("No footstep sounds assigned!");
        }
    }

    void Update()
    {
        Debug.Log($"Current MoveMode: {moveMode}");
        switch (moveMode)
        {
            case MoveMode.patrol:
                Patroling();
                break;
            case MoveMode.chase:
                Chasing();
                break;
            case MoveMode.wait:
                Waiting();
                break;
        }

        FieldOfView();
        Animations();
    }

    void Animations()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned.");
            return;
        }

        bool isChasing = moveMode == MoveMode.chase;
        animator.SetBool("chase", isChasing);
        Debug.Log($"Animator chase set to: {isChasing}");
    }

    void FieldOfView()
    {
        Collider[] range = Physics.OverlapSphere(transform.position, viewRadius, targetMask, QueryTriggerInteraction.Ignore);

        if (range.Length > 0)
        {
            currentTarget = range[0].transform;
            Vector3 direction = (currentTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < viewAngle / 2)
            {
                float m_distance = Vector3.Distance(transform.position, currentTarget.position);

                if (!Physics.Raycast(transform.position, direction, m_distance, obstacleMask, QueryTriggerInteraction.Ignore))
                {
                    isDetectTarget = true;

                    if (moveMode != MoveMode.chase)
                    {
                        Debug.Log("Target detected, switching to chase mode!");
                        SwitchMoveMode(MoveMode.chase);
                    }
                }
                else
                {
                    isDetectTarget = false;
                    Debug.Log("Target blocked by obstacle.");
                }
            }
            else
            {
                isDetectTarget = false;
                Debug.Log("Target out of view angle.");
            }
        }
        else
        {
            isDetectTarget = false;
            Debug.Log("No targets in view radius.");
        }
    }

    void Waiting()
    {
        currentTimeWaiting += Time.deltaTime;
        if (currentTimeWaiting > maxTimeWaiting)
        {
            Debug.Log("Wait time exceeded, switching to patrol.");
            SwitchMoveMode(MoveMode.patrol);
        }
    }

    void Patroling()
    {
        agent.speed = patrolSpeed;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            SwitchMoveMode(MoveMode.wait);
            isHearingSound = false;
        }
        else
        {
            PlayFootstepSound();
        }
    }

    void Chasing()
    {
        agent.speed = chaseSpeed;
        agent.destination = currentTarget.position;

        if (Physics.OverlapSphere(transform.position, radiusHit, targetMask, QueryTriggerInteraction.Ignore).Length > 0)
        {
            Debug.Log("Target hit! Game over.");
        }

        if (currentTimeChasing > maxTimeChasing)
        {
            Debug.Log("Chase time exceeded, switching to wait.");
            SwitchMoveMode(MoveMode.wait);
        }
        else if (!isDetectTarget)
        {
            currentTimeChasing += Time.deltaTime;
        }
        else
        {
            currentTimeChasing = 0;
        }

        PlayFootstepSound();
    }

    void SwitchMoveMode(MoveMode m_moveMode)
    {
        Debug.Log($"SwitchMoveMode called with: {m_moveMode}");

        if (m_moveMode == MoveMode.chase)
        {
            Debug.Log("Switching to chase mode.");
            isHearingSound = false;
            currentTimeChasing = 0;
        }

        moveMode = m_moveMode;
    }

    void PlayFootstepSound()
    {
        if (footstepClips.Length == 0 || !agent.hasPath || agent.velocity.magnitude < 0.1f) return;

        footstepTimer += Time.deltaTime;

        if (footstepTimer >= footstepInterval)
        {
            audioSource.clip = footstepClips[currentFootstepIndex];
            audioSource.Play();

            currentFootstepIndex = (currentFootstepIndex + 1) % footstepClips.Length;
            footstepTimer = 0f;
        }
    }

    void OnDrawGizmos()
    {
        if (agent == null) return;

        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        if (currentTarget != null && isDetectTarget)
            Gizmos.DrawLine(transform.position, currentTarget.position);

        float halfFov = viewAngle / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFov, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFov, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * viewRadius);
        Gizmos.DrawRay(transform.position, rightRayDirection * viewRadius);
    }
}
