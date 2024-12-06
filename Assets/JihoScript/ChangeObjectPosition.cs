using Unity.VisualScripting;
using UnityEngine;

public class ChangeObjectPosition : MonoBehaviour
{
    public GameObject[] targetObjects;

    public Vector3[] newPositions;
    public Quaternion[] newRotations;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < targetObjects.Length; i++)
            {
                if (i < newPositions.Length)
                {
                    targetObjects[i].transform.position = newPositions[i];
                    targetObjects[i].transform.rotation = newRotations[i];
}
            }

            Debug.Log("All target objects have been moved.");
            Destroy(gameObject);
        }
    }
}
