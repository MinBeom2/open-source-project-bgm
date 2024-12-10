using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum MoveMode1
{
    patrol, chase, wait
}

[RequireComponent(typeof(NavMeshAgent))]

public class playgrounAI : MonoBehaviour
{
    public MoveMode1 moveMode1;
    public Animator animator;

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

    Vector3 destination, SoundPositionHeared;
    int index_patrolpoint;
    float currentTimeChasing, currentTimeWaiting;
    bool isDetectTarget, isHearingSound;

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent.stoppingDistance < 0.5f)
            agent.stoppingDistance = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveMode1)
        {
            case MoveMode1.patrol:
                Patroling();
                break;
            case MoveMode1.chase:
                Chasing();
                break;
            case MoveMode1.wait:
                Waiting();
                break;
        }

        FieldOfView();
        Animations();
    }

    void Animations()
    {
        if (animator == null) return;

        animator.SetBool("chase", moveMode1 == MoveMode1.chase);
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

                    if (moveMode1 != MoveMode1.chase)
                    {
                        SwitchMoveMode1(MoveMode1.chase);
                    }
                }
                else
                {
                    isDetectTarget = false;
                }
            }
            else
            {
                isDetectTarget = false;
            }
        }
        else
        {
            isDetectTarget = false;
        }
    }

    void Waiting()
    {
        if (currentTimeWaiting > maxTimeWaiting)
        {
            SwitchMoveMode1(MoveMode1.patrol);
        }
        else
        {
            currentTimeWaiting += Time.deltaTime;
        }
    }

    void Patroling()
    {
        agent.speed = patrolSpeed;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            SwitchMoveMode1(MoveMode1.wait);
            isHearingSound = false;
        }
    }

    bool isHit;

    void Chasing()
    {
        agent.speed = chaseSpeed;
        agent.destination = currentTarget.position;

        Collider[] col = Physics.OverlapSphere(transform.position, radiusHit, targetMask, QueryTriggerInteraction.Ignore);

        if (col.Length > 0 && !isHit)
        {
            Debug.Log("permainan berakhir");
            isHit = true;
        }

        if (currentTimeChasing > maxTimeChasing)
        {
            SwitchMoveMode1(MoveMode1.wait);
        }
        else if (!isDetectTarget)
        {
            currentTimeChasing += Time.deltaTime;
        }
        else if (isDetectTarget)
        {
            currentTimeChasing = 0;
        }
    }

    void SwitchMoveMode1(MoveMode1 m_moveMode1)
    {
        switch (m_moveMode1)
        {
            case MoveMode1.patrol:

                int lastindex = index_patrolpoint;
                int newindex = Random.Range(0, patrolPoint.Length);

                if (lastindex == newindex)
                {
                    newindex = Random.Range(0, patrolPoint.Length);
                    return;
                }

                index_patrolpoint = newindex;

                if (isHearingSound)
                    destination = SoundPositionHeared;
                else
                    destination = patrolPoint[index_patrolpoint].position;

                agent.destination = destination;

                break;
            case MoveMode1.chase:
                isHearingSound = false;
                currentTimeChasing = 0;
                break;
            case MoveMode1.wait:
                agent.destination = transform.position;
                currentTimeWaiting = 0;
                break;
        }

        moveMode1 = m_moveMode1;
        Debug.Log("change move mode to " + moveMode1.ToString());
    }

    public void HearingSound(Vector3 m_destination)
    {
        if (moveMode1 == MoveMode1.chase) return;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(m_destination, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {

            SoundPositionHeared = m_destination;
            isHearingSound = true;

            SwitchMoveMode1(MoveMode1.patrol);
            Debug.Log("hearing sound");

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
