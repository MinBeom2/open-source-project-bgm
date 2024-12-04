using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.SetSoundVolume(0.3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void creature()
    {
        SoundManager.PlaySound("creature_scream");
    }
}
