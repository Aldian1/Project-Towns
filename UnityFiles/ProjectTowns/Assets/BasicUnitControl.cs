﻿using UnityEngine;
using System.Collections;
using Pathfinding;

public class BasicUnitControl : MonoBehaviour
{

    public Transform target;
    public Vector3 targetPosition;

    private Seeker seeker;
    private CharacterController controller;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 100;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    public GameObject[] VillageObjects;

    public VillageBuilder builder;

    public AstarPath astar;

    public Character_Animations anims;

    public void Start()
    {


    }
    public void StartPath()
    {
        targetPosition = target.transform.position;
        //Get a reference to the Seeker component we added earlier
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();

        anims.v = 5;
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

    public void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartPath();
            return;
        }
        if (target != null)
        {

            if (path == null)
            {
                //We have no path to move after yet
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count - 1F && target != null)
            {
                Debug.Log("End Of Path Reached");
                anims.v = 0;
                if (target.tag == "Tent")
                {
                    Instantiate(VillageObjects[0], target.transform.position, target.transform.rotation);
                    builder.needbuilding.Remove(builder.needbuilding[0]);
                    astar.Scan();
                    Destroy(target.gameObject);
                }
                target = null;
                return;
            }

            //Direction to the next waypoint
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;
            controller.Move(dir);

            //Check if we are close enough to the next waypoint
            //If we are, proceed to follow the next waypoint
            if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }
    }

}