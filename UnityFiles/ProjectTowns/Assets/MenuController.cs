using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {


	public float splash_timer;

	public GameObject image;

	public RawImage rawimage;

	public GameObject panel;

	public bool reachedend;

	public GameObject camera_;

	public GameObject canvas;
	// Use this for initialization
	void Start () {

		rawimage = image.GetComponent<RawImage>();
		rawimage.CrossFadeAlpha(0.0F,0, false);
		rawimage.CrossFadeAlpha(1F,splash_timer, false);
		StartCoroutine("Timer");

	}
	
	// Update is called once per frame
	void Update () {

		if(reachedend == true)
		{
			rawimage.CrossFadeAlpha(0F,splash_timer, false);
			panel.GetComponent<Image>().CrossFadeAlpha(0F, splash_timer, false);
			camera_.SetActive(true);
			reachedend = false;


		}

	}

	IEnumerator Timer()
	{

		yield return new WaitForSeconds(splash_timer);
		reachedend = true;

	}
}
