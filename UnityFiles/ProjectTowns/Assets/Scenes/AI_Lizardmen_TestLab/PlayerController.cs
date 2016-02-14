// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region PlayerControlVariables
    private Animator animator;

   public float v;
   public float h;
   public float run;

    public float walkspeed;

    public float SideStepSpeed;

    public float thrust;
    public Rigidbody rb;

    public GameObject maincamera;
    public SmoothFollow smoothcamera;

    #endregion

    #region TrajectoryControls

    // Reference to the LineRenderer we will use to display the simulated path
    public LineRenderer sightLine;

    // Reference to a Component that holds information about fire strength, location of cannon, etc.
    public float firestrength = 500;
    public Color nextColor = Color.red;

    // Number of segments to calculate - more gives a smoother line
    public int segmentCount = 20;

    // Length scale for each segment
    public float segmentScale = 1;

    // gameobject we're actually pointing at (may be useful for highlighting a target, etc.)
    private Collider _hitObject;
    public Collider hitObject { get { return _hitObject; } }

    public Transform linerend;

    #endregion

    #region combatvariables
    public bool incombat;
    public GameObject currenttarget, oldtarget;
    public float backthrust;

    //holding all the ai that are within our target range
    public List<Collider> lizardmen = new List<Collider>();
    //Acts as a memory holder for what ai in the list we are on
    
    #endregion

    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        smoothcamera = maincamera.GetComponent<SmoothFollow>();
   
    }

    void Update()
    {
        //Controls for player and animation setting
        PlayerControls();
        Combat();
        //Calculates the arc for bow and arrow + other throwables
    

    }


    void PlayerControls()
    {
        if (gameObject.tag == "Player")
        {


            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
            {
                v = 0;
                h = 0;
                run = 0;
                animator.SetFloat("Walk", 0);
                animator.SetFloat("Run", 0);
                animator.SetFloat("Turn", 0);
                return;
            }

            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");

            if (animator.GetFloat("Run") == 0.2f)
            {
                if (Input.GetKeyDown("space"))
                {
                    animator.SetBool("Jump", true);
                    rb.AddForce(transform.up * thrust);
                }
            }
            Sprinting();




            if (Input.GetKey(KeyCode.W))
            {

                transform.Translate(Vector3.forward * walkspeed * Time.deltaTime);

            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                //transform.Rotate(0, Time.deltaTime * rotatespeed, 0);
                //transform.Translate(Vector3.right * SideStepSpeed * Time.deltaTime);
                if (rb.velocity.magnitude > 1)
               {

                }
              else
                {
                    rb.AddForce(transform.right * SideStepSpeed);
                }

            }

            if (Input.GetKeyDown(KeyCode.A))
            {
               if(rb.velocity.magnitude > 1)
                {

                }
                else
               {
                   rb.AddForce(-transform.right * SideStepSpeed);
                }
                //  transform.Translate(Vector3.left * SideStepSpeed * Time.deltaTime);
              
            }

            if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.W))
            {
                animator.SetFloat("Chop", 1);
                animator.SetBool("Chopping", true);

            }
            else
            {

                animator.SetFloat("Chop", 0);
                animator.SetBool("Chopping", false);


            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                if (rb.velocity.magnitude > 1)
                {

                }
                else
                {
                    rb.AddForce(-transform.forward * backthrust);
                }

            }
        }
    }

    void Combat()
    {
        if(incombat)
        {
          //  smoothcamera.rotationDamping = 0;
            transform.LookAt(currenttarget.transform);
        }
        else
        {
            //smoothcamera.rotationDamping = 3.5F;

        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //handles highlights target switching and entering combat
            Target();

        }

    }

    void FixedUpdate()
    {
        animator.SetFloat("Walk", v);
        animator.SetFloat("Run", run);
        animator.SetFloat("Turn", h);
       // simulatePath();
    }

    void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = 0.2f;
            walkspeed = 6.5f;
        }
        else
        {
            run = 0.0f;
            walkspeed = 3.5f;
        }
    }

    void Target()
    {
   
        incombat = true;
        

        //if our current taget is null or not their then we grab the first lizard men in the list and make them the default target
        if(currenttarget == null)
        {
            currenttarget = lizardmen[0].gameObject;
            oldtarget = currenttarget;
        }
        //else we go through the list and grab the next target making sure we dont grab the old one
        else
        {
            foreach(Collider lizardmentarget in lizardmen)
            {
                if(lizardmentarget == currenttarget.GetComponent<Collider>() || lizardmentarget == oldtarget.GetComponent<Collider>())
                {
                    
                }
                else
                {
                   
                    
                    oldtarget = currenttarget;
                    oldtarget.GetComponent<LizardMenController>().NotLockedon();
                    currenttarget = lizardmentarget.gameObject;
                    currenttarget.GetComponent<LizardMenController>().Lockedon();
                
                    return;
                }

            }
        }

        

    }
    
    //Trajectory shizzle
  /* void simulatePath()
    {
        Vector3[] segments = new Vector3[segmentCount];

        // The first line point is where the linerendere object is
        segments[0] = linerend.transform.position;

        // The initial velocity
        Vector3 segVelocity = linerend.transform.forward * firestrength * Time.deltaTime;

        
        _hitObject = null;

        for (int i = 1; i < segmentCount; i++)
        {
            // Time it takes to traverse one segment of length segScale
            float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics.gravity * segTime;

            // Check to see if we're going to hit a physics object
            RaycastHit hit;
            if (Physics.Raycast(segments[i - 1], segVelocity, out hit, segmentScale))
            {
                // remember who we hit
                _hitObject = hit.collider;

                // set next position to the position where we hit the physics object
                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                // correct ending velocity, since we didn't actually travel an entire segment
                segVelocity = segVelocity - Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
            }
            else
            {
                segments[i] = segments[i - 1] + segVelocity * segTime;
            }
        }

        // At the end, apply simulations to the LineRenderer

        // apply to line renderer
        sightLine.SetVertexCount(segmentCount);
        for (int i = 0; i < segmentCount; i++)
            sightLine.SetPosition(i, segments[i]);
    }
    */

    //checks if we already have it as a target
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Collision");
        if(!lizardmen.Contains(other) && other.tag == "Lizard")
        {
            lizardmen.Add(other);

        }
    }
    //checks if we need to remove from list
    void OnTriggerExit(Collider other)
    {
        if(lizardmen.Contains(other) && other.tag == "Lizard")
        {
            lizardmen.Remove(other);
            incombat = false;

        }

    }
}