using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class InteractionSystem : MonoBehaviour
{
    public float interactionDistance = 5f;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactionText;

    private GameObject currentTarget;
    public GameObject SavePanel;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Update()
    {
        CheckForInteractable();
        HandleInteraction();

        void CheckForInteractable()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Book") || hitObject.CompareTag("Door"))
                {
                    currentTarget = hitObject;
                    ShowInteractionText(hitObject.tag);
                }
            }
            else
            {
                currentTarget = null;
                interactionText.gameObject.SetActive(false);
            }
        }

        void HandleInteraction()
        {
            if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
            {
                aisleManager gameScript = currentTarget.GetComponent<aisleManager>();
                if (gameScript != null)
                {
                    if (currentTarget.CompareTag("Book"))
                    {
                        audioManager.PlaySFX(audioManager.Book);
                        gameScript.SceneToSave();
                        Time.timeScale = 0f;
                        audioManager.SetVolume(audioManager.pauseVolume);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        EventSystem.current.SetSelectedGameObject(null);
                        SavePanel.SetActive(true);
                    }
                    else if (currentTarget.CompareTag("Door"))
                    {
                        audioManager.PlaySFX(audioManager.DoorOpen);
                        gameScript.SceneToNext();
                    }
                }
            }
        }

        void ShowInteractionText(string tag)
        {
            interactionText.gameObject.SetActive(true);

            if (tag == "Book")
            {
                interactionText.text = "Press [E] to Save";
            }
            else if (tag == "Door")
            {
                interactionText.text = "Press [E] to Next Stage";
            }
        }
    }
}