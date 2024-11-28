using UnityEngine;

public class ActionTriggerChange : MonoBehaviour
{
    // 이동시킬 대상 오브젝트
    public GameObject targetObject;

    // 새로운 위치
    public Vector3 newPosition;

    // Update 메서드는 매 프레임 호출됩니다.
    private void Update()
    {
        // 특정 키 (예: 'E')를 눌렀을 때 위치 변경
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 대상 오브젝트의 위치 변경
            targetObject.transform.position = newPosition;

            // 디버그 메시지
            Debug.Log("Target object moved to new position: " + newPosition);
        }
    }
}
