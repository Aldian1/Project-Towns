using UnityEngine;
using System.Collections;

public class LizardMenController : MonoBehaviour {


    public GameObject highlights;

	// Use this for initialization
	void Start () {
   
    }

    // Update is called once per frame
    public void Lockedon()
    {
        

     
            highlights.SetActive(true);
           
     
    }
    public void NotLockedon()
    {
        highlights.SetActive(false);

    }

}