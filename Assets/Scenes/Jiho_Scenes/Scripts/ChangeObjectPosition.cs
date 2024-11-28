using UnityEngine;

public class ChangeObjectPosition : MonoBehaviour
{
    // �̵���ų ��� ������Ʈ��
    public GameObject[] targetObjects;

    // �� ������Ʈ���� ���ο� ��ġ��
    public Vector3[] newPositions;

    private void OnTriggerEnter(Collider other)
    {
        // Player�� Ʈ���ſ� ��Ҵ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            // ��� ������Ʈ���� ��ȸ�ϸ� ��ġ ����
            for (int i = 0; i < targetObjects.Length; i++)
            {
                if (i < newPositions.Length)
                {
                    targetObjects[i].transform.position = newPositions[i];
                }
            }

            // ����� �޽���
            Debug.Log("All target objects have been moved.");
        }
    }
}
