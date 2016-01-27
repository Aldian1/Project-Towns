using Pathfinding;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class BasicUnitControl : MonoBehaviour
{
    public enum JobType
    {
        resource_collecting,
        trading,
        combat = 3,
        build
    }

    public Transform target;

    public Vector3 targetPosition;

    private Seeker seeker;

    private CharacterController controller;

    public Path path;

    public float speed = 100f;

    public float nextWaypointDistance = 3f;

    private int currentWaypoint;

    public bool buildhouse;

    public AstarPath astar;

    public Character_Animations anims;

    public GameObject storage;

    public string intentedresource;

    public GameObject AgentMother;

    private Vector3 velocity = Vector3.zero;

    public GameObject buildobject;

    public bool busy;

    public bool resourcebusy;

    public BasicUnitControl.JobType Type_;

    public GameObject currentresource;

    public void Start()
    {
      //  this.AgentMother.GetComponent<VillageStuff>().Agents.Add(base.gameObject);
    }

    public void StartPath(GameObject identifiedtarget)
    {
        if (!this.busy)
        {
            this.target = identifiedtarget.transform;
            if (this.buildhouse)
            {
                this.Type_ = BasicUnitControl.JobType.build;
                this.target = identifiedtarget.transform;
            }
            if (this.target.tag == "ResourcePoint" && Vector3.Distance(identifiedtarget.transform.position, base.transform.position) > 10f && (this.target.GetComponent<ResourceID>().resource_type == ResourceID.ResourceType.rock || this.target.GetComponent<ResourceID>().resource_type == ResourceID.ResourceType.wood))
            {
                this.Type_ = BasicUnitControl.JobType.resource_collecting;
                this.currentresource = identifiedtarget;
                this.intentedresource = this.target.tag;
            }
            this.targetPosition = this.target.transform.position;
            this.seeker = base.GetComponent<Seeker>();
            this.controller = base.GetComponent<CharacterController>();
            this.anims.v = 5f;
            this.seeker.StartPath(base.transform.position, this.targetPosition, new OnPathDelegate(this.OnPathComplete));
        }
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            this.path = p;
            this.currentWaypoint = 0;
        }
    }

    public void FixedUpdate()
    {
        if (this.target != null)
        {
            Vector3 worldPosition = new Vector3(this.target.position.x, base.transform.position.y, this.target.position.z);
            base.transform.LookAt(worldPosition);
            if (this.path == null)
            {
                return;
            }
            if ((float)this.currentWaypoint >= (float)this.path.vectorPath.Count - 1f && this.target != null)
            {
                UnityEngine.Debug.Log("End Of Path Reached");
                this.anims.v = 0f;
                if (this.Type_ == BasicUnitControl.JobType.resource_collecting)
                {
                    base.StartCoroutine("resourcetimer");
                    this.currentWaypoint = 0;
                    this.path = null;
                    return;
                }
                if (this.Type_ == BasicUnitControl.JobType.build)
                {
                    base.StartCoroutine("Build");
                    this.currentWaypoint = 0;
                    this.path = null;
                    return;
                }
                return;
            }
            else
            {
                Vector3 vector = (this.path.vectorPath[this.currentWaypoint] - base.transform.position).normalized;
                vector *= this.speed * Time.fixedDeltaTime;
                this.controller.Move(vector);
                if (Vector3.Distance(base.transform.position, this.path.vectorPath[this.currentWaypoint]) < this.nextWaypointDistance)
                {
                    this.currentWaypoint++;
                    return;
                }
            }
        }
    }

    public void Harvest()
    {
        if (Vector3.Distance(this.currentresource.transform.position, base.transform.position) <= 10f)
        {
            this.StartPath(this.storage);
            return;
        }
        if (Vector3.Distance(this.storage.transform.position, base.transform.position) < 5f)
        {
            if (this.currentresource.GetComponent<ResourceID>().resource_type == ResourceID.ResourceType.rock)
            {
                this.storage.transform.parent.GetComponent<StorageInv>().stone++;
            }
            if (this.currentresource.GetComponent<ResourceID>().resource_type == ResourceID.ResourceType.wood)
            {
                this.storage.transform.parent.GetComponent<StorageInv>().wood++;
            }
            UnityEngine.Debug.Log("We are travelling too the resource");
            this.StartPath(this.currentresource);
            return;
        }
    }

    public void BuildBuilding()
    {
        UnityEngine.Debug.Log("Build");
        this.busy = true;
        this.target.GetChild(0).transform.position = Vector3.SmoothDamp(this.target.GetChild(0).transform.position, this.target.transform.position, ref this.velocity, 0.3f);
        if (Vector3.Distance(this.target.GetChild(0).transform.position, this.target.transform.position) < 0.001f)
        {
            this.target.GetChild(0).transform.SetParent(null);
            UnityEngine.Object.Destroy(this.target.gameObject);
            this.busy = false;
            base.CancelInvoke("BuildBuilding");
            return;
        }
    }

 
    public IEnumerator resourcetimer()
    {
        yield return new WaitForSeconds(5f);
        Harvest();
    }

   
    public IEnumerator Build()
    {
        yield return new WaitForSeconds(10f);
        InvokeRepeating("BuildBuilding", 0, .3F);
    }
}
