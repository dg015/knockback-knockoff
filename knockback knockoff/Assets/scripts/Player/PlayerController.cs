using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //basic movement
    [SerializeField] public Vector3 location;
    public Rigidbody2D rb;

    //New movement system
    [SerializeField] public Vector2 PVelocity;
    [SerializeField] private bool MaxVelocityReached;

    [Header("Horizontal")]
    
    [SerializeField] private float accelerationTime;
    private float accelerationRate;
    [SerializeField] private float decelerationTime;
    private float decelerationRate;
    [SerializeField] private float maxSpeed;

     private InputActionReference move;
    [SerializeField] private Vector2 playerInput;

    [Header("Veritcal")]
    [SerializeField] private float apexHeight;
    [SerializeField] private float apexTime;
    [SerializeField] private float GravityStrenght;
    private float gravity;
    public float intialJumpSpeed;

    //to rotate the player in the correct direction
    [SerializeField] private Transform head;

    //health system
    public bool alive = true;

    //Jump
    [Header("IsGrounded")]
    //Is grounded
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask slope;


    [Header("Multiplayer")]
    //multiplayer
    public static int PlayerCount;
    [SerializeField] private SpeedChecker speedChecker;
    [SerializeField] public int score;
    [SerializeField] private MultiplayerScoreManager scoreManager;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("new Input system")]
    [SerializeField] private PlayerInput playerInputComponent;
    [SerializeField] private PlayerInputActions inputActions;

    protected void Awake()
    {
        PlayerCount++;

    }
    private void Start()
    {
        score = 0;
        speedChecker = gameObject.GetComponent<SpeedChecker>();

        //Aceleration formula
        accelerationRate = maxSpeed / accelerationTime;
        decelerationRate = maxSpeed / decelerationTime;

        // jumping formulas
        gravity = -GravityStrenght * apexHeight / (apexTime * apexHeight);
        intialJumpSpeed = 2 * apexHeight / apexTime;

        //getanimator
        animator = transform.GetComponentInChildren<Animator>();

        playerInputComponent = GetComponent<PlayerInput>();

        scoreManager.addPlayerToScoreBoard(this,score);

    }

    private void OnEnable()
    {
       //inputActions = new PlayerInputActions();
       //inputActions.Player.Enable();
       //inputActions.Player.Move.performed += checkHorizontalInput;
       //inputActions.Player.Jump.performed += jump;
       //inputActions.Player.Move.canceled += resetInput;
    }

    private void OnDisable()
    {
       
        //inputActions.Player.Move.performed -= checkHorizontalInput;
        //inputActions.Player.Jump.performed -= jump;
        //inputActions.Player.Move.canceled -= resetInput;
        //inputActions.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckMaxVelocity();
        isGrounded();
        location = gameObject.transform.position;

        updateScoreTest();
    }

    private void FixedUpdate()
    {
        

        
        //set head rotaion
        scaleBodyToHeadRotation(head, transform);

        //read inputs
        


        //apply forces
        Movement(playerInput);

        VerticalForces();



        rb.linearVelocity = PVelocity;

        //if 
        hasHitWall();

    }

    private void hasHitWall()
    {
        //check with boxcast if the player has hit the ceiling, if yes then reset speed and let them fall down
        if(Physics2D.BoxCast(transform.position, boxSize,0,transform.up,castDistance,ground))
        {
            //Debug.Log("has hit ceiling");
            PVelocity.y = 0;
        }
        // now check if the have hit a wall if so then reset their fall speed
            //this one checks for the right side
        else if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, castDistance, ground) && !isGrounded())
        {
            //Debug.Log("has hit right wall");
            PVelocity.x = 0;
        }
        else if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.right, castDistance, ground) && !isGrounded())
        {
            //Debug.Log("has hit left wall");
            PVelocity.x = 0;
        }

    }


    private void updateScoreTest()
    {
        if (Input.GetKey(KeyCode.V))
        {
            scoreManager.updateScore(this, score);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {

            if(speedChecker.KillSpeed == true)
            {
                scoreManager.updateScore(this,score);
                scoreManager.endGame(score);
                Destroy(collision.gameObject);
                score++;
                Debug.Log("killed player");
            }
            
        }
    }


    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance,ground))
        {
            animator.SetBool("InAir", false);
            return true;
        }
        else 
        {
            animator.SetBool("InAir", true);
            return false;
        }
    }

    private void scaleBodyToHeadRotation(Transform head, Transform body )
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mouseWorld - head.position;

        if (dir.x <0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }



    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }


    private void resetInput(InputAction.CallbackContext context)
    {
        playerInput = new Vector2(0, PVelocity.y);
    }
    public void checkHorizontalInput(InputAction.CallbackContext context)
    {
        //Debug.Log("reading");
        playerInput = context.ReadValue<Vector2>();
        
    }


    /// <param name="playerInput"></param>
    public void Movement(Vector2 playerInput)
    {

        if(playerInput.x != 0)
        {

            //apply velocity
            if (!MaxVelocityReached) 
            {
                PVelocity.x += accelerationRate * playerInput.x * Time.deltaTime;

            }
            else if (MaxVelocityReached && PVelocity.x > 0 && playerInput.x < 0) //changing to right while in max speed
            {
                PVelocity.x += accelerationRate * playerInput.x * Time.deltaTime;
            }
            else if (MaxVelocityReached && PVelocity.x < 0 && playerInput.x > 0) //changing to left while in max speed
            {
                PVelocity.x += accelerationRate * playerInput.x * Time.deltaTime;
            }
            if(isGrounded())
            {
                animator.SetBool("IsWalking", true);
            }
        }
        else // here for decealartion 
        { 
            animator.SetBool("IsWalking", false);
            //deceleration
            //decelrationRate is applied every second eats away the remaining velocity
            if (PVelocity.x > 0)
            {
                
                PVelocity.x -= decelerationRate * Time.deltaTime;
                //Setting max will help by not allowing the speed to go over/lower the asked amount will make it be moving slightly forever
                PVelocity.x = Mathf.Max(PVelocity.x, 0);

            }
            else if (PVelocity.x < 0)
            {
                
                PVelocity.x += decelerationRate * Time.deltaTime;
                PVelocity.x = Mathf.Min(PVelocity.x, 0);

            }
        }
    }

    private void CheckMaxVelocity()
    {
        if(PVelocity.x >= maxSpeed || PVelocity.x <= -maxSpeed)
        {
            MaxVelocityReached = true;
        }
        else
        {
            MaxVelocityReached = false;
        }
    }

    private void VerticalForces()
    {
        if(isGrounded() && PVelocity.y <0 )
        {
            PVelocity.y = 0;
            
        }
        else
        {
            PVelocity.y += gravity;
        }

    }
    public void jump(InputAction.CallbackContext context)
    {
        if(isGrounded() == true)
        {
      
            animator.SetTrigger("Jumping");
            PVelocity.y = intialJumpSpeed;
        }
        

    }
}
