using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VillageBuilder : MonoBehaviour {

    public List<GameObject> needbuilding = new List<GameObject>();

    public string[] VillageItemNames;

    public GameObject CurrentSelectedItem;

    public bool placebuilding;

    public Material build, nobuild;

    public GameObject buildarea;

    public bool ghost;

    public GameObject ghost_;
    public GameObject[] ghosts;

    public GameObject currentbuilding;
    public GameObject[] buildings;

    public Camera myCam;

    public InventoryController inv;

    public List<GameObject> Villagers = new List<GameObject>();


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if(ghost)
        {
            GhostPlacement(ghost_);
            buildarea.SetActive(true);

        }else if(ghost == false && ghost_ != null)
        {
            ghost_.SetActive(false);
            buildarea.SetActive(false);

        }

       
	}

   void OnGUI()
    {
        if(GUILayout.Button("Tent", GUILayout.Width(100), GUILayout.Height(100)))
        {
            ghost = true;
            ghost_ = ghosts[0];
            currentbuilding = buildings[0];
        }

    }
    void GhostPlacement(GameObject objmanip)
    {

        objmanip.SetActive(true);
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.DrawLine(ray.origin, hit.point);
           // Debug.Log(hit.transform.name);
            objmanip.transform.position = new Vector3(hit.point.x, 1, hit.point.z);

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject obj = Instantiate(ghost_, hit.point, objmanip.transform.rotation) as GameObject;
                needbuilding.Add(obj);
                Villagers[0].GetComponent<BasicUnitControl>().target = needbuilding[0].transform;
                Villagers[0].GetComponent<BasicUnitControl>().StartPath();
                inv.RemoveItem("4", 4);
                ghost = false;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                objmanip.transform.Rotate(0, 15, 0);

            }
           
        }

       
    }
}
