using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMatarial : MonoBehaviour
{
    // Start is called before the first frame update

    public Material newMaterial;
    public GameObject Door;
    public void changematerial()
    {
        Renderer renderer = GetComponent<Renderer>();

        Material[] materials = renderer.materials;
        materials[1] = newMaterial;
        renderer.materials = materials;

        Door.GetComponent<DoorOpening>().Open();
    }
}
