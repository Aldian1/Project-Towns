using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CharacterControls : MonoBehaviour {


    EventSystem eventsystem;

    public GameObject currentObject;
    RaycastHit hit;

    public GameObject UITooltip;

    public GameObject CraftingMenu;

    public GameObject[] LoadBarItems;
    public List<GameObject> Inventory = new List<GameObject>();

    private RectTransform mainbar;

    public float display;

    public GameObject stump;

    private int invswitcher;


    public GameObject invUIBase;

    public List<GameObject> invtiles = new List<GameObject>();
    private List<GameObject> invtileactiveset = new List<GameObject>();

    public GameObject woodtileinv;

    public GameObject tentinv;

    public Sprite woodtileinvhighlighted;

    public GameObject tooltip;

    public List<GameObject> craftableitems = new List<GameObject>();

    public List<GameObject> craftingoptions = new List<GameObject>();

    public List<int> itemamount = new List<int>();



    public GameObject tentghost;
    private bool ghostactive;

    public Transform ghosttransform;
    
    
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
       



        if (itemamount[0] <= 0)
        {
            woodtileinv.SetActive(false);


        }

        if(!currentObject)
        {

            LoadBarItems[0].SetActive(false);
            LoadBarItems[1].SetActive(false);
            LoadBarItems[2].SetActive(false);
          
            UITooltip.SetActive(false);

        }
        #region inv
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (invswitcher == 0)
            {
                
                InvokeRepeating("Inventory_", 0, 1);
                invswitcher = 1;
                return;
            }

            if (invswitcher == 1)
            {
                CancelInvoke("Inventory_");
                invUIBase.SetActive(false);
                CraftingMenu.SetActive(false);
                tentinv.SetActive(false);
                foreach(GameObject p in invtileactiveset)
                {

                    p.SetActive(false);

                }
                invswitcher = 0;
                return;
            }

        }
        #endregion
        if (LoadBarItems[1].GetComponent<Image>().fillAmount >= 1F)
        {

           // Debug.Log("firing");


            if(currentObject.tag == "Interactible")
            {
                if (currentObject.name == "Tree_Desert")
                {
                    Instantiate(stump, new Vector3(transform.position.x,transform.position.y + 5,transform.position.z), Quaternion.identity);
                    LoadBarItems[0].SetActive(false);
                    LoadBarItems[1].SetActive(false);
                    LoadBarItems[2].SetActive(false);
                    Destroy(currentObject.gameObject);
                  
                }
               
                LoadBarItems[1].GetComponent<Image>().fillAmount = 0;

            }

        }
      


        //max load bar length -521.495

        Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(this.transform.position, fwd * 10, Color.green);

        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {

            currentObject = hit.transform.parent.gameObject;

            if (currentObject.tag == "Pickup")
            {
              
                UITooltip.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Inventory.Add(stump);
                    Destroy(currentObject);
                }

            }
            else if(currentObject.tag == "Interactible")
            {
            }
            else
            {
                UITooltip.SetActive(false);
                LoadBarItems[0].SetActive(false);
                LoadBarItems[1].SetActive(false);
                LoadBarItems[2].SetActive(false);
            }


          

            if (Vector3.Distance(transform.position, currentObject.transform.position) < 5F)
            {



                if (currentObject.tag == "Interactible" || LoadBarItems[1].GetComponent<Image>().fillAmount == 0)
                {

                    UITooltip.SetActive(true);


                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        display = 0;
                        LoadBarItems[1].GetComponent<Image>().fillAmount = 0;
                        LoadBar();

                    }

                    if (Input.GetKey(KeyCode.F))
                    {

                        LoadBarItems[1].GetComponent<Image>().fillAmount += display;
                        LoadBarItems[0].SetActive(true);
                        LoadBarItems[1].SetActive(true);
                        LoadBarItems[2].SetActive(true);

                    }
                    else
                    {
                        LoadBarItems[0].SetActive(false);
                        LoadBarItems[1].SetActive(false);
                        LoadBarItems[2].SetActive(false);

                    }

                }
                else if (currentObject.tag == "Pickup")
                {




                }
                else
                {
                    LoadBarItems[0].SetActive(false);
                    LoadBarItems[1].SetActive(false);
                    LoadBarItems[2].SetActive(false);
                    UITooltip.SetActive(false);
                }

            }
           
        }

      
    }
    
    void LoadBar()
    {
        float min = 0;
        float max = 1;
        float smoothtime = 0.05F;
        

        display = Mathf.Lerp(min, max, smoothtime * Time.deltaTime);

       
        return;
    }

    void Inventory_()
    {

       
            invUIBase.SetActive(true);
             CraftingMenu.SetActive(true);


        if (Inventory.Contains(stump))
            {
                invtileactiveset.Add(woodtileinv);

            int i = 0;
                woodtileinv.transform.position = invtiles[i].transform.position;
                woodtileinv.transform.parent = invtiles[i].gameObject.transform;
                woodtileinv.SetActive(true);
                woodtileinv.GetComponentInChildren<Text>().text = itemamount[0].ToString();

            }


        #region recipes
        itemamount[0] = 0;

        foreach (GameObject item in Inventory)
        {

            //0 = log
            //1 = stone
            if(item.name == "Log")
            {
                itemamount[0]++;

            }
        }

        if(itemamount[0] > 3)
        {
            craftingoptions[0].SetActive(true);


        }
        else
        {
            craftingoptions[0].SetActive(false);

        }

        #endregion

        CancelInvoke("Inventory_");

       

    }


    public void ToolTip(Button button)
    {
       // tooltip.SetActive(true);
      //  float y = button.transform.position.y + 4;
      //  tooltip.transform.position = new Vector2(button.transform.position.x, y);

    }

    public void CreateInv(Button button)
    {

        string item = button.GetComponentInChildren<Text>().text;
        if(item == "Tent")
        {

            Debug.Log("poo");
            itemamount[0] -= 4;
            for(int i = 0; i < 4; i++)
            {
                Inventory.Remove(Inventory[i]);

            }


        }
        


    }
        
}
