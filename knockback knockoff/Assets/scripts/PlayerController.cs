using System;
using System.Collections;
using System.Collections.Generic;
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

    //Jump
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce;

    //Is grounded
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask ground;

    public static int PlayerCount;


    protected void Awake()
    {
        PlayerCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded();
        location = gameObject.transform.position;
        
    }

    private void FixedUpdate()
    {
        movement();
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

    private void movement()
    {

        float Hspeed = Input.GetAxis("Horizontal") * speed;
       

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (rb.velocity.x > -15 && rb.velocity.x < 15f)
            {
                rb.AddForce(new Vector3(Hspeed * speed, 0f));
            }
        }
        if (isGrounded())
        {
            // rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15); //OLD SCRIPT 
            
            if (Input.GetKey(KeyCode.Space))
            {
                

                rb.AddForce(new Vector2(0, jumpForce) * speed * Time.deltaTime, ForceMode2D.Impulse);
                
            }
        }
    }



}
