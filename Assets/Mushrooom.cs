using UnityEngine;
using UnityEngine.UI;

public class Mushrooom : MonoBehaviour
{
    public float interactionDistance = 5f;
    public KeyCode interactionKey = KeyCode.E;
    public GameObject interactionPanel;
    public Text interactionText;
    public Button yesButton;
    public Button noButton;

    private Camera playerCamera;
    private bool isInRange = false;
    private bool isInteractionPanelActive = false;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        playerCamera = Camera.main;


        interactionPanel.SetActive(false);


        yesButton.onClick.AddListener(EatMushroom);
        noButton.onClick.AddListener(CloseInteractionPanel);
    }

    void Update()
    {
        if (!isInteractionPanelActive)
        {
            CheckInteractionDistance();

            if (isInRange && Input.GetKeyDown(interactionKey))
            {

                ShowInteractionPanel();
            }
        }
    }

    void CheckInteractionDistance()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Mushroom"))
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
                CloseInteractionPanel();
            }
        }
        else
        {
            isInRange = false;
            CloseInteractionPanel();
        }
    }

    void ShowInteractionPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;


        interactionPanel.SetActive(true);
        isInteractionPanelActive = true;

        playerCamera.GetComponent<Cam>().SetInteractionPanelActive(true);
    }

    void CloseInteractionPanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;


        interactionPanel.SetActive(false);
        isInteractionPanelActive = false;

        playerCamera.GetComponent<Cam>().SetInteractionPanelActive(false);
    }

    void EatMushroom()
    {

        Destroy(GameObject.FindGameObjectWithTag("Mushroom"));


        CloseInteractionPanel();
    }
}
