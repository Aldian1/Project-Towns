using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _Collectible : MonoBehaviour {

    public enum Type { rock, tree, sometingElse }

    public GameObject playerObject;
    public GameObject GUI;
    public Type type;
    public InventoryController inventoryController;
    public string resourceName = "Resource name";
    public string description = "Resource description";
    public int unitsAvailable = 0;
    public int collectionTime = 1;
    public int yield = 1;

    [HideInInspector]
    public bool beingLookedAt = false;

    private GameObject canvas;
    private GameObject unitsUI;
    private GameObject collectingProgressIndicator;
    private bool tooltipShown = false;
    bool inputDisables = false;
    private int itemDBIndex = 0;
   
	// Use this for initialization
	void Awake ()
    {
        switch (type)
        {
            case Type.rock:
                itemDBIndex = 3;
                break;
            case Type.tree:
                itemDBIndex = 4;
                break;
        }

        canvas = GUI.transform.Find("CollectibleInfo").gameObject;
        unitsUI = canvas.transform.Find("Units").gameObject;
        canvas.transform.Find("ResourceName").GetComponent<Text>().text = resourceName;
        canvas.transform.Find("Description").GetComponent<Text>().text = description;

        collectingProgressIndicator = playerObject.transform.Find("CollectingProgress").Find("Image").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (beingLookedAt)
        {
            if (!tooltipShown)
            {
                tooltipShown = true;
                canvas.SetActive(true);
            }
            unitsUI.GetComponent<Text>().text = "Units left: " + unitsAvailable.ToString();

            //Gather resource
            if (Input.GetKey(KeyCode.E) && unitsAvailable > 0)
            {
                if (!inputDisables)
                {
                    inputDisables = true;
                    playerObject.GetComponent<Character_Animations>().enabled = false;
                    playerObject.GetComponent<_CharacterController>().allowInput = false;
                }

                collectingProgressIndicator.GetComponent<Image>().fillAmount += collectionTime * Time.deltaTime;
                float progress = collectingProgressIndicator.GetComponent<Image>().fillAmount;
                
                if (progress >= 1)
                {
                    collectingProgressIndicator.GetComponent<Image>().fillAmount = 0;
                    inventoryController.AddItem(ItemDB.ItemList[itemDBIndex], yield);
                    unitsAvailable--;

                    if (unitsAvailable == 0)
                    {
                        canvas.SetActive(false);
                        playerObject.transform.GetComponent<_CharacterController>().RemoveTrigger(this.gameObject);
                        playerObject.GetComponent<Character_Animations>().enabled = true;
                        playerObject.GetComponent<_CharacterController>().allowInput = true;
                        Destroy(this.gameObject);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                    inputDisables = false;
                    playerObject.GetComponent<Character_Animations>().enabled = true;
                    playerObject.GetComponent<_CharacterController>().allowInput = true;

                    collectingProgressIndicator.GetComponent<Image>().fillAmount = 0;
            }
        }
        else
        {
            if (tooltipShown)
            {
                tooltipShown = false;
                canvas.SetActive(false);
            }
        }
	}
}
