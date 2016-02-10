using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    //name of obj
    public string name;
    //type of obj
    public enum Type { Equip, Consumable, Misc, Resource };
    //stackable or not
    public enum Stackable { Stackable, Non_Stackable};
    //amount
    public int amount;

    

    public Type type;
    public Stackable stackable;

    //sprite to use
    public Sprite sprite_;

    public Text amounttext;
    // Use this for initialization

    //description for tooltip
    public string description;

    //max stack to get rid of problems such as insane stack numbers
    public int maxstack;

	void Start () {

        //overrides sprite
        this.GetComponent<Image>().overrideSprite = sprite_;
        //gets the text object
        amounttext = this.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (amount > 0)
        {
            amounttext.text = "x" + amount.ToString();
        }

    if(amount < 0)
        {
            Destroy(this.gameObject);

        }
      /*  if (EventSystem.current.IsPointerOverGameObject())
        {
          
                MouseOver();
            
        }*/
	}

    public void MouseOver()
    {
        
        transform.parent.parent.GetComponent<InventoryController>().selecteditem = this.transform;
        
    }

    public void MouseOverExit()
    {
        if (!transform.parent.parent.GetComponent<InventoryController>().candragitem)
        {
            transform.parent.parent.GetComponent<InventoryController>().selecteditem = null;
        }
    }

}
