using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class InventoryController : MonoBehaviour {

    public Transform selecteditem, selectedSlot, OriginalSlot;

    public GameObject slotprefab, itemprefab;

    public Vector2 inventorysize = new Vector2(3,6);

    public float slotsize;

    public Vector2 windowSize;

    public GameObject[] slots;

    public List<Item> items = new List<Item>();

    public List<int> amountofitems = new List<int>();

    public bool candragitem;

    public int invamount;

    public GameObject tooltip;


    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        //tool tip description set
	if(selecteditem != null)
        {
            tooltip.GetComponent<Text>().text = selecteditem.GetComponent<Item>().description;
        }
        else
        {
            tooltip.GetComponent<Text>().text = "";

        }

        if(Input.GetMouseButton(0))
        {
            SetItemColliders(false);

            if (selecteditem == null)
            { }
            else
            {
                candragitem = true;
                OriginalSlot = selecteditem.parent;
                selecteditem.position = Input.mousePosition;
                selecteditem.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetItemColliders(true);
            if (selecteditem != null)
            {
                selecteditem.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            candragitem = false;

            if (selecteditem == null)
            { }
            else
            {
                    //if the slot target slot is null then we can place the item in its original slot i.e off screen drag
                    if (selectedSlot == null)
                     {
                    selecteditem.parent = OriginalSlot;
                    selecteditem.localPosition = Vector3.zero;
                     }
                   //if the target slot has no children (other items) then we place the item here (also checks if item stack is avaliable)
                    if (selectedSlot.childCount == 0)
                    {
                        selecteditem.parent = selectedSlot;
                        selecteditem.localPosition = Vector3.zero;
                    }
                    //else if the selected slot has a child and its not stackable then this if statement returns the item to original slot
                    else if(selectedSlot.childCount > 0)
                    {
                    if (selectedSlot.GetComponentInChildren<Item>().Name == selecteditem.GetComponent<Item>().Name)
                    {
                        //if the the selected item is the same name as the present item then we add to the stack unless the stack is already at its max!
                        //we get the item amount from both add it to the present item in slot and delete the dragged one

                        //small hack we move the parent first to create a condition to check against
                        selecteditem.parent = selectedSlot;

                        //get the selected item amount and check it against the items max stack in current slot
                        //string amounttext_ = selectedSlot.GetComponentInChildren<Text>().text.Substring(1, 4);


                        //converts string into int and then checks this against current amount in slot
                        string selectedslotdata = selectedSlot.GetComponentInChildren<Text>().text;
                        char[] chartobedelete = { 'x'};

                        string amounttext_ = selectedslotdata.TrimStart(chartobedelete);
                      
                        int currentamountinslot = Int32.Parse(amounttext_);

                        
                        if (selectedSlot.childCount > 1 && currentamountinslot < selectedSlot.GetComponentInChildren<Item>().maxstack)
                        {
                            selectedSlot.GetComponentInChildren<Item>().amount += selecteditem.GetComponent<Item>().amount;
                            Destroy(selecteditem.gameObject);
                        }
                        else
                        {
                            //we've got more than the stackable amount in their so we'll just return it to its slot
                            selecteditem.parent = OriginalSlot;
                            selecteditem.localPosition = Vector3.zero;

                        }
                    }
                    else
                    {
                        //return to original slot
                        selecteditem.parent = OriginalSlot;
                        selecteditem.localPosition = Vector3.zero;
                    }
                    }

                }

                selecteditem = null;
            }
        }

	
    //takes the items from a AddItem Call and takes the item + amount
    public void AddItem(Item item, int amount)
    {

       
        
        items.Add(item);
        amountofitems.Add(amount);
        GameObject ITEM = Instantiate(itemprefab) as GameObject;

        //setting item type,amount,name,stackable. passed from the create 
        ITEM.GetComponent<Item>().type = item.type;
        ITEM.GetComponent<Item>().sprite_ = item.sprite_;
        ITEM.GetComponent<Item>().amount = amount;
        ITEM.GetComponent<Item>().name = item.Name;
        ITEM.GetComponent<Item>().Name = item.Name;
        ITEM.GetComponent<Item>().stackable = item.stackable;
        ITEM.GetComponent<Item>().description = item.description;
        ITEM.GetComponent<Item>().maxstack = item.maxstack;


        //this for loop checks slots and places the item a created item in the empty slot, we also check if the name is the same and if it is we add it to that stack
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].transform.childCount >= 1 && slots[i].transform.GetChild(0).transform.name == ITEM.name)
            {
                if (slots[i].transform.GetChild(0).GetComponent<Item>().amount != slots[i].transform.GetChild(0).GetComponent<Item>().maxstack)
                {
                    slots[i].transform.GetChild(0).GetComponent<Item>().amount += ITEM.GetComponent<Item>().amount;
                    Destroy(ITEM);
                    return;
                }
            }

            if(slots[i].transform.childCount <= 0)
            {
                ITEM.transform.parent = slots[i].transform;
                ITEM.GetComponent<RectTransform>().anchoredPosition = new Vector3(-25,0,0);
                // 
                return;
                
            }else if(invamount >= 24)
            {
                Destroy(ITEM);
                Debug.Log("Inv FUll so weve deleted the object");
            }

        





        }
      
    }

    void SetItemColliders(bool b)
    {
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("Item"))
        {
            i.GetComponent<CanvasGroup>().blocksRaycasts = b;

        }

    }


    public void RemoveItem(string name,int cost)
    {
        //loop through slots
        for (int i = 0; i < slots.Length; i++)
        {

            //find a slot and if found then delte from amount the cost
            if (slots[i].transform.childCount >= 1 && slots[i].transform.GetChild(0).transform.name == name)
            {
                slots[i].transform.GetChild(0).GetComponent<Item>().amount -= cost;

            }

        }
        }


    //checks that when the player goes to process an item that we have that amount of item, this is called everytime an item is processed
    public void CheckCost(int cost, string resourcerequired, GameObject calledfrom)
    {
        //loop through slots
        for (int i = 0; i < slots.Length; i++)
        {

            //find a slot with the resource
            if (slots[i].transform.childCount >= 1 && slots[i].transform.GetChild(0).transform.name == resourcerequired)
            {

                //check the cost against the amount
                if(slots[i].transform.GetChild(0).GetComponent<Item>().amount >= cost)
                {
                    //if the amount > cost then we let them process
                    calledfrom.GetComponent<usablebuilding>().canProcess();

                }

                if (slots[i].transform.GetChild(0).GetComponent<Item>().amount <= cost)
                {

                    calledfrom.GetComponent<usablebuilding>().cantProcess();
                }

            }

        }

    }
}
