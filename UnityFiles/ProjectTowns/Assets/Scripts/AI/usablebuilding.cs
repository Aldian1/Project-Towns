using UnityEngine;
using System.Collections;

public class usablebuilding : MonoBehaviour {

    public int itemcreated;

    public int yield;
    public float creationtime;

    public string ResourceRequired;
    public int cost;

    public GameObject player;

    public GameObject inv;


    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
   public void OnTriggerEnter(Collider other)
    {
        player.GetComponent<controller_>().resource = this.gameObject;
        inv.GetComponent<InventoryController>().CheckCost(cost, ResourceRequired, this.gameObject);

    }
   public void OnTriggerExit(Collider other)
    {
        player.GetComponent<controller_>().enteredresource = false;

        player.GetComponent<controller_>().Itemdbparam = 0;
        player.GetComponent<controller_>().yieldparam = 0;
        player.GetComponent<controller_>().creationparam = 0;
        player.GetComponent<controller_>().creationcost = false;
        player.GetComponent<controller_>().ItemNameCost = "";
        player.GetComponent<controller_>().cost = 0;
        player.GetComponent<controller_>().resource = null;
    }

    public void updateprocess()
    {
        inv.GetComponent<InventoryController>().CheckCost(cost, ResourceRequired, this.gameObject);

    }

    public void canProcess()
    {
        player.GetComponent<controller_>().enteredresource = true;
        player.GetComponent<controller_>().canprocess = true;

        player.GetComponent<controller_>().Itemdbparam = itemcreated;
        player.GetComponent<controller_>().yieldparam = yield;
        player.GetComponent<controller_>().creationparam = creationtime;
        player.GetComponent<controller_>().creationcost = true;
        player.GetComponent<controller_>().ItemNameCost = ResourceRequired;
        player.GetComponent<controller_>().cost = cost;

    }

    public void cantProcess()
    {
        player.GetComponent<controller_>().canprocess = false;

    }
}
