using UnityEngine;
using TMPro; 


public class InteractionSystem : MonoBehaviour
{
    public float interactionDistance = 5f;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactionText; 

    private GameObject currentTarget;

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
                Game gameScript = currentTarget.GetComponent<Game>();
                if (gameScript != null)
                {
                    if (currentTarget.CompareTag("Book"))
                    {
                        Debug.Log("1");
                        gameScript.SceneToSave();
                    }
                    else if (currentTarget.CompareTag("Door"))
                    {
                        Debug.Log("1");
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