using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    SoundManager soundmanager;

    public AudioSource[] bgm;
    // Start is called before the first frame update
    void Start()
    {
        soundmanager = transform.GetComponent<SoundManager>();
        SoundManager.SetSoundVolume(0.3f);
        SoundManager.PlaySound("horror_bgm");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changebgm()
    {
        bgm[0].enabled = false;
        bgm[1].enabled = true;

    }

    IEnumerator stop_sound(SMSound s_obj)
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.Stop(s_obj);
    }

    public void creature()
    {
        SMSound scream = SoundManager.PlaySound("creature_scream");
        stop_sound(scream);
        
    }

    public void gameover()
    {
        SMSound gameover = SoundManager.PlaySound("gameover");
        stop_sound(gameover);
    }

    public void piercing()
    {
        SMSound piercing = SoundManager.PlaySound("piercing");
        stop_sound(piercing);
    }

    public void door()
    {
        SMSound door = SoundManager.PlaySound("door");
        stop_sound(door);
    }

    public void lockedDoor()
    {
        SMSound lockedDoor = SoundManager.PlaySound("lockedDoor");
        stop_sound(lockedDoor);
    }
}
