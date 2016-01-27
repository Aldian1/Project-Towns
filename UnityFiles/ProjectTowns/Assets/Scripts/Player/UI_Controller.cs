using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Controller : MonoBehaviour {


    public GameObject StaminaBar;

    public MonoBehaviour AnimationScript;

	// Use this for initialization
	void Start () {
        StaminaBar.GetComponent<Image>().fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
