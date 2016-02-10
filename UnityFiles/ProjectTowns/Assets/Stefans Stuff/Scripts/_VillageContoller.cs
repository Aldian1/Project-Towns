using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class _VillageContoller : MonoBehaviour {

    public GameObject playerObject;
    public GameObject GUI;
    public GameObject mainCamera;
    public GameObject villageLookout;
    public GameObject lookatPoint;
    public string tooltipTextValue;

    //dont set these by hand!
    public bool beingLookedAt = false;

    private GameObject tooltipWindow;
    private GameObject villageModeWindow;
    private GameObject villageModeText;
    private GameObject lookatTarget;
    private Vector3 lookatTargetStart;
    private bool insideTrigger = false;
    private bool villageMode = false;
    private bool tooltipShown = false;

    void Awake()
    {
        tooltipWindow = transform.Find("TooltipCanvas").gameObject;
        tooltipWindow.transform.Find("Panel").Find("Text").gameObject.GetComponent<Text>().text = tooltipTextValue;
        
        villageModeWindow = GUI.transform.Find("VillageMode").gameObject;
        villageModeText = villageModeWindow.transform.Find("Text").gameObject;

        lookatTarget = transform.Find("LookatTarget").gameObject;
        lookatTargetStart = lookatTarget.transform.position;
    }

	void Update ()
    {
        Quaternion newRotation = Quaternion.LookRotation(tooltipWindow.transform.position - mainCamera.transform.position);
        newRotation.x = 0;
        newRotation.z = 0;
        tooltipWindow.transform.rotation = Quaternion.Slerp(tooltipWindow.transform.rotation, newRotation, 5 * Time.deltaTime);

        if (!villageMode)
        {
            if (beingLookedAt)
            {
                if (!tooltipShown)
                {
                    tooltipShown = true;
                    tooltipWindow.gameObject.SetActive(true);
                    tooltipWindow.transform.rotation = Quaternion.LookRotation(tooltipWindow.transform.position - mainCamera.transform.position);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnterVillageMode();
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

            beingLookedAt = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitVillageMode();
            }
        }
    }

    void EnterVillageMode ()
    {
        //Debug.Log("EnterVillageMode");
        
        //Enter village mode
        playerObject.GetComponent<Character_Animations>().enabled = false;
        playerObject.GetComponent<_CharacterController>().enabled = false;
        mainCamera.GetComponent<SmoothFollow>().enabled = false;
        mainCamera.GetComponent<_CameraController>().MoveToLookout(villageLookout, lookatPoint);
        ToggleTooltip();
        villageMode = true;
        villageModeText.GetComponent<Text>().text = "Welcome in village mode. Press E to return.";
        villageModeWindow.gameObject.SetActive(true);
        
    }

    void ExitVillageMode ()
    {
        //Debug.Log("ExitVillageMode");

        //Leave village mode
        villageModeWindow.gameObject.SetActive(false);
        mainCamera.GetComponent<CameraControls>().enabled = false;
        mainCamera.GetComponent<SmoothFollow>().enabled = true;
        mainCamera.GetComponent<_CameraController>().SetMouseControl(false);
        playerObject.GetComponent<Character_Animations>().enabled = true;
        playerObject.GetComponent<_CharacterController>().enabled = true;
        lookatTarget.transform.position = Vector3.Lerp(lookatTarget.transform.position, lookatTargetStart, 5 * Time.deltaTime);
        ToggleTooltip();
        villageMode = false;
    }

    void ToggleTooltip ()
    {
        if(!villageMode)
        {
             tooltipWindow.gameObject.SetActive(false);
        }
        else
        {
            tooltipWindow.gameObject.SetActive(true);
        }
    }
}
