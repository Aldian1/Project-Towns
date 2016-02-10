using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _CharacterController : MonoBehaviour
{

    public GameObject GUI;
    public bool allowInput = false;

    private bool inventoryOpened = false;
    private GameObject tempGameObject;
    private GameObject[] tempGameObjects;
    private GameObject[] triggersEntered;
    private GameObject inventoryCanvas;
    private bool raycast = false;

    void Awake()
    {
        tempGameObjects = new GameObject[10];
        triggersEntered = new GameObject[10];
        inventoryCanvas = GUI.transform.Find("Inventory").gameObject;
        allowInput = true;

    }

    void Update()
    {
        #region Raycast
        if (raycast)
        {
            //Debug.Log("Do the raycast!");
            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 3;
            //Debug.DrawRay(transform.position + new Vector3(0, 1, 0), forward, Color.green);
            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), forward, out hit))
            {
                //Debug.Log(hit.transform.name);
                hit.transform.GetComponent<_ScriptManager>().beingLookedAt = true;
            }
        }
        #endregion

        if (allowInput) //Everything in here requires allowInput to be true
        {
            #region Inventory controller
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!inventoryOpened)
                {
                    int a = 0;
                    foreach (Transform child in GUI.transform)
                    {
                        if (child.gameObject.active)
                        {
                            tempGameObjects[a] = child.gameObject;
                            //Debug.Log("Added: " + child.gameObject + " to array in position: " + a);
                            a++;
                        }
                    }


                    foreach (GameObject panel in tempGameObjects)
                    {
                        if (panel != null)
                        {
                            panel.SetActive(false);
                        }
                    }

                    this.GetComponent<Character_Animations>().enabled = false;
                    inventoryCanvas.SetActive(true);
                    inventoryOpened = true;
                }
                else
                {
                    this.GetComponent<Character_Animations>().enabled = true;
                    inventoryCanvas.SetActive(false);
                    inventoryOpened = false;

                    foreach (GameObject panel in tempGameObjects)
                    {
                        if (panel != null)
                        {
                            panel.SetActive(true);
                        }
                    }
                    tempGameObjects = new GameObject[10];
                }
            }
            #endregion
        }
    }

    public void SetPlayerInput (bool state)
    {
        allowInput = state;
    }

    public void ReceiveTrigger(GameObject Object)
    {
        for (int i = 0; i < 10; i++)
        {
            if (triggersEntered[i] == null)
            {
                //Debug.Log("added trigger");
                triggersEntered[i] = Object;
                break;
            }
        }
        raycast = true;
    }

    public void RemoveTrigger(GameObject Object)
    {
        for (int i = 0; i < 10; i++)
        {
            if (triggersEntered[i] == Object)
            {
                //Debug.Log("removed trigger");
                triggersEntered[i] = null;
                break;
            }
        }

        int tempNumber = 0;
        foreach (GameObject go in triggersEntered)
        {
            if (go != null)
            {
                tempNumber++;
            }
        }
        if (tempNumber == 0)
        {
            raycast = false;
        }
    }
}
