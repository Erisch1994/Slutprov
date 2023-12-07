using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float openAngle = 90f;
    public float rotationSpeed = 5f;
    public float interactionDistance = 5f;

    private bool isOpen = false;
    private Quaternion originalRotation;
    private Camera playerCamera;

    void Start()
    {
        originalRotation = transform.rotation;
        playerCamera = Camera.main;
    }

    void Update()
    {
        CheckInteractionDistance();

        if (isOpen)
        {
            float targetAngle = Quaternion.Euler(0, openAngle, 0).eulerAngles.y;
            float currentAngle = transform.rotation.eulerAngles.y;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, newAngle, 0);
        }
    }

    void ToggleGate()
    {
        if (isOpen)
        {
            CloseGate();
        }
        else
        {
            if (IsPlayerFacingGate())
            {
                OpenGate();
            }
        }
    }

    void OpenGate()
    {
        isOpen = true;
    }

    void CloseGate()
    {
        isOpen = false;
        transform.rotation = originalRotation;
    }

    void CheckInteractionDistance()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Gate"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleGate();
                }
            }
        }
    }

    bool IsPlayerFacingGate()
    {
        Vector3 toGate = (transform.position - playerCamera.transform.position).normalized;
        float dotProduct = Vector3.Dot(playerCamera.transform.forward, toGate);

        float facingThreshold = 0.1f; 

        return dotProduct >= facingThreshold;
    }
}
