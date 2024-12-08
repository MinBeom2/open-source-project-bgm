using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMoving : MonoBehaviour
{
    public GameObject L_eye;
    public Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        transform.position = new Vector3(L_eye.transform.localPosition.x+vec.x, L_eye.transform.localPosition.y+vec.y, L_eye.transform.localPosition.z-0.026f+vec.z);
        //transform.rotation = L_eye.transform.localRotation;
    }
}
