using UnityEngine;

public class ActionTriggerChange : MonoBehaviour
{
    // �̵���ų ��� ������Ʈ
    public GameObject targetObject;

    // ���ο� ��ġ
    public Vector3 newPosition;

    // Update �޼���� �� ������ ȣ��˴ϴ�.
    private void Update()
    {
        // Ư�� Ű (��: 'E')�� ������ �� ��ġ ����
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ��� ������Ʈ�� ��ġ ����
            targetObject.transform.position = newPosition;

            // ����� �޽���
            Debug.Log("Target object moved to new position: " + newPosition);
        }
    }
}
