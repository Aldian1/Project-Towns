using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {


	public float speed;

	private GameObject camera_;

	// Use this for initialization
	void Start () {
		camera_ = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
		this.transform.rotation = camera_.transform.rotation;

		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(transform.forward * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A))
		{

		}
		if(Input.GetKey(KeyCode.D))
		{

		}
		if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * speed * Time.deltaTime);
		}
	}
}
