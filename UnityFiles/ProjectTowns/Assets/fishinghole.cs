using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class fishinghole : MonoBehaviour {

    public GameObject Player;

    public GameObject CanvasHolder;

    public GameObject Changingbar, objectivebar,img;

    public InventoryController inv;

    public GameObject itemdb;

    public int currentlevel;

    public int CurrentFishCaught;

    private float switcher;

    public MonoBehaviour jsanim;

    public bool reset;

    public bool gamerun;

    public Vector2 changesize;

    public float speed;

    public bool playerhere;

    public GameObject fishcaught;
	// Use this for initialization
	void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");
        jsanim = Player.GetComponent("Character_Animations") as MonoBehaviour;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R))
        {
            inv.AddItem(ItemDB.ItemList[6], 5);

        }
        if(gamerun)
        {
            FishingGame();
        }
        if(gamerun == false)
        {

            jsanim.enabled = true;
           
            CurrentFishCaught = 0;
            currentlevel = 50;
            CanvasHolder.SetActive(false);
            speed = 10;

        }

	if(Input.GetKeyDown(KeyCode.E) && switcher == 0 && playerhere)
        {

            gamerun = true;
            switcher = 1;
            jsanim.enabled = false;
            CanvasHolder.SetActive(true);
            return;

        }else if(Input.GetKeyDown(KeyCode.E) && switcher == 1)
        {
            gamerun = false;
            switcher = 0;
            jsanim.enabled = true;
            CanvasHolder.SetActive(false);
            CurrentFishCaught = 0;
            return;
        }
	}


    void OnTriggerEnter(Collider other)
    {
        Player.GetComponent<controller_>().fishingtooltip.SetActive(true);
        playerhere = true;

    }


    void OnTriggerExit(Collider other)
    {
        Player.GetComponent<controller_>().fishingtooltip.SetActive(false);
        playerhere = false;
    }


    public void FishingGame()
    {
        if(CurrentFishCaught == 5)
        {
          
            inv.AddItem(ItemDB.ItemList[6], CurrentFishCaught);
            gamerun = false;
            return;
        }
        CanvasHolder.GetComponentInChildren<Text>().text = "Fish Caught\n" + CurrentFishCaught.ToString();


        if(reset)
        {
           
            Changingbar.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
            objectivebar.GetComponent<RectTransform>().sizeDelta = new Vector2(Random.Range(3,currentlevel), 100);
            changesize.x = 100;
            reset = false;
        }
        if(!reset)
        {
            if (changesize.x <= 5)
            {
                reset = true;
                changesize.x = 100;
                Debug.Log("reset");
                return;
            }



            changesize.x -= speed * Time.deltaTime;
            Changingbar.GetComponent<RectTransform>().sizeDelta = new Vector2(changesize.x, 100);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (Changingbar.GetComponent<RectTransform>().sizeDelta.x <= objectivebar.GetComponent<RectTransform>().sizeDelta.x + 10)
                {
                    Debug.Log("we caught a fish adding to level");
                    currentlevel -= 10;
                    speed += Random.Range(5,10);
                    CurrentFishCaught += 1;
                    //add item
                    reset = true;
                    return;
                }
                else
                {
                    Debug.Log("We didnt catch a fish running reset");
                    reset = true;
                    return;
                }
            }

        }
    }
}
