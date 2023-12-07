using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    public GameObject footstep;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
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
        // Play your audio source here
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);
        // Stop your audio source here
    }
}
