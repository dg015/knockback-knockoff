using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerController : MonoBehaviour
{
    //basic movement
    [SerializeField] public Vector3 location;
    public Rigidbody2D rb;

    //New movement system
    public Vector2 PVelocity;
    [SerializeField] private bool MaxVelocityReached;

    [Header("Horizontal")]
    [SerializeField] private float accelerationTime;
    private float accelerationRate;
    [SerializeField] private float decelerationTime;
    private float decelerationRate;
    [SerializeField] private float maxSpeed;


    [Header("Veritcal")]
    [SerializeField] private float apexHeight;
    [SerializeField] private float apexTime;
    [SerializeField] private float GravityStrenght;
    private float gravity;
    private float intialJumpSpeed;
    

    //health system
    public bool alive = true;

    //Jump
    [Header("IsGrounded")]
    //Is grounded
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask ground;


    [Header("Multiplayer")]
    //multiplayer
    public static int PlayerCount;


    protected void Awake()
    {
        PlayerCount++;
    }
    private void Start()
    {
        //Aceleration formula
        accelerationRate = maxSpeed / accelerationTime;
        decelerationRate = maxSpeed / decelerationTime;

        // jumping formulas
        gravity = -GravityStrenght * apexHeight / (apexTime * apexHeight);
        intialJumpSpeed = 2 * apexHeight / apexTime;

    }

    // Update is called once per frame
    void Update()
    {
        CheckMaxVelocity();
        isGrounded();
        location = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 PlayerInput = new Vector2();
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        Movement(PlayerInput);
        VerticalForces();
        jump();
        CheckIfPlayerTurned(PlayerInput);
        rb.velocity = PVelocity;
        hasHitWall();
    }

    private void hasHitWall()
    {
        //check with boxcast if the player has hit the ceiling, if yes then reset speed and let them fall down
        if(Physics2D.BoxCast(transform.position, boxSize,0,transform.up,castDistance,ground))
        {
            Debug.Log("has hit ceiling");
            PVelocity.y = 0;
        }
        // now check if the have hit a wall if so then reset their fall speed
            //this one checks for the right side
        else if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, castDistance, ground) && !isGrounded())
        {
            Debug.Log("has hit right wall");
            PVelocity.x = 0;
        }
        else if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.right, castDistance, ground) && !isGrounded())
        {
            Debug.Log("has hit left wall");
            PVelocity.x = 0;
        }

    }

    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance,ground))
        {
           
            return true;
        }
        else 
        {
            
            return false;
        }
    }

    private void CheckIfPlayerTurned(Vector2 playerInput)
    {
        float moveDirection = playerInput.x;
        if(moveDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("goin right");
            
        }
        else if( moveDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Debug.Log("going left");
        }
    }

    

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + 5)
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    /*
    private void movement()
    {

        float Hspeed = Input.GetAxis("Horizontal") * speed;

        if (isGrounded())
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (rb.velocity.x > -15 && rb.velocity.x < 15f)
                {
                    rb.AddForce(new Vector3(Hspeed * speed, 0f));
                    //Debug.Log("ground running");
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                

                rb.AddForce(new Vector2(0, jumpForce) * speed * Time.deltaTime, ForceMode2D.Impulse);
                
            }
        }
        else if (isGrounded() == false)
        {
            
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (rb.velocity.x > -15 && rb.velocity.x < 15f)
                {
                    rb.AddForce(new Vector3(Hspeed * airSpeed, 0f));
                    
                }
            }
        }
    }
    */

    private void Movement(Vector2 playerInput)
    {
        //uses != so that it checks both for -1 and 1
        //gets aceleration
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            
            //apply velocity
            if (!MaxVelocityReached)
            {
                PVelocity.x += accelerationRate * playerInput.x * Time.deltaTime;
            }
        }
        else
        {
            //deceleration
            //decelrationRate is applied every second eats away the remaining velocity
            if (PVelocity.x>0)
            {
                PVelocity.x -= decelerationRate * Time.deltaTime;
                //Setting max will help by not allowing the speed to go over/lower the asked amount will make it be moving slightly forever
                PVelocity.x = Mathf.Max(PVelocity.x, 0);

            }
            else if (PVelocity.x<0)
            {
                PVelocity.x += decelerationRate * Time.deltaTime;
                PVelocity.x = Mathf.Min(PVelocity.x, 0);

            }
        }
        /*
        if (isGrounded())
        {
            //PVelocity.x = Mathf.Clamp(PVelocity.x, -maxSpeed, maxSpeed);

        }
        */
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
        if(isGrounded())
        {
            PVelocity.y = 0;
        }
        else
        {
            PVelocity.y += gravity;
        }

    }
    private void jump()
    {
        if(isGrounded() == true && (Input.GetButton("Jump")))
        {
            PVelocity.y = intialJumpSpeed;
        }
    }
}
