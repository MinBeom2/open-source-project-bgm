using UnityEngine;

public class ChangeObjectPosition : MonoBehaviour
{
    // 이동시킬 대상 오브젝트들
    public GameObject[] targetObjects;

    // 각 오브젝트들의 새로운 위치들
    public Vector3[] newPositions;

    private void OnTriggerEnter(Collider other)
    {
        // Player가 트리거에 닿았는지 확인
        if (other.CompareTag("Player"))
        {
            // 대상 오브젝트들을 순회하며 위치 변경
            for (int i = 0; i < targetObjects.Length; i++)
            {
                if (i < newPositions.Length)
                {
                    targetObjects[i].transform.position = newPositions[i];
                }
            }

            // 디버그 메시지
            Debug.Log("All target objects have been moved.");
        }
    }
}
