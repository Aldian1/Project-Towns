using System;
using UnityEngine;

public class villagecontroller : MonoBehaviour
{
    public GameObject player;

    public GameObject camera_;

    public GameObject overview_camera;

    public GameObject tooltipF;

    public Transform campos;

    public MonoBehaviour jsanim;

    public int switcher;

    public GameObject ResourceElemements;

    public GameObject villageoverlay;

    private void Start()
    {
        this.jsanim = (this.player.GetComponent("Character_Animations") as MonoBehaviour);
    }

    private void Update()
    {
        if (this.tooltipF.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) && this.switcher == 0)
            {
                Debug.Log("Turn the overlay on");
                this.villageoverlay.gameObject.SetActive(true);
                this.switcher++;
                this.camera_.SetActive(false);
                this.overview_camera.SetActive(true);
                this.jsanim.enabled = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.F) && this.switcher == 1)
            {
                this.switcher = 0;
                this.overview_camera.SetActive(false);
                this.camera_.SetActive(true);
                this.jsanim.enabled = true;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.tooltipF.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        this.tooltipF.SetActive(false);
    }

    public void turnJsOff()
    {
        this.jsanim.enabled = true;
        this.switcher = 0;
        this.overview_camera.SetActive(false);
        this.camera_.SetActive(true);
        this.villageoverlay.gameObject.SetActive(false);
        this.jsanim.enabled = true;
    }
}
