using System;
using UnityEngine;
using UnityEngine.UI;

public class StorageInv : MonoBehaviour
{
    public GameObject storagedisplay;

    public int stone;

    public int wood;

    public GameObject maincanvas;

    public GameObject StoneId;

    public GameObject WoodID;

    public GameObject villageoverlaystone;

    public GameObject villageoverlaywood;

    private void Start()
    {
        this.maincanvas.SetActive(false);
    }

    private void Update()
    {
        this.villageoverlaystone.GetComponent<Text>().text = "Stone: " + this.stone.ToString();
        this.villageoverlaywood.GetComponent<Text>().text = "Wood: " + this.wood.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.StoneId.GetComponent<Text>().text = this.stone.ToString();
        this.WoodID.GetComponent<Text>().text = this.wood.ToString();
        this.maincanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        this.maincanvas.SetActive(false);
    }
}
