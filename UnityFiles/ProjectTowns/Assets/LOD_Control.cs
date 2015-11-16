using UnityEngine;
using System.Collections;

public class LOD_Control : MonoBehaviour {


	public float[] DistanceRanges;
	public GameObject[] lodmodels; //highest poly to lowest etc 0 = high

	private int current = -2;

	// Use this for initialization
	void Start () {
	
		for(int i = 0; i < lodmodels.Length; i++)
		{
			lodmodels[i].SetActiveRecursively(false);

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
