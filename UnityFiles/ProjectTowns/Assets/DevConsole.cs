using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DevConsole : MonoBehaviour {

    public string[] tpnames;

    public Transform[] tplocations;

    public int switcher;

    public Transform tptarget;

    public GameObject maincanvas, inputfield, mainconsole;

    public InventoryController inv;
    // Use this for initialization
    void Start () {

        
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(","))
        {
            if (switcher == 0)

            {
                Debug.Log("Open COnsole");
                maincanvas.SetActive(false);
                switcher = 1;
                Time.timeScale = 1;
                return;
            }

            if (switcher == 1)
            {
                Debug.Log("Close Console");
                maincanvas.SetActive(true);
                switcher = 0;
                Time.timeScale = 0;
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return) && switcher == 0)
        {

   
            string tobesplit = inputfield.GetComponent<Text>().text;

            string[] split = tobesplit.Split(null);
            RunCommand(split[0], split[1]);

            mainconsole.GetComponent<Text>().text += "\n" + tobesplit;
            inputfield.GetComponent<Text>().text = "";
            


        }
	}

    void RunCommand(string command,string item)
    {
        #region hannahEasterEgg

        if(command == "Hannah")
        {
            Debug.Log(item);
            inv.AddItem(ItemDB.ItemList[0], 10);
            inv.AddItem(ItemDB.ItemList[1], 10);
            inv.AddItem(ItemDB.ItemList[2], 10);
            inv.AddItem(ItemDB.ItemList[3], 10);
          

            return;
        }
        #endregion

        #region tpcommand

        if (command == "tp")
        {
            

            foreach (string name in tpnames)
            {
                if (name == item)
                {
                    foreach (Transform target in tplocations)
                    {
                        if (target.name == name)
                        {
                            tptarget = target;
                            transform.position = new Vector3(tptarget.position.x, tptarget.position.y, tptarget.position.z);

                        }
                }
                }
            }
            return;
        }

        #endregion


        #region ItemCommand
        if(command == "give")
        {
            Debug.Log(item);
            int parsed = int.Parse(item);
            Debug.Log(parsed);
           inv.AddItem(ItemDB.ItemList[parsed], 10);
            return;
        }


        #endregion

        mainconsole.GetComponent<Text>().text += "\nCommand Not Recognised";

    }
}
