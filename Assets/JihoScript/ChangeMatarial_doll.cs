using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMatarial_doll : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Doll_mat;
    public Material newMaterial;
    public void changematerial_doll()
    {
        Renderer renderer = GetComponent<Renderer>();
        for (int i = 0; i < 4; i++)
        {
            Doll_mat[i].GetComponent<SkinnedMeshRenderer>().material = newMaterial;
        }
    }
}
