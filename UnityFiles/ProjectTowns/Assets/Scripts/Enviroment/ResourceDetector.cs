using UnityEngine;
using System.Collections;

public class ResourceDetector : MonoBehaviour {

    public GameObject player;
    public int YieldPerMine;

    public float miningTime;
    public int ItemDBDefinition;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        player.GetComponent<controller_>().enteredresource = true;

        player.GetComponent<controller_>().Itemdbparam = ItemDBDefinition;
        player.GetComponent<controller_>().yieldparam = YieldPerMine;
        player.GetComponent<controller_>().creationparam = miningTime;
        player.GetComponent<controller_>().Itemdbparam = ItemDBDefinition;
    }

    void OnTriggerExit(Collider other)
    {
        player.GetComponent<controller_>().enteredresource = false;
      
        player.GetComponent<controller_>().yieldparam = 0;
        player.GetComponent<controller_>().creationparam = 0;
        player.GetComponent<controller_>().Itemdbparam = 0;
    }
    public void additem()
    {



    }

}
