using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

   public void MouseOver()
    {
      
        transform.parent.GetComponent<InventoryController>().selectedSlot = this.transform;
    }
}
