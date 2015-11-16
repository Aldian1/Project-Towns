using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class villagecontroller : MonoBehaviour {

    public GameObject player;

    public GameObject camera_;

    public GameObject overview_camera;

    public GameObject tooltipF;

    public Transform campos;

    private MonoBehaviour jsanim;

    private int switcher = 0;

	// Use this for initialization
	void Start () {
        jsanim = player.GetComponent("Character_Animations") as MonoBehaviour;
    }
	
	// Update is called once per frame
	void Update () {
	
        if(tooltipF.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && switcher == 0)
            {
                switcher += 1;
                camera_.SetActive(false);
                overview_camera.SetActive(true);
                jsanim.enabled = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.F) && switcher == 1)
            {
                switcher = 0;
                overview_camera.SetActive(false);
                camera_.SetActive(true);
                jsanim.enabled = true;
                return;
            }

        }


	}
    void OnTriggerEnter(Collider other)
    {
        tooltipF.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {

        tooltipF.SetActive(false);
    }

}
