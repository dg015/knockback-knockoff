using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerController : MonoBehaviour
{
    //basic movement
    [SerializeField] public Vector3 location;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float airSpeed = 5;

    //New movement system

    [SerializeField] private Vector2 PVelocity;

    [Header("Horizontal")]
    [SerializeField] private float accelerationTime;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float decelerationTime;
    [SerializeField] private float decelerationRate;

    [SerializeField] private float maxSpeed;




    [Header("Veritcal")]

    [SerializeField] private float apexHeight;
    [SerializeField] private float apexTime;
    [SerializeField] private float gravity;
    [SerializeField] private float intialJumpSpeed;
    

    //health system
    public bool alive = true;

    //Jump

    [SerializeField] private float jumpForce;

    //Is grounded
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask ground;



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
        gravity = -2 * apexHeight / (apexTime * apexHeight);
        intialJumpSpeed = 2 * apexHeight / apexTime;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded();
        location = gameObject.transform.position;
        
    }

    private void FixedUpdate()
    {
       
        Vector2 PlayerInput = new Vector2();
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        Movement(PlayerInput);
        VerticalForces();
        rb.velocity = PVelocity;
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

    private void OnDrawGizmos()
    {
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
            Debug.Log("here");
            //apply velocity
            PVelocity.x += accelerationRate * playerInput.x * Time.deltaTime;
        }
        else
        {
            //deceleration
            //decelrationRate is applied every second eats away the remaining velocity
            if (PVelocity.x>0)
            {
                PVelocity.x -= decelerationRate * Time.deltaTime;
                PVelocity.x = Mathf.Max(PVelocity.x, 0);
            }
            else if (PVelocity.x<0)
            {
                PVelocity.x += decelerationRate * Time.deltaTime;
                PVelocity.x = Mathf.Min(PVelocity.x, 0);
            }

        }


    }


    private void VerticalForces()
    {
        if( isGrounded())
        {
            PVelocity.y = 0;
        }
        else
        {
            PVelocity.y = gravity;
        }

    }
    private void jump()
    {
        if(isGrounded() == true && (Input.GetAxisRaw("Verical")==0))
        {
            



        }
    }




}
