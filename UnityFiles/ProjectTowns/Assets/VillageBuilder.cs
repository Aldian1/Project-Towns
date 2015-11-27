using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VillageBuilder : MonoBehaviour {

    public GameObject[] villageitems;

    public string[] VillageItemNames;

    public GameObject CurrentSelectedItem;

    public bool placebuilding;

    public Material build, nobuild;

    public GameObject buildarea;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(placebuilding == true)
        {
            buildarea.SetActive(true);
           

        }
        else
        {
            buildarea.SetActive(false);
        }
	}

   void OnGUI()
    {
        if(GUILayout.Button("Tent", GUILayout.Width(100), GUILayout.Height(100)))
        {

        }

    }
    
}
