using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject chestOpenText;
    public GameObject chestCloseText;
    public string chestOpenAnimName, chestCloseAnimName;

    public AudioClip chestOpenSound;
    public AudioClip chestCloseSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        chestOpenText.SetActive(false);
        chestCloseText.SetActive(false);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.tag == "Chest")
            {
                GameObject Chest = hit.collider.transform.root.gameObject;
                Animator chestAnim = Chest.GetComponent<Animator>();
                AudioSource chestAudio = Chest.GetComponent<AudioSource>(); // ���ڿ��� AudioSource ��������

                if (chestAnim.GetCurrentAnimatorStateInfo(0).IsName(chestCloseAnimName))
                {
                    chestCloseText.SetActive(false);
                    chestOpenText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        chestAnim.ResetTrigger("Close");
                        chestAnim.SetTrigger("Open");
                        // ���� ���� �Ҹ� ���
                        PlaySound(chestAudio, chestOpenSound);
                    }
                }

                if (chestAnim.GetCurrentAnimatorStateInfo(0).IsName(chestOpenAnimName))
                {
                    chestOpenText.SetActive(false);
                    chestCloseText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        chestAnim.ResetTrigger("Open");
                        chestAnim.SetTrigger("Close");
                        // ���� ���� �Ҹ� ���
                        PlaySound(chestAudio, chestCloseSound);
                    }
                }
            }
            else
            {
                chestOpenText.SetActive(false);
                chestCloseText.SetActive(false);
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
