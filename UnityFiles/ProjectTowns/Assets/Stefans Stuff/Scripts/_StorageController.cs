using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _StorageController : MonoBehaviour {

    public GameObject GUI;
    public GameObject mainCamera;
    public GameObject playerObject;
    public string tooltipTextValue;
    public bool beingLookedAt = false;

    private GameObject tooltipWindow;
    private GameObject tooltipText;
    private GameObject storageWindow;
    private GameObject storageText;
    private bool tooltipShown = false;
    private bool insideTrigger = false;
    private bool storageOpened = false;

    // Use this for initialization
    void Awake() {
        tooltipWindow = transform.Find("TooltipCanvas").gameObject;
        tooltipWindow.transform.Find("Panel").Find("Text").gameObject.GetComponent<Text>().text = tooltipTextValue;

        storageWindow = GUI.transform.Find("StorageWindow").gameObject;
        storageText = storageWindow.transform.Find("Text").gameObject;
    }

    void Update()
    {
        Quaternion newRotation = Quaternion.LookRotation(tooltipWindow.transform.position - mainCamera.transform.position);
        newRotation.x = 0;
        newRotation.z = 0;
        tooltipWindow.transform.rotation = Quaternion.Slerp(tooltipWindow.transform.rotation, newRotation, 5 * Time.deltaTime);

        if (!storageOpened)
        {
            if (beingLookedAt)
            {
                if(!tooltipShown)
                {
                    tooltipShown = true;
                    tooltipWindow.gameObject.SetActive(true);
                    tooltipWindow.transform.rotation = Quaternion.LookRotation(tooltipWindow.transform.position - mainCamera.transform.position);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnterStorage();
                }
            }
            else
            {
                if (tooltipShown)
                {
                    tooltipShown = false;
                    tooltipWindow.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitStorage();
            }
        }
    }

    void EnterStorage()
    {
        //Debug.Log("EnterStorage");

        //Enter village mode
        playerObject.GetComponent<Character_Animations>().enabled = false;
        playerObject.GetComponent<_CharacterController>().enabled = false;
        tooltipWindow.gameObject.SetActive(false);
        storageText.GetComponent<Text>().text = "Welcome in your storage. Press E to return.";
        storageWindow.gameObject.SetActive(true);
        ToggleTooltip();
        storageOpened = true;

    }

    void ExitStorage()
    {
        //Debug.Log("ExitStorage");

        //Leave village mode
        storageWindow.gameObject.SetActive(false);
        playerObject.GetComponent<Character_Animations>().enabled = true;
        playerObject.GetComponent<_CharacterController>().enabled = true;
        tooltipWindow.gameObject.SetActive(true);
        ToggleTooltip();
        storageOpened = false;
    }

    public void ToggleTooltip()
    {
        if (!storageOpened)
        {
             tooltipWindow.gameObject.SetActive(false);
        }
        else
        {
            tooltipWindow.gameObject.SetActive(true);
        }
    }
}
