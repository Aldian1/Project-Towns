using UnityEngine;
using System.Collections;

public class _ScriptManager : MonoBehaviour {

    public enum ObjectType { Campfire, Storage, Collectible }
    public ObjectType type;
    public GameObject playerObject;

    //dont set these by hand!
    public bool insideTrigger = false;
    public bool beingLookedAt = false;

    void Update()
    {
        //Debug.Log("insideTrigger: " + insideTrigger);
        //Debug.Log("beingLookedAt: " + beingLookedAt);
        if (insideTrigger)
        {
            if (beingLookedAt)
            {
                //Debug.Log("Something is lookint at " + this.transform.name);
                switch (type)
                {
                    case ObjectType.Campfire:
                        this.gameObject.GetComponent<_VillageContoller>().beingLookedAt = true;
                        break;
                    case ObjectType.Storage:
                        this.gameObject.GetComponent<_StorageController>().beingLookedAt = true;
                        break;
                    case ObjectType.Collectible:
                        this.gameObject.GetComponent<_Collectible>().beingLookedAt = true;
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case ObjectType.Campfire:
                        this.gameObject.GetComponent<_VillageContoller>().beingLookedAt = false;
                        break;
                    case ObjectType.Storage:
                        this.gameObject.GetComponent<_StorageController>().beingLookedAt = false;
                        break;
                    case ObjectType.Collectible:
                        this.gameObject.GetComponent<_Collectible>().beingLookedAt = false;
                        break;
                }
            }

            beingLookedAt = false;
        }        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == playerObject)
        {
            //Debug.Log("PlayerObject entered my trigger");
            insideTrigger = true;
            playerObject.transform.GetComponent<_CharacterController>().ReceiveTrigger(this.gameObject);
            switch (type)
            {
                case ObjectType.Campfire:
                    this.gameObject.GetComponent<_VillageContoller>().enabled = true;
                    break;
                case ObjectType.Storage:
                    this.gameObject.GetComponent<_StorageController>().enabled = true;
                    break;
                case ObjectType.Collectible:
                    this.gameObject.GetComponent<_Collectible>().enabled = true;
                    break;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == playerObject)
        {
            //Debug.Log("PlayerObject left my trigger");
            insideTrigger = false;
            playerObject.transform.GetComponent<_CharacterController>().RemoveTrigger(this.gameObject);
            switch (type)
            {
                case ObjectType.Campfire:
                    this.gameObject.GetComponent<_VillageContoller>().enabled = false;
                    break;
                case ObjectType.Storage:
                    this.gameObject.GetComponent<_StorageController>().enabled = false;
                    break;
                case ObjectType.Collectible:
                    this.gameObject.GetComponent<_Collectible>().enabled = false;
                    break;
            }
        }
    }
}
