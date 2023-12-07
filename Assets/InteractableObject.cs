using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactRange = 5f;
    public float distanceInFront = 1f;
    public float heightOffset = 0.5f;
    public AudioClip pickupSound;
    public AudioClip releaseSound;

    private Camera playerCamera;
    private bool isObjectPicked = false;
    private Transform pickedObject;
    private AudioSource audioSource;

    void Start()
    {
        playerCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isObjectPicked)
            {
                ReleaseObject();
            }
            else
            {
                TryPickupObject();
            }
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                PickupObject(hit.transform);
            }
        }
    }

    void PickupObject(Transform objToPickup)
    {
        isObjectPicked = true;
        pickedObject = objToPickup;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        pickedObject.GetComponent<Collider>().enabled = false;

        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

        Vector3 playerForward = playerCamera.transform.forward;
        Vector3 newPosition = playerCamera.transform.position + playerForward * distanceInFront;
        newPosition.y -= heightOffset;

        pickedObject.position = newPosition;

        pickedObject.SetParent(playerCamera.transform);
    }

    void ReleaseObject()
    {
        isObjectPicked = false;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.GetComponent<Collider>().enabled = true;

        if (releaseSound != null)
        {
            audioSource.PlayOneShot(releaseSound);
        }

        pickedObject.SetParent(null);
        pickedObject = null;
    }
}
