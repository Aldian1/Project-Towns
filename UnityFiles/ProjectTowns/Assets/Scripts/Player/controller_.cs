using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class controller_ : MonoBehaviour {

    public GameObject canvas;

    public int switcher;

    public InventoryController inv;

    public bool enteredresource;

    public GameObject tooltip;

    public GameObject resource;

    public GameObject fishingtooltip;

    public int Itemdbparam, yieldparam;

    public float creationparam;

    public bool creationcost;

    public string ItemNameCost;

    public int cost;

    public bool canprocess;
    

    void Awake()
    {
        canvas.SetActive(true);

    }
	// Use this for initialization
	void Start () {
        canvas.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.I))
        {
        
            if(switcher == 0)
            {
                canvas.SetActive(true);
                switcher = 1;
                return;
            }

            if (switcher == 1)
            {
                canvas.SetActive(false);
                switcher = 0;
                return;
            }

        }

    if(enteredresource == true)
        {
            tooltip.SetActive(true);

        }else
        {
            tooltip.SetActive(false);
        }

    if(Input.GetKey(KeyCode.E) && enteredresource == true)
        {
            //passing exterior script params to function
            Resource(creationparam,Itemdbparam,yieldparam);

        }else if(Input.GetKeyUp(KeyCode.E) || enteredresource == false)
        {
            //this.GetComponentInChildren<Image>().fillAmount = 0;
        }
        
	}

    public void Resource(float creationtime, int itemDB_, int yield)
    {


            //setting time for fill amount
            this.GetComponentInChildren<Image>().fillAmount += creationtime * Time.deltaTime;

            //checking if item has a cost. if not just create specified item
            if (this.GetComponentInChildren<Image>().fillAmount == 1 && creationcost == false)
            {
                this.GetComponentInChildren<Image>().fillAmount = 0;
                inv.AddItem(ItemDB.ItemList[itemDB_], yield);

                //else if it does have a cost then we run the add item function and the remove item funcion
            }
            else if (this.GetComponentInChildren<Image>().fillAmount == 1 && creationcost == true && canprocess == true)
            {

            //we also communicate the process cost needs to be reprocessed as this isnt called every frame

            resource.GetComponent<usablebuilding>().updateprocess();
                this.GetComponentInChildren<Image>().fillAmount = 0;
                inv.AddItem(ItemDB.ItemList[itemDB_], yield);
                inv.RemoveItem(ItemNameCost, cost);
               


        }
        }

    }

