using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InteractInstructor : MonoBehaviour
{
    public TMP_Text interactionText;
    public float interactionRange = 2f;

    void Update()
    {
        CheckForInteractableObject();
    }

    void CheckForInteractableObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Interactable"))
            {
                interactionText.text = "Press E to Interact";

            }
            else
            {
                interactionText.text = "";
            }
        }
        else
        {
            interactionText.text = "";
        }
    }
}
