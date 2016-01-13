#pragma strict
internal var animator:Animator;
var v:float;
var h:float;
var run:float;

var walkspeed:float;

var rotatespeed:float;


function Start () {
	animator=GetComponent (Animator);
}

function Update () {
    if(gameObject.tag == "Player")
    {

    
        if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
        {
            v = 0;
            h = 0;
            run = 0;
            animator.SetFloat("Walk",0);
            animator.SetFloat("Run",0);
            animator.SetFloat("Turn",0);
            return;
        }

        v=Input.GetAxis("Vertical");
        h=Input.GetAxis("Horizontal");

        if (animator.GetFloat("Run")==0.2){
            if (Input.GetKeyDown("space")){
                animator.SetBool("Jump",true);
            }
        }
        Sprinting();


	

        if (Input.GetKey(KeyCode.W)){
	
            transform.Translate(Vector3.forward * walkspeed * Time.deltaTime);
	
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Time.deltaTime * rotatespeed, 0);

        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,- Time.deltaTime * rotatespeed, 0);

        }

        if(Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("Chop",1);
            animator.SetBool("Chopping",true);

        }else{

            animator.SetFloat("Chop",0);
            animator.SetBool("Chopping",false);
 

        }
    }
}

function FixedUpdate (){
	animator.SetFloat("Walk",v);
	animator.SetFloat("Run",run);
	animator.SetFloat("Turn",h);
}

function Sprinting(){
	if (Input.GetKey(KeyCode.LeftShift)){
	    run=0.2;
	    walkspeed = 6.5;
	}
	else
	{
	    run=0.0;
	    walkspeed = 3.5;
	}
}