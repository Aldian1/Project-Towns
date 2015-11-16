using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class BasicUnitControl : MonoBehaviour {

    public List<GameObject> Inventory = new List<GameObject>();
    public List<Transform> StoredLocations = new List<Transform>();
	public Transform target;
    public GameObject log;

	Seeker seeker;

	public Path path;

	int currentWaypoint;

	public float speed;

	public float rotatespeed;
	CharacterController charactercontroller;

	float waypointdistance = 2F;

	public float nextWaypointDistance = 3;

	private Animator animator_;

    private bool newtargetactive;

    public enum targetType
    {
       Tree,
       Building


    };

    public targetType TargetType;

	public void Start () {

		animator_ = this.GetComponent<Animator>();
		//Get a reference to the Seeker component we added earlier
		Seeker seeker = GetComponent<Seeker>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,target.transform.position, OnPathComplete);

		charactercontroller = this.GetComponent<CharacterController>();
	}
	public void OnPathComplete (Path p) {
		Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);

		animator_.SetFloat("Walk", 1);

		if (!p.error) {
            //Reset the waypoint counter
            currentWaypoint = 0;
            path = p;

		}
	}
	public void Update () {
		if(target)
		{
			Vector3 targetDir = target.position - transform.position;
			float step = rotatespeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
			Debug.DrawRay(transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation(newDir);
		}

		if (path == null) {
			//We have no path to move after yet
			return;
		}

        if (newtargetactive == false)
        {
            if (currentWaypoint >= path.vectorPath.Count)
            {
                //Debug.Log ("End Of Path Reached");
                //reached the end of path
                animator_.SetFloat("Walk", 0);
                newTarget();
                return;
            }
        }
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.deltaTime;
		charactercontroller.SimpleMove (dir);
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}

    void newTarget()
    {
        newtargetactive = true;

        if (TargetType == targetType.Building)
        {
            GameObject spawn;
            spawn = target.gameObject;
            Instantiate(log, new Vector3(spawn.transform.position.x + 3, spawn.transform.position.y + 3,spawn.transform.position.z), spawn.transform.rotation);
            Inventory.Remove(Inventory[0]);
            target = null;
        }


        if (TargetType == targetType.Tree)
        {
            Destroy(target.gameObject);
            Inventory.Add(log);
            target = StoredLocations[0];
            TargetType = targetType.Building;
            AstarPath.active.Scan();
        }
        

        if (target)
        {
            Seeker seeker = GetComponent<Seeker>();
            Debug.Log("Done");

            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
            newtargetactive = false;
        }
    }
}
