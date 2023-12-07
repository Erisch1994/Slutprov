using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    public GameObject footstep;
    private bool isWalking;

    void Start()
    {
        footstep.SetActive(false);
    }

    void Update()
    {
        HandleFootstepsInput();
    }

    void HandleFootstepsInput()
    {
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            if (!isWalking)
            {
                footsteps();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                StopFootsteps();
                isWalking = false;
            }
        }
    }

    void footsteps()
    {
        footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);

    }
}
