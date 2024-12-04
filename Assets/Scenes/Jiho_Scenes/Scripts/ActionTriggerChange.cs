using UnityEngine;

public class ActionTriggerChange : MonoBehaviour
{
    public GameObject[] targetObjects;

    public Vector3[] newPositions;
    public Quaternion[] newRotations;

    public void Action_change_position()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (i < newPositions.Length)
            {
                targetObjects[i].transform.position = newPositions[i];
                targetObjects[i].transform.rotation = newRotations[i];
                if (i == 1)
                {
                    Transform obj = targetObjects[i].transform.Find("Zweihander");
                    obj.gameObject.SetActive(false);
                }
            }
        }
        Debug.Log("All target objects have been moved.");
    }
}
