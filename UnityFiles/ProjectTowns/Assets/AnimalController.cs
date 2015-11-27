using UnityEngine;
using System.Collections;

 

public class AnimalController : MonoBehaviour {

    public enum state
    {
        randomMovement,
        straightmovement,
        attackandpatrol

    };

    public state State;

    public float speed;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(State == state.straightmovement)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
	}
}
