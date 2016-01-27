using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageStuff : MonoBehaviour
{
    public Rect windowRect = new Rect(20f, 20f, 120f, 50f);

    public GameObject currentactor;

    public GameObject house_ghost;

    public GameObject buildablehouse;

    public Texture2D cursorTexture;

    public CursorMode cursorMode;

    public Vector2 hotSpot = Vector2.zero;

    public bool PickResource;

    public List<GameObject> Agents = new List<GameObject>();

    public List<GameObject> housestobebuilt = new List<GameObject>();

    public GameObject villagec;

    public GameObject storage;

    private void Start()
    {
        this.CanelAllBuilding();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 1000f))
        {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.green);
            if (raycastHit.transform.tag == "ResourcePoint")
            {
                raycastHit.transform.GetChild(3).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0) && this.PickResource)
                {
                    this.CanelAllBuilding();
                    foreach (GameObject current in this.Agents)
                    {
                        if (!current.GetComponent<BasicUnitControl>().resourcebusy)
                        {
                            current.GetComponent<BasicUnitControl>().StartPath(raycastHit.transform.gameObject);
                            current.GetComponent<BasicUnitControl>().Type_ = BasicUnitControl.JobType.resource_collecting;
                            current.GetComponent<BasicUnitControl>().resourcebusy = true;
                            return;
                        }
                    }
                }
            }
            if (this.house_ghost.activeSelf)
            {
                this.house_ghost.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
                if (Input.GetKeyDown(KeyCode.Mouse0) && this.storage.GetComponent<StorageInv>().stone >= 2 && this.storage.GetComponent<StorageInv>().wood >= 3)
                {
                    this.storage.GetComponent<StorageInv>().stone -= 2;
                    this.storage.GetComponent<StorageInv>().wood -= 3;
                    foreach (GameObject current2 in this.Agents)
                    {
                        if (!current2.GetComponent<BasicUnitControl>().busy)
                        {
                            GameObject identifiedtarget = UnityEngine.Object.Instantiate(this.house_ghost, raycastHit.point, this.house_ghost.transform.rotation) as GameObject;
                            current2.GetComponent<BasicUnitControl>().StartPath(identifiedtarget);
                            current2.GetComponent<BasicUnitControl>().Type_ = BasicUnitControl.JobType.build;
                            current2.GetComponent<BasicUnitControl>().busy = true;
                            this.CanelAllBuilding();
                            return;
                        }
                    }
                    this.CanelAllBuilding();
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.CanelAllBuilding();
                }
            }
        }
        if (this.house_ghost.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            this.house_ghost.transform.Rotate(0f, 0f, 25f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.CanelAllBuilding();
        }
    }

    private void OnGUI()
    {
        this.windowRect = GUI.Window(0, this.windowRect, new GUI.WindowFunction(this.DoMyWindow), "DebugMenu");
    }

    private void DoMyWindow(int windowID)
    {
        GUILayout.Label("Active Agents: " + this.Agents.Count.ToString(), new GUILayoutOption[0]);
    }

    public void OnButtonClick(Button button)
    {
        if (button.name == "House")
        {
            this.house_ghost.SetActive(true);
        }
        if (button.name == "Mining")
        {
            Debug.Log("work motherfucker");
            this.PickResource = true;
            Cursor.SetCursor(this.cursorTexture, this.hotSpot, this.cursorMode);
        }
        if (button.name == "Exit")
        {
            this.villagec.GetComponent<villagecontroller>().turnJsOff();
            return;
        }
    }

    private void CanelAllBuilding()
    {
        this.house_ghost.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        this.PickResource = false;
    }
}
