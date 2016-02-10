using UnityEngine;
using System.Collections;

public class _CameraController : MonoBehaviour {

    private bool haveToMove = false;
    private bool moveWithMouse = false;
    private GameObject targetPosition;
    private GameObject targetRotation;

    public GameObject testPosition;
    public GameObject testLookatPosition;

	public void MoveToLookout (GameObject targetObject, GameObject lookatObject)
    {
        GetComponent<CameraControls>().distance = Vector3.Distance(targetObject.transform.position, lookatObject.transform.position);
        GetComponent<CameraControls>().maxDistance = Vector3.Distance(targetObject.transform.position, lookatObject.transform.position);
        targetPosition = targetObject;
        targetRotation = lookatObject;
        haveToMove = true;
	}

    public void SetMouseControl(bool state)
    {
        GetComponent<CameraControls>().enabled = state;
    }

    void Update()
    {
        if (haveToMove)
        {
            transform.position = Vector3.Lerp(transform.position, testPosition.transform.position, 5 * Time.deltaTime);
            transform.LookAt(testLookatPosition.transform);

            float distance = Vector3.Distance(transform.position, testPosition.transform.position);
            if (distance < 0.05)
            {
                transform.position = testPosition.transform.position;
                haveToMove = false;
                SetMouseControl(true);
            }
        }
    }
}
