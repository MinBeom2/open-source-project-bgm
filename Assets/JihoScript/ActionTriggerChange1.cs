using UnityEngine;

public class ActionTriggerChange1 : MonoBehaviour
{
    public GameObject[] targetObjects;

    public GameObject Doll;

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
                }
            }
        Doll.transform.position = newPositions[11];
        Doll.transform.rotation = newRotations[11];
        Debug.Log("All target objects have been moved.");
    }
}
