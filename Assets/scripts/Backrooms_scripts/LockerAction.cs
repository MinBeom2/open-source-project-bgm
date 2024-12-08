using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerAction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject lockerOpenText;
    public GameObject lockerCloseText;
    public string lockerOpenAnimName, lockerCloseAnimName;

    public AudioClip lockerOpenSound;
    public AudioClip lockerCloseSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lockerOpenText.SetActive(false);
        lockerCloseText.SetActive(false);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.tag == "Locker")
            {
                GameObject Locker = hit.collider.transform.root.gameObject;
                Animator lockerAnim = Locker.GetComponent<Animator>(); 
                AudioSource lockerAudio = Locker.GetComponent<AudioSource>(); // ĳ��ݿ��� AudioSource ��������

                if (lockerAnim.GetCurrentAnimatorStateInfo(0).IsName(lockerCloseAnimName))
                {
                    lockerCloseText.SetActive(false);
                    lockerOpenText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        lockerAnim.ResetTrigger("Close");
                        lockerAnim.SetTrigger("Open");
                        // ĳ��� ���� �Ҹ� ���
                        PlaySound(lockerAudio, lockerOpenSound);
                    }
                }

                if (lockerAnim.GetCurrentAnimatorStateInfo(0).IsName(lockerOpenAnimName))
                {
                    lockerOpenText.SetActive(false);
                    lockerCloseText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        lockerAnim.ResetTrigger("Open");
                        lockerAnim.SetTrigger("Close");
                        // ĳ��� ���� �Ҹ� ���
                        PlaySound(lockerAudio, lockerCloseSound);
                    }
                }
            }
            else
            {
                lockerOpenText.SetActive(false);
                lockerCloseText.SetActive(false);
            }
        }
    }
    void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // ������ AudioClip ���
        }
    }
}
