using System.Collections;
using UnityEngine;

public class runner : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int jumpForce = 300;
    public Animator my_animator;
    public Rigidbody body;
    
    public Collider my_collider;

    public const string running = "isRunning";
    public const string jumping = "isJumping";
    public const string sliding = "isSliding";

    public bool isGrounded = true;
    public bool isRunning = true;
    public bool isJumping = false;
    public bool isSliding = false;
    private bool isAlive = true;

    private float speed = 10f;
    public Camera cam;
    public Vector3 offset = new Vector3(0, 1, -2.5f);
    public Vector3 nrmlOffset = new Vector3(0, 1, -3f);
    public Vector3 slideOffset = new Vector3(0, 1.5f, -4f);
    
    public float moveSpeed = 10f; 
    private float orgSpeed = 10f;

    

    void Start()
    {
        my_animator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
       
        jumpForce = 32;
        speed = 50f;
        offset = nrmlOffset;
        cam.transform.position = gameObject.transform.position + nrmlOffset;

        
    }
   

    // Update is called once per frame
    void Update()
    {
        moveSpeed = orgSpeed;
        if (isAlive)
        {
            jump();
            run();
            slide();
            camControllor();
            setScore();
        }
       
        my_animator.SetBool(running, isRunning);
        my_animator.SetBool(jumping, isJumping);
        my_animator.SetBool(sliding, isSliding);

    }



    void run()
    {
        body.velocity = new Vector3(body.velocity.x, body.velocity.y, moveSpeed);
        Vector3 pos = body.position;
       
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(pos.x >= -5f && pos.x <= -3f)
            {
                
                StartCoroutine(move(new Vector3(0, pos.y, pos.z), speed));
            }
            else if(pos.x <= 1 && pos.x >= -1)
            {
                
                StartCoroutine(move(new Vector3(4, pos.y, pos.z), speed));
            }
           
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (pos.x <= 5f && pos.x >= 3f)
            {
                
                StartCoroutine(move(new Vector3(0, pos.y, pos.z), speed));
            }
            else if (pos.x <= 1 && pos.x >= -1)
            {
                
                StartCoroutine(move(new Vector3(-4f, pos.y, pos.z), speed));
            }
            
        }
       

    }
    IEnumerator move(Vector3 pos, float speed)
    {
        
        while(body.position.x != pos.x)
        {
            
            Vector3 vct = new Vector3(pos.x, body.position.y, body.position.z);
            body.position = Vector3.MoveTowards(body.position, vct, speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        
    }
 

    void jump()
    {
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))&& isGrounded)
        {
            moveSpeed = 0f;
            isJumping = true;
            isRunning = false;
            body.AddForce(new Vector3(-0.005f, 10, 0)*jumpForce, ForceMode.Force);
            isGrounded = false;
        }
       
        if (isGrounded)
        {
            isJumping = false;
            isRunning = true;
            
        }
        else
        {
           
            isRunning = false;
            isJumping = true;
            isSliding = false;
            
        }
       

        
        
    }
    void slide()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                /* Vector3 pos = gameObject.transform.position;
                 pos.z += 0.81f;
                 gameObject.transform.position = pos;*/
                body.AddForce(new Vector3(0, -10, 0) * jumpForce, ForceMode.Force);
                offset = slideOffset;
                isSliding = true;
                isRunning = false;
            }
            else
            { 
                isRunning = true;
                isSliding = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                body.AddForce(new Vector3(0, -10, 0) * jumpForce * 2, ForceMode.Force);

                offset = slideOffset;
                isSliding = true;
                isRunning = false;
            }
            else
            {
                isRunning = true;
                isSliding = false;
            }
        }
       
    }
    public void setScore()
    {
        float score = gameObject.transform.position.z;
        scoreControlor._score = (int)score;
        if(score > 0 && score%500 == 0 && orgSpeed<=15)
        {
            orgSpeed += 0.5f;
        }
    }
    
   
    public void returnPos()
    {
        offset = nrmlOffset;
        

    }

    public void camControllor()
    {
        Vector3 camPos = new Vector3(gameObject.transform.position.x, cam.transform.position.y, offset.z+gameObject.transform.position.z);
        cam.transform.position = camPos; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
        {
            isGrounded = true;
            /*Debug.Log(collision.gameObject.name);*/

        }
        if (collision.gameObject.tag.Equals("hd"))
        {
            orgSpeed = 0;
            Vector3 direction = gameObject.transform.position - collision.gameObject.transform.position;
            body.AddForce( (direction/direction.magnitude)*10f, ForceMode.Impulse);
            isAlive = false;
            scoreControlor.isAlive = false;
            isRunning = false;
            isJumping = false;
            isSliding = false;
            //body.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("hd");
            /*Debug.Log(collision.gameObject.name);*/
            Invoke("die", 1);
        }

        /*foreach(Transform child in collision.gameObject.transform)
        {
          
            if (child.tag.Equals("ground")) isGrounded = true;
            Debug.Log("collide");
        }*/
    }
    void die()
    {
        if (!isAlive)
        {
            Destroy(gameObject);
        }
    }
   
}
